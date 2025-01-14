$input v_texCoord0

// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
#define DEFERRED_DIRECT_LIGHT
#include "Common.sh"
#include "UniformsFragment.sh"
#include "FragmentFunctions.sh"

uniform vec4 u_lightDataFragment[LIGHTDATA_FRAGMENT_SIZE];

#ifdef SHADOW_CONTACT
uniform mat4 viewProj;
uniform mat4 invViewProj;
#endif


SAMPLER2D(s_sceneTexture, 0);
SAMPLER2D(s_normalTexture, 1);
SAMPLER2D(s_gBuffer2Texture, 2);
SAMPLER2D(s_gBuffer3Texture, 9);
SAMPLER2D(s_depthTexture, 3);
SAMPLER2D(s_gBuffer4Texture, 10);
SAMPLER2D(s_gBuffer5Texture, 11);

#ifdef GLOBAL_LIGHT_MASK_SUPPORT
	#ifdef LIGHT_TYPE_POINT
		SAMPLERCUBE(s_lightMask, 4);
	#elif defined(LIGHT_TYPE_SPOTLIGHT) || defined(LIGHT_TYPE_DIRECTIONAL)
		SAMPLER2D(s_lightMask, 4);
	#endif
#endif

#ifdef SHADOW_MAP
	#ifdef LIGHT_TYPE_POINT
		#ifdef SHADOW_TEXTURE_FORMAT_BYTE4 //GLOBAL_SHADOW_TECHNIQUE_EVSM
			SAMPLERCUBE(s_shadowMapShadow, 5);
		#else
			SAMPLERCUBESHADOW(s_shadowMapShadow, 5);
		#endif
	#else
		#ifdef SHADOW_TEXTURE_FORMAT_BYTE4 //GLOBAL_SHADOW_TECHNIQUE_EVSM
			SAMPLER2DARRAY(s_shadowMapShadow, 5);
		#else
			SAMPLER2DARRAYSHADOW(s_shadowMapShadow, 5);
		#endif
	#endif
#endif

SAMPLER2D(s_brdfLUT, 8);

#ifdef SHADOW_MAP
	#include "ShadowReceiverFunctions.sh"
#endif

#define SHADING_MODEL_SUBSURFACE

#include "PBRFilament/common_math.sh"
#include "PBRFilament/brdf.sh"
#include "PBRFilament/PBRFilament.sh"

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

void main()
{
	vec4 fragCoord = getFragCoord();
	
	float rawDepth = texture2D(s_depthTexture, v_texCoord0).r;
	vec3 worldPosition = reconstructWorldPosition(u_invViewProj, v_texCoord0, rawDepth);

	//objectLightAttenuation
	float objectLightAttenuation = 1;
	#if defined(LIGHT_TYPE_SPOTLIGHT) || defined(LIGHT_TYPE_POINT)
		float lightDistance = length(u_lightPosition.xyz - worldPosition);
		objectLightAttenuation = getLightAttenuation(u_lightAttenuation, (float)lightDistance);
	#endif

	BRANCH
	if(objectLightAttenuation <= 0)
		discard;

	//lightWorldDirection
	vec3 lightWorldDirection;
	#ifdef LIGHT_TYPE_DIRECTIONAL
		lightWorldDirection = -u_lightDirection;
	#else
		lightWorldDirection = (vec3)normalize(u_lightPosition.xyz - worldPosition);
	#endif
	
	#ifdef LIGHT_TYPE_SPOTLIGHT
		// factor in spotlight angle
		float rho0 = saturate(dot(-u_lightDirection, lightWorldDirection));
		// factor = (rho - cos(outer/2)) / (cos(inner/2) - cos(outer/2)) ^ falloff 
		float spotFactor0 = saturate(pow(saturate(rho0 - u_lightSpot.y) / (u_lightSpot.x - u_lightSpot.y), u_lightSpot.z));
		objectLightAttenuation *= spotFactor0;
	#endif

	BRANCH
	if(objectLightAttenuation <= 0)
		discard;	
	
	vec3 baseColor = decodeRGBE8(texture2D(s_sceneTexture, v_texCoord0));
	vec4 normal_and_reflectance = texture2D(s_normalTexture, v_texCoord0);	
	vec3 normal = normalize(normal_and_reflectance.rgb * 2 - 1);

	vec4 gBuffer2Data = texture2D(s_gBuffer2Texture, v_texCoord0);
	vec4 gBuffer3Data = texture2D(s_gBuffer3Texture, v_texCoord0);
	vec4 gBuffer4Data = texture2D(s_gBuffer4Texture, v_texCoord0);
	vec4 gBuffer5Data = texture2D(s_gBuffer5Texture, v_texCoord0);

	vec4 tangent = gBuffer4Data * 2.0 - 1.0;
	tangent.xyz = normalize(tangent.xyz);

	int shadingModel = int(round(gBuffer5Data.x * 8.0));
	bool receiveDecals = gBuffer5Data.y == 1.0;
	float thickness = gBuffer5Data.z;
	float subsurfacePower = gBuffer5Data.w * 15.0;
	
	bool shadingModelSubsurface = shadingModel == 1;
	bool shadingModelSimple = shadingModel == 3;
	
	vec3 subsurfaceColor = vec3_splat(0);
	{
		vec3 v = decodeRGBE8(gBuffer3Data);
		if(shadingModelSubsurface)
			subsurfaceColor = v;
	}
	
	/*
	//get GBuffer 4
	vec4 tangent;
	bool shadingModelSimple;
	bool receiveDecals;
	vec4 gBuffer4Data = texture2D(s_gBuffer4Texture, v_texCoord0);	
	decodeGBuffer4(gBuffer4Data, tangent, shadingModelSimple, receiveDecals);
	*/
	
	vec3 bitangent = normalize(cross(tangent.xyz, normal) * tangent.w);

	vec3 toCamera = u_viewportOwnerCameraPosition - worldPosition.xyz;
	float cameraDistance = length(toCamera);
	toCamera = normalize(toCamera);

	//lightMaskMultiplier
	vec3 lightMaskMultiplier = vec3(1,1,1);
#ifdef GLOBAL_LIGHT_MASK_SUPPORT
	{
	#ifdef LIGHT_TYPE_POINT
		vec3 dir = normalize(worldPosition - u_lightPosition.xyz);
		dir = mul(u_lightMaskMatrix, vec4(dir, 0)).xyz;
		//!!!!flipped cubemaps, already applied in lightMaskTextureMatrixArray.
		//dir = float3(-dir.y, dir.z, dir.x);
		lightMaskMultiplier = textureCube(s_lightMask, dir).rgb;
	#elif defined(LIGHT_TYPE_SPOTLIGHT)
		vec4 texCoord = mul( u_lightMaskMatrix, vec4( worldPosition, 1 ) );
		lightMaskMultiplier = texture2DProj( s_lightMask, texCoord ).rgb;
	#elif defined(LIGHT_TYPE_DIRECTIONAL)
		vec2 texCoord = mul( u_lightMaskMatrix, vec4( worldPosition, 1 ) ).xy;
		lightMaskMultiplier = texture2D( s_lightMask, texCoord ).rgb;
	#endif
	}
#endif

	//shadows
	float shadowMultiplier = 1;
	#ifdef SHADOW_MAP
		float depth = getDepthValue(rawDepth, u_viewportOwnerNearClipDistance, u_viewportOwnerFarClipDistance);
		shadowMultiplier = getShadowMultiplier(worldPosition, cameraDistance, depth, lightWorldDirection, normal, v_texCoord0, fragCoord);
	#endif
	
	//light color
	vec3 lightColor = u_lightPower.rgb * u_cameraExposure * objectLightAttenuation * lightMaskMultiplier * shadowMultiplier;

	
	vec3 resultColor = vec3_splat(0);

	if(shadingModelSimple)
	{
		//Simple shading model
		resultColor.rgb = baseColor * lightColor;
	}
	else
	{
		//Lit, Subsurface shading models

#if GLOBAL_MATERIAL_SHADING == GLOBAL_MATERIAL_SHADING_SIMPLE
		resultColor.rgb = baseColor * lightColor * saturate(dot(normal, lightWorldDirection) / PI);
#else
		
		float metallic = gBuffer2Data.x;
		float roughness = gBuffer2Data.y;
		float ambientOcclusion = gBuffer2Data.z;
		float reflectance = normal_and_reflectance.a;

		//toLight
		vec3 toLight;
		#if defined(LIGHT_TYPE_SPOTLIGHT) || defined(LIGHT_TYPE_POINT)
			toLight = u_lightPosition.xyz - worldPosition;
		#elif LIGHT_TYPE_DIRECTIONAL
			toLight = -u_lightDirection;
		#else
			toLight = normal; // For ambient light dot(N, L) == 1, i.e. we can do L = N;
		#endif
		toLight = normalize(toLight);

		MaterialInputs material;

		material.baseColor = vec4(baseColor, 0.0);
		material.roughness = roughness;
		material.metallic = metallic;
		material.reflectance = reflectance;
		material.ambientOcclusion = ambientOcclusion;

		material.anisotropyDirection = vec3_splat(0);
		material.anisotropy = 0;

		//#if defined(SHADING_MODEL_SUBSURFACE)
		material.thickness = thickness;
		material.subsurfacePower = subsurfacePower;
		material.subsurfaceColor = subsurfaceColor;
		//#endif

		material.clearCoat = 0;
		material.clearCoatRoughness = 0;
		material.clearCoatNormal = vec3_splat(0);

		/*
		#if defined(SHADING_MODEL_CLOTH)
			material.sheenColor = vec3_splat(0);
			material.subsurfaceColor = vec3_splat(0);
		#endif
		*/

		setupPBRFilamentParams(material, tangent.xyz, bitangent, normal, vec3_splat(0), toLight, toCamera, gl_FrontFacing);

		PixelParams pixel;
		getPBRFilamentPixelParams(material, pixel);

		vec3 shadingResult;
		BRANCH
		if(shadingModelSubsurface)
			shadingResult = surfaceShadingSubSurface(pixel);
		else
			shadingResult = surfaceShadingStandard(pixel);
		resultColor.rgb = shadingResult * lightColor;
		//resultColor.rgb = surfaceShading(pixel) * lightColor;
		
#endif
	}
	
	gl_FragColor = vec4(resultColor, 1.0);
}

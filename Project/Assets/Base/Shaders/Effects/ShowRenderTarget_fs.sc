$input v_texCoord0

// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
#include "../Common.sh"
#include "../FragmentFunctions.sh"

SAMPLER2D(s_sourceTexture, 0);

#ifdef SHADOW_POINT_LIGHT
	SAMPLERCUBE(s_showTexture, 1);
#elif defined(SHADOW_DIRECTIONAL_LIGHT)
	SAMPLER2DARRAY(s_showTexture, 1);
#else
	SAMPLER2D(s_showTexture, 1);
#endif

uniform vec4/*float*/ intensity;
uniform vec4/*float*/ nearClipDistance;
uniform vec4/*float*/ farClipDistance;
uniform vec4/*int*/ mode;
uniform vec4/*float*/ multiplier;

void main()
{
	vec4 sourceColor = texture2D(s_sourceTexture, v_texCoord0);

	vec4 showColor;
	
#ifdef SHADOW_POINT_LIGHT

	float h = v_texCoord0.x * PI * 2.0;
	float v = (0.5 - v_texCoord0.y) * PI;
	vec3 coord = flipCubemapCoords(sphericalDirectionGetVector(vec2(h,v)));
	showColor = textureCube(s_showTexture, coord);
	
#elif defined(SHADOW_DIRECTIONAL_LIGHT)
	float index = 0.0;
	if(mode.x >= 3.0 && mode.x <= 6.0)
		index = float(round(mode.x - 3.0));
	showColor = texture2DArray(s_showTexture, vec3(v_texCoord0, index));	
#else	
	showColor = texture2D(s_showTexture, v_texCoord0);	
#endif	
	
	vec4 color;

	if(mode.x == 0.0)
	{
		//normal
		color = showColor;
	}
	else if(mode.x == 1.0)
	{
		//depth
		float depth = getDepthValue(showColor.r, nearClipDistance.x, farClipDistance.x);		
		float v = saturate(depth / farClipDistance.x * multiplier.x);
		//float v = saturate(depth * multiplier.x);
		color = vec4(v, v, v, 1.0);
	}
	else if(mode.x == 2.0)
	{
		//motion vector
		color = showColor * multiplier.x;
	}
	else
	{
		//shadow
		color = vec4(showColor.x, showColor.x, showColor.x, 1.0) * multiplier.x;
	}

	gl_FragColor = lerp(sourceColor, color, intensity.x);
}

$input a_position
$output v_texCoord0

// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
#include "Common.sh"

void main()
{      
	gl_Position = mul(u_modelViewProj, vec4(a_position, 1.0)).xyww;
	// thus z will be 1.0 after perspective divide

	v_texCoord0 = a_position.xyz;
}

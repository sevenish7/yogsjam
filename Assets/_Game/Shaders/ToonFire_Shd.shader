// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/ToonFire_Shd"
{
	Properties
	{
		_tex_FireShapeClamp("tex_FireShapeClamp", 2D) = "white" {}
		_NoiseTexture("NoiseTexture", 2D) = "white" {}
		_Noise1Speed("Noise 1 Speed", Float) = -1.5
		_Noise1Scale("Noise 1 Scale", Float) = 0.75
		_Noise2Scale("Noise 2 Scale", Float) = 1
		_Noise2Speed("Noise 2 Speed", Float) = -1
		_InnerFlameStep("Inner Flame Step", Range( 0 , 1)) = 0.9
		_InnerFlameColour("Inner Flame Colour", Color) = (0.9686275,0.8235295,0.2784314,1)
		_OuterFlameStep("Outer Flame Step", Range( 0 , 1)) = 0.1
		_OuterFlamColour("Outer FlamColour", Color) = (0.8000001,0,0,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _InnerFlameColour;
		uniform float _InnerFlameStep;
		uniform sampler2D _tex_FireShapeClamp;
		uniform float4 _tex_FireShapeClamp_ST;
		uniform sampler2D _NoiseTexture;
		uniform float _Noise1Speed;
		uniform float _Noise1Scale;
		uniform float _Noise2Speed;
		uniform float _Noise2Scale;
		uniform float _OuterFlameStep;
		uniform float4 _OuterFlamColour;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 temp_cast_0 = (_InnerFlameStep).xxxx;
			float2 uv_tex_FireShapeClamp = i.uv_texcoord * _tex_FireShapeClamp_ST.xy + _tex_FireShapeClamp_ST.zw;
			float4 tex2DNode2 = tex2D( _tex_FireShapeClamp, uv_tex_FireShapeClamp );
			float2 appendResult19 = (float2(0.0 , _Noise1Speed));
			float2 panner14 = ( 1.0 * _Time.y * appendResult19 + ( _Noise1Scale * i.uv_texcoord ));
			float2 appendResult20 = (float2(0.0 , _Noise2Speed));
			float2 panner15 = ( 1.0 * _Time.y * appendResult20 + ( i.uv_texcoord * _Noise2Scale ));
			float4 temp_output_31_0 = saturate( ( tex2DNode2 * ( ( 0.75 * tex2DNode2 ) + ( tex2D( _NoiseTexture, panner14 ) * tex2D( _NoiseTexture, panner15 ) ) ) ) );
			float4 temp_cast_1 = (_OuterFlameStep).xxxx;
			float4 temp_output_35_0 = step( temp_cast_1 , temp_output_31_0 );
			o.Emission = ( ( _InnerFlameColour * step( temp_cast_0 , temp_output_31_0 ) ) + ( temp_output_35_0 * _OuterFlamColour ) ).rgb;
			o.Alpha = temp_output_35_0.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17200
2570.667;49.33334;1235;556;2985.052;735.1987;2.93819;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-2065.855,-107.624;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-1968.592,70.5957;Inherit;False;Property;_Noise2Scale;Noise 2 Scale;4;0;Create;True;0;0;False;0;1;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1972.592,199.5957;Inherit;False;Property;_Noise2Speed;Noise 2 Speed;5;0;Create;True;0;0;False;0;-1;-0.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1923.496,-331.2591;Inherit;False;Property;_Noise1Speed;Noise 1 Speed;2;0;Create;True;0;0;False;0;-1.5;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1923.496,-193.2591;Inherit;False;Property;_Noise1Scale;Noise 1 Scale;3;0;Create;True;0;0;False;0;0.75;1.22;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1674.855,-199.924;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1650.055,66.57599;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-1669.926,-369.9355;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;20;-1641.149,184.5764;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;15;-1315.755,40.576;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;14;-1350.855,-277.924;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-787.906,-509.8305;Inherit;True;Property;_tex_FireShapeClamp;tex_FireShapeClamp;0;0;Create;True;0;0;False;0;-1;4c383cb8089a74f44bc9459764cb0ab0;4c383cb8089a74f44bc9459764cb0ab0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;43;-530.3461,-586.937;Inherit;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;0.75;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-985.5547,10.67597;Inherit;True;Property;_NoiseTexture;NoiseTexture;1;0;Create;True;0;0;False;0;-1;6df96ba22b03fbc4889829e16270d738;6df96ba22b03fbc4889829e16270d738;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-969.9548,-267.524;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Instance;11;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-400.7279,-466.9918;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-547.3124,-97.97405;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-337.0244,-98.0834;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-60.3766,-195.8805;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;31;162.8845,-180.4101;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;36;64.82877,109.6694;Inherit;False;Property;_OuterFlameStep;Outer Flame Step;8;0;Create;True;0;0;False;0;0.1;0.082;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;68.88452,-349.4101;Inherit;False;Property;_InnerFlameStep;Inner Flame Step;6;0;Create;True;0;0;False;0;0.9;0.633;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;35;423.6288,56.36945;Inherit;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;38;337.9182,-628.6299;Inherit;False;Property;_InnerFlameColour;Inner Flame Colour;7;0;Create;True;0;0;False;0;0.9686275,0.8235295,0.2784314,1;0.9686275,0.8235295,0.2784314,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;40;375.8922,352.8657;Inherit;False;Property;_OuterFlamColour;Outer FlamColour;9;0;Create;True;0;0;False;0;0.8000001,0,0,1;0.8000001,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;33;429.8845,-326.4101;Inherit;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;817.5573,268.2678;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;783.9135,-344.8687;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;1111.246,-222.5389;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1351.169,-158.8388;Float;False;True;2;ASEMaterialInspector;0;0;Unlit;Custom/ToonFire_Shd;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;22;0
WireConnection;17;1;16;0
WireConnection;18;0;16;0
WireConnection;18;1;23;0
WireConnection;19;1;21;0
WireConnection;20;1;24;0
WireConnection;15;0;18;0
WireConnection;15;2;20;0
WireConnection;14;0;17;0
WireConnection;14;2;19;0
WireConnection;11;1;15;0
WireConnection;12;1;14;0
WireConnection;44;0;43;0
WireConnection;44;1;2;0
WireConnection;13;0;12;0
WireConnection;13;1;11;0
WireConnection;26;0;44;0
WireConnection;26;1;13;0
WireConnection;27;0;2;0
WireConnection;27;1;26;0
WireConnection;31;0;27;0
WireConnection;35;0;36;0
WireConnection;35;1;31;0
WireConnection;33;0;34;0
WireConnection;33;1;31;0
WireConnection;39;0;35;0
WireConnection;39;1;40;0
WireConnection;37;0;38;0
WireConnection;37;1;33;0
WireConnection;42;0;37;0
WireConnection;42;1;39;0
WireConnection;0;2;42;0
WireConnection;0;9;35;0
ASEEND*/
//CHKSM=C5D6BF52EB373EEE742766934C7FB2083496ACBA
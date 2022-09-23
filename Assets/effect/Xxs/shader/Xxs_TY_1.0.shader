// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Xxs/TY_1.0"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][Enum(UnityEngine.Rendering.CullMode)]_Cull_Mode("Cull_Mode", Float) = 2
		[Enum(Default,0,On,1,Off,2)]_ZWrite_Mode("ZWrite_Mode", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)]_ZTest_Mode("ZTest_Mode", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)]_RGB_Src("RGB_Src", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)]_RGB_Dst("RGB_Dst", Float) = 1
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Main_Texture("Main_Texture", 2D) = "white" {}
		_Tex_ST_Main("Tex_ST_Main", Vector) = (1,1,0,0)
		_U_Clamp_Main("U_Clamp_Main", Float) = 0
		_V_Clamp_Main("V_Clamp_Main", Float) = 0
		_AR_Main("A/R_Main", Float) = 0
		_U_Speed_Main("U_Speed_Main", Float) = 0
		_V_Speed_Main("V_Speed_Main", Float) = 0
		_Rotator_Main("Rotator_Main", Range( 0 , 360)) = 0
		_Refine_Main("Refine_Main", Vector) = (1,1,2,0)
		[Toggle(_CUSTOM_MAIN_ON)] _Custom_Main("Custom_Main", Float) = 0
		_Custom12_Main_U("Custom1/2_Main_U", Float) = 0
		_xyzw_Main_U("x/y/z/w_Main_U", Float) = 0
		_Custom12_Main_V("Custom1/2_Main_V", Float) = 0
		_xyzw_Main_V("x/y/z/w_Main_V", Float) = 1
		_Mask_Texture("Mask_Texture", 2D) = "white" {}
		_Tex_ST_Mask("Tex_ST_Mask", Vector) = (1,1,0,0)
		_U_Clamp_Mask("U_Clamp_Mask", Float) = 0
		_V_Clamp_Mask("V_Clamp_Mask", Float) = 0
		_U_Speed_Mask("U_Speed_Mask", Float) = 0
		_V_Speed_Mask("V_Speed_Mask", Float) = 0
		_Rotator_Mask("Rotator_Mask", Range( 0 , 360)) = 0
		[Toggle(_CUSTOM_MASK_ON)] _Custom_Mask("Custom_Mask", Float) = 0
		_Custom12_Mask_U("Custom1/2_Mask_U", Float) = 0
		_xyzw_Mask_U("x/y/z/w_Mask_U", Float) = 2
		_Custom12_Mask_V("Custom1/2_Mask_V", Float) = 0
		_xyzw_Mask_V("x/y/z/w_Mask_V", Float) = 3
		_Disslove_Texture("Disslove_Texture", 2D) = "white" {}
		_Tex_TS_Disslove("Tex_TS_Disslove", Vector) = (1,1,0,0)
		_U_Clamp_Disslove("U_Clamp_Disslove", Float) = 0
		_V_Clamp_Disslove("V_Clamp_Disslove", Float) = 0
		_U_Speed_Disslove("U_Speed_Disslove", Float) = 0
		_V_Speed_Disslove("V_Speed_Disslove", Float) = 0
		_Rotator_Disslove("Rotator_Disslove", Range( 0 , 360)) = 0
		[Toggle(_CUSTOM_UV_DISSLOVE_ON)] _Custom_UV_Disslove("Custom_UV_Disslove", Float) = 0
		_Custom12_Diosslve_U("Custom1/2_Diosslve_U", Float) = 1
		_xyzw_Disslove_U("x/y/z/w_Disslove_U", Float) = 0
		_Custom12_Diosslve_V("Custom1/2_Diosslve_V", Float) = 1
		_xyzw_Disslove_V("x/y/z/w_Disslove_V", Float) = 1
		_Disslove_Tex_Power("Disslove_Tex_Power", Float) = 1
		_Disslove_Factor("Disslove_Factor", Float) = 0.5
		_Custom_DissloveCustom2_x("Custom_Disslove(Custom2_x)", Float) = 0
		_Soft_Disslove("Soft_Disslove", Float) = 0.5
		[Toggle(_DISSLOVE_SOFTSTING_ON)] _Disslove_SoftSting("Disslove_Soft/Sting", Float) = 0
		_Wide_Disslove("Wide_Disslove", Float) = 0
		[HDR]_Color_Wide("Color_Wide", Color) = (1,1,1,0)
		[Toggle(_MAIN_NOISE_ON)] _Main_Noise("Main_Noise", Float) = 0
		[Toggle(_MASK_NOISE_ON)] _Mask_Noise("Mask_Noise", Float) = 0
		[Toggle(_DISSLOVE_NOISE_ON)] _Disslove_Noise("Disslove_Noise", Float) = 0
		_Noise_Texture("Noise_Texture", 2D) = "white" {}
		_Tex_TS_Noise("Tex_TS_Noise", Vector) = (1,1,0,0)
		_V_Speed_Noise("V_Speed_Noise", Float) = 0
		_U_Speed_Noise("U_Speed_Noise", Float) = 0
		_Rotator_Noise("Rotator_Noise", Range( 0 , 360)) = 0
		_Noise_Intensity("Noise_Intensity", Float) = 1
		[Toggle(_CUSTOM_NOISE_INTENSITY_ON)] _Custom_Noise_Intensity("Custom_Noise_Intensity", Float) = 0
		_Custom12_Noise_Intensity("Custom1/2_Noise_Intensity", Float) = 1
		_xyzw_Noise_Intensity("x/y/z/w_Noise_Intensity", Float) = 3
		[Toggle(_DEPTH_FADE_ON)] _Depth_Fade("Depth_Fade", Float) = 0
		[ASEEnd]_Depth_Fade_Distance("Depth_Fade_Distance", Float) = 1

		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull [_Cull_Mode]
		AlphaToMask Off
		
		HLSLINCLUDE
		#pragma target 2.0

		#pragma prefer_hlslcc gles
		#pragma exclude_renderers d3d11_9x 

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS

		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }
			
			Blend [_RGB_Src] [_RGB_Dst], One OneMinusSrcAlpha
			ZWrite [_ZWrite_Mode]
			ZTest [_ZTest_Mode]
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 100801
			#define REQUIRE_DEPTH_TEXTURE 1

			
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _MAIN_NOISE_ON
			#pragma shader_feature_local _CUSTOM_MAIN_ON
			#pragma shader_feature_local _CUSTOM_NOISE_INTENSITY_ON
			#pragma shader_feature_local _MASK_NOISE_ON
			#pragma shader_feature_local _CUSTOM_MASK_ON
			#pragma shader_feature_local _DISSLOVE_SOFTSTING_ON
			#pragma shader_feature_local _DISSLOVE_NOISE_ON
			#pragma shader_feature_local _CUSTOM_UV_DISSLOVE_ON
			#pragma shader_feature_local _DEPTH_FADE_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
				float fogFactor : TEXCOORD2;
				#endif
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_color : COLOR;
				float4 ase_texcoord6 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Tex_ST_Mask;
			float4 _Color_Wide;
			float4 _Color;
			float4 _Refine_Main;
			float4 _Tex_ST_Main;
			float4 _Tex_TS_Disslove;
			float4 _Tex_TS_Noise;
			float _Custom12_Mask_U;
			float _xyzw_Mask_V;
			float _Custom12_Mask_V;
			float _Rotator_Mask;
			float _V_Clamp_Mask;
			float _Soft_Disslove;
			float _Custom_DissloveCustom2_x;
			float _Disslove_Factor;
			float _U_Speed_Disslove;
			float _xyzw_Mask_U;
			float _V_Speed_Disslove;
			float _xyzw_Disslove_U;
			float _Custom12_Diosslve_U;
			float _xyzw_Disslove_V;
			float _Custom12_Diosslve_V;
			float _Rotator_Disslove;
			float _V_Clamp_Disslove;
			float _Disslove_Tex_Power;
			float _Wide_Disslove;
			float _U_Clamp_Disslove;
			float _ZTest_Mode;
			float _U_Speed_Mask;
			float _AR_Main;
			float _RGB_Src;
			float _Cull_Mode;
			float _ZWrite_Mode;
			float _RGB_Dst;
			float _U_Clamp_Main;
			float _U_Speed_Main;
			float _V_Speed_Main;
			float _xyzw_Main_U;
			float _Custom12_Main_U;
			float _V_Speed_Mask;
			float _xyzw_Main_V;
			float _U_Speed_Noise;
			float _V_Speed_Noise;
			float _Rotator_Noise;
			float _Noise_Intensity;
			float _xyzw_Noise_Intensity;
			float _Custom12_Noise_Intensity;
			float _Rotator_Main;
			float _V_Clamp_Main;
			float _U_Clamp_Mask;
			float _Custom12_Main_V;
			float _Depth_Fade_Distance;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _Main_Texture;
			sampler2D _Noise_Texture;
			sampler2D _Mask_Texture;
			sampler2D _Disslove_Texture;
			uniform float4 _CameraDepthTexture_TexelSize;


						
			VertexOutput VertexFunction ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord6 = screenPos;
				
				o.ase_texcoord3 = v.ase_texcoord;
				o.ase_texcoord4 = v.ase_texcoord1;
				o.ase_texcoord5.xy = v.ase_texcoord2.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord5.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				#ifdef ASE_FOG
				o.fogFactor = ComputeFogFactor( positionCS.z );
				#endif
				o.clipPos = positionCS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif
				float2 appendResult49_g122 = (float2(_U_Speed_Main , _V_Speed_Main));
				float2 texCoord6_g113 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g113 = _Tex_ST_Main;
				float2 appendResult1_g113 = (float2(break18_g113.x , break18_g113.y));
				float2 appendResult3_g113 = (float2(break18_g113.z , break18_g113.w));
				float2 break64 = ( ( texCoord6_g113 * appendResult1_g113 ) + appendResult3_g113 );
				float2 appendResult65 = (float2(break64.x , break64.y));
				float temp_output_25_0_g118 = _xyzw_Main_U;
				float4 texCoord19_g118 = IN.ase_texcoord3;
				texCoord19_g118.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g118 = IN.ase_texcoord4;
				texCoord29_g118.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g118 = (float4(texCoord19_g118.z , texCoord19_g118.w , texCoord29_g118.x , texCoord29_g118.y));
				float2 texCoord34_g118 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g118 = (float4(texCoord29_g118.z , texCoord29_g118.w , texCoord34_g118.x , texCoord34_g118.y));
				float4 break30_g118 = ( _Custom12_Main_U == 0.0 ? appendResult38_g118 : appendResult39_g118 );
				float ifLocalVar20_g118 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g118 )
				ifLocalVar20_g118 = ( temp_output_25_0_g118 == 0.0 ? break30_g118.x : break30_g118.y );
				else if( 2.0 == temp_output_25_0_g118 )
				ifLocalVar20_g118 = break30_g118.z;
				else if( 2.0 < temp_output_25_0_g118 )
				ifLocalVar20_g118 = ( temp_output_25_0_g118 == 4.0 ? 0.0 : break30_g118.w );
				float temp_output_25_0_g116 = _xyzw_Main_V;
				float4 texCoord19_g116 = IN.ase_texcoord3;
				texCoord19_g116.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g116 = IN.ase_texcoord4;
				texCoord29_g116.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g116 = (float4(texCoord19_g116.z , texCoord19_g116.w , texCoord29_g116.x , texCoord29_g116.y));
				float2 texCoord34_g116 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g116 = (float4(texCoord29_g116.z , texCoord29_g116.w , texCoord34_g116.x , texCoord34_g116.y));
				float4 break30_g116 = ( _Custom12_Main_V == 0.0 ? appendResult38_g116 : appendResult39_g116 );
				float ifLocalVar20_g116 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g116 )
				ifLocalVar20_g116 = ( temp_output_25_0_g116 == 0.0 ? break30_g116.x : break30_g116.y );
				else if( 2.0 == temp_output_25_0_g116 )
				ifLocalVar20_g116 = break30_g116.z;
				else if( 2.0 < temp_output_25_0_g116 )
				ifLocalVar20_g116 = ( temp_output_25_0_g116 == 4.0 ? 0.0 : break30_g116.w );
				float2 appendResult71 = (float2(( break64.x + ifLocalVar20_g118 ) , ( break64.y + ifLocalVar20_g116 )));
				#ifdef _CUSTOM_MAIN_ON
				float2 staticSwitch63 = appendResult71;
				#else
				float2 staticSwitch63 = appendResult65;
				#endif
				float2 Main_UV72 = staticSwitch63;
				float2 appendResult49_g109 = (float2(_U_Speed_Noise , _V_Speed_Noise));
				float2 texCoord6_g107 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g107 = _Tex_TS_Noise;
				float2 appendResult1_g107 = (float2(break18_g107.x , break18_g107.y));
				float2 appendResult3_g107 = (float2(break18_g107.z , break18_g107.w));
				float2 panner44_g109 = ( 1.0 * _Time.y * appendResult49_g109 + ( ( texCoord6_g107 * appendResult1_g107 ) + appendResult3_g107 ));
				float cos55_g109 = cos( ( ( ( _Rotator_Noise / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g109 = sin( ( ( ( _Rotator_Noise / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g109 = mul( panner44_g109 - float2( 0.5,0.5 ) , float2x2( cos55_g109 , -sin55_g109 , sin55_g109 , cos55_g109 )) + float2( 0.5,0.5 );
				float2 break52_g109 = rotator55_g109;
				float2 break54_g109 = rotator55_g109;
				float clampResult60_g109 = clamp( break54_g109.x , 0.0 , 1.0 );
				float clampResult50_g109 = clamp( break54_g109.y , 0.0 , 1.0 );
				float2 appendResult53_g109 = (float2(( (float)0 == 0.0 ? break52_g109.x : clampResult60_g109 ) , ( (float)0 == 0.0 ? break52_g109.y : clampResult50_g109 )));
				float4 tex2DNode27_g109 = tex2D( _Noise_Texture, appendResult53_g109 );
				float temp_output_25_0_g110 = _xyzw_Noise_Intensity;
				float4 texCoord19_g110 = IN.ase_texcoord3;
				texCoord19_g110.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g110 = IN.ase_texcoord4;
				texCoord29_g110.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g110 = (float4(texCoord19_g110.z , texCoord19_g110.w , texCoord29_g110.x , texCoord29_g110.y));
				float2 texCoord34_g110 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g110 = (float4(texCoord29_g110.z , texCoord29_g110.w , texCoord34_g110.x , texCoord34_g110.y));
				float4 break30_g110 = ( _Custom12_Noise_Intensity == 0.0 ? appendResult38_g110 : appendResult39_g110 );
				float ifLocalVar20_g110 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g110 )
				ifLocalVar20_g110 = ( temp_output_25_0_g110 == 0.0 ? break30_g110.x : break30_g110.y );
				else if( 2.0 == temp_output_25_0_g110 )
				ifLocalVar20_g110 = break30_g110.z;
				else if( 2.0 < temp_output_25_0_g110 )
				ifLocalVar20_g110 = ( temp_output_25_0_g110 == 4.0 ? 0.0 : break30_g110.w );
				#ifdef _CUSTOM_NOISE_INTENSITY_ON
				float staticSwitch188 = ifLocalVar20_g110;
				#else
				float staticSwitch188 = ( tex2DNode27_g109.r * _Noise_Intensity );
				#endif
				float Noise_A178 = staticSwitch188;
				#ifdef _MAIN_NOISE_ON
				float2 staticSwitch193 = ( Main_UV72 + Noise_A178 );
				#else
				float2 staticSwitch193 = Main_UV72;
				#endif
				float2 panner44_g122 = ( 1.0 * _Time.y * appendResult49_g122 + staticSwitch193);
				float cos55_g122 = cos( ( ( ( _Rotator_Main / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g122 = sin( ( ( ( _Rotator_Main / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g122 = mul( panner44_g122 - float2( 0.5,0.5 ) , float2x2( cos55_g122 , -sin55_g122 , sin55_g122 , cos55_g122 )) + float2( 0.5,0.5 );
				float2 break52_g122 = rotator55_g122;
				float2 break54_g122 = rotator55_g122;
				float clampResult60_g122 = clamp( break54_g122.x , 0.0 , 1.0 );
				float clampResult50_g122 = clamp( break54_g122.y , 0.0 , 1.0 );
				float2 appendResult53_g122 = (float2(( (float)(int)_U_Clamp_Main == 0.0 ? break52_g122.x : clampResult60_g122 ) , ( (float)(int)_V_Clamp_Main == 0.0 ? break52_g122.y : clampResult50_g122 )));
				float4 tex2DNode27_g122 = tex2D( _Main_Texture, appendResult53_g122 );
				float3 temp_output_9_0_g125 = tex2DNode27_g122.rgb;
				float4 break7_g125 = _Refine_Main;
				float3 temp_cast_7 = (max( break7_g125.z , 0.01 )).xxx;
				float3 lerpResult4_g125 = lerp( ( temp_output_9_0_g125 * break7_g125.x ) , ( pow( temp_output_9_0_g125 , temp_cast_7 ) * break7_g125.y ) , break7_g125.w);
				float3 Main_Color74 = lerpResult4_g125;
				float2 appendResult49_g123 = (float2(_U_Speed_Mask , _V_Speed_Mask));
				float2 texCoord6_g111 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g111 = _Tex_ST_Mask;
				float2 appendResult1_g111 = (float2(break18_g111.x , break18_g111.y));
				float2 appendResult3_g111 = (float2(break18_g111.z , break18_g111.w));
				float2 break92 = ( ( texCoord6_g111 * appendResult1_g111 ) + appendResult3_g111 );
				float2 appendResult93 = (float2(break92.x , break92.y));
				float temp_output_25_0_g117 = _xyzw_Mask_U;
				float4 texCoord19_g117 = IN.ase_texcoord3;
				texCoord19_g117.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g117 = IN.ase_texcoord4;
				texCoord29_g117.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g117 = (float4(texCoord19_g117.z , texCoord19_g117.w , texCoord29_g117.x , texCoord29_g117.y));
				float2 texCoord34_g117 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g117 = (float4(texCoord29_g117.z , texCoord29_g117.w , texCoord34_g117.x , texCoord34_g117.y));
				float4 break30_g117 = ( _Custom12_Mask_U == 0.0 ? appendResult38_g117 : appendResult39_g117 );
				float ifLocalVar20_g117 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g117 )
				ifLocalVar20_g117 = ( temp_output_25_0_g117 == 0.0 ? break30_g117.x : break30_g117.y );
				else if( 2.0 == temp_output_25_0_g117 )
				ifLocalVar20_g117 = break30_g117.z;
				else if( 2.0 < temp_output_25_0_g117 )
				ifLocalVar20_g117 = ( temp_output_25_0_g117 == 4.0 ? 0.0 : break30_g117.w );
				float temp_output_25_0_g119 = _xyzw_Mask_V;
				float4 texCoord19_g119 = IN.ase_texcoord3;
				texCoord19_g119.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g119 = IN.ase_texcoord4;
				texCoord29_g119.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g119 = (float4(texCoord19_g119.z , texCoord19_g119.w , texCoord29_g119.x , texCoord29_g119.y));
				float2 texCoord34_g119 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g119 = (float4(texCoord29_g119.z , texCoord29_g119.w , texCoord34_g119.x , texCoord34_g119.y));
				float4 break30_g119 = ( _Custom12_Mask_V == 0.0 ? appendResult38_g119 : appendResult39_g119 );
				float ifLocalVar20_g119 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g119 )
				ifLocalVar20_g119 = ( temp_output_25_0_g119 == 0.0 ? break30_g119.x : break30_g119.y );
				else if( 2.0 == temp_output_25_0_g119 )
				ifLocalVar20_g119 = break30_g119.z;
				else if( 2.0 < temp_output_25_0_g119 )
				ifLocalVar20_g119 = ( temp_output_25_0_g119 == 4.0 ? 0.0 : break30_g119.w );
				float2 appendResult101 = (float2(( break92.x + ifLocalVar20_g117 ) , ( break92.y + ifLocalVar20_g119 )));
				#ifdef _CUSTOM_MASK_ON
				float2 staticSwitch94 = appendResult101;
				#else
				float2 staticSwitch94 = appendResult93;
				#endif
				float2 Mask_UV103 = staticSwitch94;
				#ifdef _MASK_NOISE_ON
				float2 staticSwitch197 = ( Mask_UV103 + Noise_A178 );
				#else
				float2 staticSwitch197 = Mask_UV103;
				#endif
				float2 panner44_g123 = ( 1.0 * _Time.y * appendResult49_g123 + staticSwitch197);
				float cos55_g123 = cos( ( ( ( _Rotator_Mask / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g123 = sin( ( ( ( _Rotator_Mask / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g123 = mul( panner44_g123 - float2( 0.5,0.5 ) , float2x2( cos55_g123 , -sin55_g123 , sin55_g123 , cos55_g123 )) + float2( 0.5,0.5 );
				float2 break52_g123 = rotator55_g123;
				float2 break54_g123 = rotator55_g123;
				float clampResult60_g123 = clamp( break54_g123.x , 0.0 , 1.0 );
				float clampResult50_g123 = clamp( break54_g123.y , 0.0 , 1.0 );
				float2 appendResult53_g123 = (float2(( (float)(int)_U_Clamp_Mask == 0.0 ? break52_g123.x : clampResult60_g123 ) , ( (float)(int)_V_Clamp_Mask == 0.0 ? break52_g123.y : clampResult50_g123 )));
				float4 tex2DNode27_g123 = tex2D( _Mask_Texture, appendResult53_g123 );
				float Mask_R108 = tex2DNode27_g123.r;
				float temp_output_51_0_g121 = _Soft_Disslove;
				float4 texCoord28_g121 = IN.ase_texcoord4;
				texCoord28_g121.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_53_0_g121 = ( _Custom_DissloveCustom2_x == 0.0 ? ( _Disslove_Factor + 0.001 ) : ( 0.0 == 0.0 ? texCoord28_g121.z : (1.0 + (IN.ase_color.a - 0.0) * (0.0 - 1.0) / (1.0 - 0.0)) ) );
				float temp_output_2_0_g121 = ( ( temp_output_51_0_g121 + 1.0 ) * temp_output_53_0_g121 );
				float2 appendResult49_g120 = (float2(_U_Speed_Disslove , _V_Speed_Disslove));
				float2 texCoord6_g76 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g76 = _Tex_TS_Disslove;
				float2 appendResult1_g76 = (float2(break18_g76.x , break18_g76.y));
				float2 appendResult3_g76 = (float2(break18_g76.z , break18_g76.w));
				float2 break125 = ( ( texCoord6_g76 * appendResult1_g76 ) + appendResult3_g76 );
				float2 appendResult128 = (float2(break125.x , break125.y));
				float temp_output_25_0_g100 = _xyzw_Disslove_U;
				float4 texCoord19_g100 = IN.ase_texcoord3;
				texCoord19_g100.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g100 = IN.ase_texcoord4;
				texCoord29_g100.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g100 = (float4(texCoord19_g100.z , texCoord19_g100.w , texCoord29_g100.x , texCoord29_g100.y));
				float2 texCoord34_g100 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g100 = (float4(texCoord29_g100.z , texCoord29_g100.w , texCoord34_g100.x , texCoord34_g100.y));
				float4 break30_g100 = ( _Custom12_Diosslve_U == 0.0 ? appendResult38_g100 : appendResult39_g100 );
				float ifLocalVar20_g100 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g100 )
				ifLocalVar20_g100 = ( temp_output_25_0_g100 == 0.0 ? break30_g100.x : break30_g100.y );
				else if( 2.0 == temp_output_25_0_g100 )
				ifLocalVar20_g100 = break30_g100.z;
				else if( 2.0 < temp_output_25_0_g100 )
				ifLocalVar20_g100 = ( temp_output_25_0_g100 == 4.0 ? 0.0 : break30_g100.w );
				float temp_output_25_0_g106 = _xyzw_Disslove_V;
				float4 texCoord19_g106 = IN.ase_texcoord3;
				texCoord19_g106.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g106 = IN.ase_texcoord4;
				texCoord29_g106.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g106 = (float4(texCoord19_g106.z , texCoord19_g106.w , texCoord29_g106.x , texCoord29_g106.y));
				float2 texCoord34_g106 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g106 = (float4(texCoord29_g106.z , texCoord29_g106.w , texCoord34_g106.x , texCoord34_g106.y));
				float4 break30_g106 = ( _Custom12_Diosslve_V == 0.0 ? appendResult38_g106 : appendResult39_g106 );
				float ifLocalVar20_g106 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g106 )
				ifLocalVar20_g106 = ( temp_output_25_0_g106 == 0.0 ? break30_g106.x : break30_g106.y );
				else if( 2.0 == temp_output_25_0_g106 )
				ifLocalVar20_g106 = break30_g106.z;
				else if( 2.0 < temp_output_25_0_g106 )
				ifLocalVar20_g106 = ( temp_output_25_0_g106 == 4.0 ? 0.0 : break30_g106.w );
				float2 appendResult129 = (float2(( break125.x + ifLocalVar20_g100 ) , ( break125.y + ifLocalVar20_g106 )));
				#ifdef _CUSTOM_UV_DISSLOVE_ON
				float2 staticSwitch130 = appendResult129;
				#else
				float2 staticSwitch130 = appendResult128;
				#endif
				float2 Disslove_UV135 = staticSwitch130;
				#ifdef _DISSLOVE_NOISE_ON
				float2 staticSwitch200 = ( Disslove_UV135 + Noise_A178 );
				#else
				float2 staticSwitch200 = Disslove_UV135;
				#endif
				float2 panner44_g120 = ( 1.0 * _Time.y * appendResult49_g120 + staticSwitch200);
				float cos55_g120 = cos( ( ( ( _Rotator_Disslove / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g120 = sin( ( ( ( _Rotator_Disslove / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g120 = mul( panner44_g120 - float2( 0.5,0.5 ) , float2x2( cos55_g120 , -sin55_g120 , sin55_g120 , cos55_g120 )) + float2( 0.5,0.5 );
				float2 break52_g120 = rotator55_g120;
				float2 break54_g120 = rotator55_g120;
				float clampResult60_g120 = clamp( break54_g120.x , 0.0 , 1.0 );
				float clampResult50_g120 = clamp( break54_g120.y , 0.0 , 1.0 );
				float2 appendResult53_g120 = (float2(( (float)(int)_U_Clamp_Disslove == 0.0 ? break52_g120.x : clampResult60_g120 ) , ( (float)(int)_V_Clamp_Disslove == 0.0 ? break52_g120.y : clampResult50_g120 )));
				float4 tex2DNode27_g120 = tex2D( _Disslove_Texture, appendResult53_g120 );
				float Disslove_Tex138 = tex2DNode27_g120.r;
				float temp_output_57_0_g121 = saturate( pow( Disslove_Tex138 , _Disslove_Tex_Power ) );
				float temp_output_4_0_g121 = saturate( ( ( ( temp_output_57_0_g121 / 1.0 ) + ( 0.0 == 0.0 ? temp_output_57_0_g121 : 1.0 ) ) / 2.0 ) );
				float smoothstepResult21_g121 = smoothstep( ( temp_output_2_0_g121 - temp_output_51_0_g121 ) , temp_output_2_0_g121 , temp_output_4_0_g121);
				float SoftA144 = smoothstepResult21_g121;
				float temp_output_44_0_g121 = _Wide_Disslove;
				float temp_output_3_0_g121 = ( temp_output_53_0_g121 * ( 1.0 + temp_output_44_0_g121 ) );
				float StingA145 = step( ( temp_output_3_0_g121 - temp_output_44_0_g121 ) , temp_output_4_0_g121 );
				#ifdef _DISSLOVE_SOFTSTING_ON
				float staticSwitch157 = StingA145;
				#else
				float staticSwitch157 = SoftA144;
				#endif
				float Disslove_A166 = staticSwitch157;
				float StingA1146 = step( temp_output_3_0_g121 , temp_output_4_0_g121 );
				float4 Disslove_Wide_Color163 = ( ( StingA145 - StingA1146 ) * _Color_Wide );
				float Main_AR75 = ( (float)(int)_AR_Main == 0.0 ? tex2DNode27_g122.a : tex2DNode27_g122.r );
				float temp_output_82_0 = ( IN.ase_color.a * Main_AR75 * _Color.a * Mask_R108 * Disslove_A166 );
				
				float4 screenPos = IN.ase_texcoord6;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth202 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth202 = abs( ( screenDepth202 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _Depth_Fade_Distance ) );
				#ifdef _DEPTH_FADE_ON
				float staticSwitch203 = ( temp_output_82_0 * saturate( pow( distanceDepth202 , abs( 2.0 ) ) ) );
				#else
				float staticSwitch203 = temp_output_82_0;
				#endif
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = ( ( float4( Main_Color74 , 0.0 ) * IN.ase_color * _Color * Mask_R108 * Disslove_A166 ) + ( Disslove_Wide_Color163 * temp_output_82_0 ) ).rgb;
				float Alpha = staticSwitch203;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					clip( Alpha - AlphaClipThreshold );
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogFactor );
				#endif

				return half4( Color, Alpha );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }

			ZWrite On
			ZTest LEqual
			AlphaToMask Off
			ColorMask 0

			HLSLPROGRAM
			
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 100801
			#define REQUIRE_DEPTH_TEXTURE 1

			
			#pragma vertex vert
			#pragma fragment frag
#if ASE_SRP_VERSION >= 110000
			#pragma multi_compile _ _CASTING_PUNCTUAL_LIGHT_SHADOW
#endif
			#define SHADERPASS SHADERPASS_SHADOWCASTER

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _DEPTH_FADE_ON
			#pragma shader_feature_local _MAIN_NOISE_ON
			#pragma shader_feature_local _CUSTOM_MAIN_ON
			#pragma shader_feature_local _CUSTOM_NOISE_INTENSITY_ON
			#pragma shader_feature_local _MASK_NOISE_ON
			#pragma shader_feature_local _CUSTOM_MASK_ON
			#pragma shader_feature_local _DISSLOVE_SOFTSTING_ON
			#pragma shader_feature_local _DISSLOVE_NOISE_ON
			#pragma shader_feature_local _CUSTOM_UV_DISSLOVE_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_color : COLOR;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Tex_ST_Mask;
			float4 _Color_Wide;
			float4 _Color;
			float4 _Refine_Main;
			float4 _Tex_ST_Main;
			float4 _Tex_TS_Disslove;
			float4 _Tex_TS_Noise;
			float _Custom12_Mask_U;
			float _xyzw_Mask_V;
			float _Custom12_Mask_V;
			float _Rotator_Mask;
			float _V_Clamp_Mask;
			float _Soft_Disslove;
			float _Custom_DissloveCustom2_x;
			float _Disslove_Factor;
			float _U_Speed_Disslove;
			float _xyzw_Mask_U;
			float _V_Speed_Disslove;
			float _xyzw_Disslove_U;
			float _Custom12_Diosslve_U;
			float _xyzw_Disslove_V;
			float _Custom12_Diosslve_V;
			float _Rotator_Disslove;
			float _V_Clamp_Disslove;
			float _Disslove_Tex_Power;
			float _Wide_Disslove;
			float _U_Clamp_Disslove;
			float _ZTest_Mode;
			float _U_Speed_Mask;
			float _AR_Main;
			float _RGB_Src;
			float _Cull_Mode;
			float _ZWrite_Mode;
			float _RGB_Dst;
			float _U_Clamp_Main;
			float _U_Speed_Main;
			float _V_Speed_Main;
			float _xyzw_Main_U;
			float _Custom12_Main_U;
			float _V_Speed_Mask;
			float _xyzw_Main_V;
			float _U_Speed_Noise;
			float _V_Speed_Noise;
			float _Rotator_Noise;
			float _Noise_Intensity;
			float _xyzw_Noise_Intensity;
			float _Custom12_Noise_Intensity;
			float _Rotator_Main;
			float _V_Clamp_Main;
			float _U_Clamp_Mask;
			float _Custom12_Main_V;
			float _Depth_Fade_Distance;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _Main_Texture;
			sampler2D _Noise_Texture;
			sampler2D _Mask_Texture;
			sampler2D _Disslove_Texture;
			uniform float4 _CameraDepthTexture_TexelSize;


			
			float3 _LightDirection;
#if ASE_SRP_VERSION >= 110000 
			float3 _LightPosition;
#endif
			VertexOutput VertexFunction( VertexInput v )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord5 = screenPos;
				
				o.ase_color = v.ase_color;
				o.ase_texcoord2 = v.ase_texcoord;
				o.ase_texcoord3 = v.ase_texcoord1;
				o.ase_texcoord4.xy = v.ase_texcoord2.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord4.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				float3 normalWS = TransformObjectToWorldDir( v.ase_normal );
#if ASE_SRP_VERSION >= 110000 
			#if _CASTING_PUNCTUAL_LIGHT_SHADOW
				float3 lightDirectionWS = normalize(_LightPosition - positionWS);
			#else
				float3 lightDirectionWS = _LightDirection;
			#endif
				float4 clipPos = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));
			#if UNITY_REVERSED_Z
				clipPos.z = min(clipPos.z, UNITY_NEAR_CLIP_VALUE);
			#else
				clipPos.z = max(clipPos.z, UNITY_NEAR_CLIP_VALUE);
			#endif
#else
				float4 clipPos = TransformWorldToHClip( ApplyShadowBias( positionWS, normalWS, _LightDirection ) );
				#if UNITY_REVERSED_Z
					clipPos.z = min(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
				#else
					clipPos.z = max(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
				#endif
#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				o.clipPos = clipPos;

				return o;
			}
			
			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_color = v.ase_color;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord2 = v.ase_texcoord2;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 appendResult49_g122 = (float2(_U_Speed_Main , _V_Speed_Main));
				float2 texCoord6_g113 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g113 = _Tex_ST_Main;
				float2 appendResult1_g113 = (float2(break18_g113.x , break18_g113.y));
				float2 appendResult3_g113 = (float2(break18_g113.z , break18_g113.w));
				float2 break64 = ( ( texCoord6_g113 * appendResult1_g113 ) + appendResult3_g113 );
				float2 appendResult65 = (float2(break64.x , break64.y));
				float temp_output_25_0_g118 = _xyzw_Main_U;
				float4 texCoord19_g118 = IN.ase_texcoord2;
				texCoord19_g118.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g118 = IN.ase_texcoord3;
				texCoord29_g118.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g118 = (float4(texCoord19_g118.z , texCoord19_g118.w , texCoord29_g118.x , texCoord29_g118.y));
				float2 texCoord34_g118 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g118 = (float4(texCoord29_g118.z , texCoord29_g118.w , texCoord34_g118.x , texCoord34_g118.y));
				float4 break30_g118 = ( _Custom12_Main_U == 0.0 ? appendResult38_g118 : appendResult39_g118 );
				float ifLocalVar20_g118 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g118 )
				ifLocalVar20_g118 = ( temp_output_25_0_g118 == 0.0 ? break30_g118.x : break30_g118.y );
				else if( 2.0 == temp_output_25_0_g118 )
				ifLocalVar20_g118 = break30_g118.z;
				else if( 2.0 < temp_output_25_0_g118 )
				ifLocalVar20_g118 = ( temp_output_25_0_g118 == 4.0 ? 0.0 : break30_g118.w );
				float temp_output_25_0_g116 = _xyzw_Main_V;
				float4 texCoord19_g116 = IN.ase_texcoord2;
				texCoord19_g116.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g116 = IN.ase_texcoord3;
				texCoord29_g116.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g116 = (float4(texCoord19_g116.z , texCoord19_g116.w , texCoord29_g116.x , texCoord29_g116.y));
				float2 texCoord34_g116 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g116 = (float4(texCoord29_g116.z , texCoord29_g116.w , texCoord34_g116.x , texCoord34_g116.y));
				float4 break30_g116 = ( _Custom12_Main_V == 0.0 ? appendResult38_g116 : appendResult39_g116 );
				float ifLocalVar20_g116 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g116 )
				ifLocalVar20_g116 = ( temp_output_25_0_g116 == 0.0 ? break30_g116.x : break30_g116.y );
				else if( 2.0 == temp_output_25_0_g116 )
				ifLocalVar20_g116 = break30_g116.z;
				else if( 2.0 < temp_output_25_0_g116 )
				ifLocalVar20_g116 = ( temp_output_25_0_g116 == 4.0 ? 0.0 : break30_g116.w );
				float2 appendResult71 = (float2(( break64.x + ifLocalVar20_g118 ) , ( break64.y + ifLocalVar20_g116 )));
				#ifdef _CUSTOM_MAIN_ON
				float2 staticSwitch63 = appendResult71;
				#else
				float2 staticSwitch63 = appendResult65;
				#endif
				float2 Main_UV72 = staticSwitch63;
				float2 appendResult49_g109 = (float2(_U_Speed_Noise , _V_Speed_Noise));
				float2 texCoord6_g107 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g107 = _Tex_TS_Noise;
				float2 appendResult1_g107 = (float2(break18_g107.x , break18_g107.y));
				float2 appendResult3_g107 = (float2(break18_g107.z , break18_g107.w));
				float2 panner44_g109 = ( 1.0 * _Time.y * appendResult49_g109 + ( ( texCoord6_g107 * appendResult1_g107 ) + appendResult3_g107 ));
				float cos55_g109 = cos( ( ( ( _Rotator_Noise / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g109 = sin( ( ( ( _Rotator_Noise / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g109 = mul( panner44_g109 - float2( 0.5,0.5 ) , float2x2( cos55_g109 , -sin55_g109 , sin55_g109 , cos55_g109 )) + float2( 0.5,0.5 );
				float2 break52_g109 = rotator55_g109;
				float2 break54_g109 = rotator55_g109;
				float clampResult60_g109 = clamp( break54_g109.x , 0.0 , 1.0 );
				float clampResult50_g109 = clamp( break54_g109.y , 0.0 , 1.0 );
				float2 appendResult53_g109 = (float2(( (float)0 == 0.0 ? break52_g109.x : clampResult60_g109 ) , ( (float)0 == 0.0 ? break52_g109.y : clampResult50_g109 )));
				float4 tex2DNode27_g109 = tex2D( _Noise_Texture, appendResult53_g109 );
				float temp_output_25_0_g110 = _xyzw_Noise_Intensity;
				float4 texCoord19_g110 = IN.ase_texcoord2;
				texCoord19_g110.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g110 = IN.ase_texcoord3;
				texCoord29_g110.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g110 = (float4(texCoord19_g110.z , texCoord19_g110.w , texCoord29_g110.x , texCoord29_g110.y));
				float2 texCoord34_g110 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g110 = (float4(texCoord29_g110.z , texCoord29_g110.w , texCoord34_g110.x , texCoord34_g110.y));
				float4 break30_g110 = ( _Custom12_Noise_Intensity == 0.0 ? appendResult38_g110 : appendResult39_g110 );
				float ifLocalVar20_g110 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g110 )
				ifLocalVar20_g110 = ( temp_output_25_0_g110 == 0.0 ? break30_g110.x : break30_g110.y );
				else if( 2.0 == temp_output_25_0_g110 )
				ifLocalVar20_g110 = break30_g110.z;
				else if( 2.0 < temp_output_25_0_g110 )
				ifLocalVar20_g110 = ( temp_output_25_0_g110 == 4.0 ? 0.0 : break30_g110.w );
				#ifdef _CUSTOM_NOISE_INTENSITY_ON
				float staticSwitch188 = ifLocalVar20_g110;
				#else
				float staticSwitch188 = ( tex2DNode27_g109.r * _Noise_Intensity );
				#endif
				float Noise_A178 = staticSwitch188;
				#ifdef _MAIN_NOISE_ON
				float2 staticSwitch193 = ( Main_UV72 + Noise_A178 );
				#else
				float2 staticSwitch193 = Main_UV72;
				#endif
				float2 panner44_g122 = ( 1.0 * _Time.y * appendResult49_g122 + staticSwitch193);
				float cos55_g122 = cos( ( ( ( _Rotator_Main / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g122 = sin( ( ( ( _Rotator_Main / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g122 = mul( panner44_g122 - float2( 0.5,0.5 ) , float2x2( cos55_g122 , -sin55_g122 , sin55_g122 , cos55_g122 )) + float2( 0.5,0.5 );
				float2 break52_g122 = rotator55_g122;
				float2 break54_g122 = rotator55_g122;
				float clampResult60_g122 = clamp( break54_g122.x , 0.0 , 1.0 );
				float clampResult50_g122 = clamp( break54_g122.y , 0.0 , 1.0 );
				float2 appendResult53_g122 = (float2(( (float)(int)_U_Clamp_Main == 0.0 ? break52_g122.x : clampResult60_g122 ) , ( (float)(int)_V_Clamp_Main == 0.0 ? break52_g122.y : clampResult50_g122 )));
				float4 tex2DNode27_g122 = tex2D( _Main_Texture, appendResult53_g122 );
				float Main_AR75 = ( (float)(int)_AR_Main == 0.0 ? tex2DNode27_g122.a : tex2DNode27_g122.r );
				float2 appendResult49_g123 = (float2(_U_Speed_Mask , _V_Speed_Mask));
				float2 texCoord6_g111 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g111 = _Tex_ST_Mask;
				float2 appendResult1_g111 = (float2(break18_g111.x , break18_g111.y));
				float2 appendResult3_g111 = (float2(break18_g111.z , break18_g111.w));
				float2 break92 = ( ( texCoord6_g111 * appendResult1_g111 ) + appendResult3_g111 );
				float2 appendResult93 = (float2(break92.x , break92.y));
				float temp_output_25_0_g117 = _xyzw_Mask_U;
				float4 texCoord19_g117 = IN.ase_texcoord2;
				texCoord19_g117.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g117 = IN.ase_texcoord3;
				texCoord29_g117.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g117 = (float4(texCoord19_g117.z , texCoord19_g117.w , texCoord29_g117.x , texCoord29_g117.y));
				float2 texCoord34_g117 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g117 = (float4(texCoord29_g117.z , texCoord29_g117.w , texCoord34_g117.x , texCoord34_g117.y));
				float4 break30_g117 = ( _Custom12_Mask_U == 0.0 ? appendResult38_g117 : appendResult39_g117 );
				float ifLocalVar20_g117 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g117 )
				ifLocalVar20_g117 = ( temp_output_25_0_g117 == 0.0 ? break30_g117.x : break30_g117.y );
				else if( 2.0 == temp_output_25_0_g117 )
				ifLocalVar20_g117 = break30_g117.z;
				else if( 2.0 < temp_output_25_0_g117 )
				ifLocalVar20_g117 = ( temp_output_25_0_g117 == 4.0 ? 0.0 : break30_g117.w );
				float temp_output_25_0_g119 = _xyzw_Mask_V;
				float4 texCoord19_g119 = IN.ase_texcoord2;
				texCoord19_g119.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g119 = IN.ase_texcoord3;
				texCoord29_g119.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g119 = (float4(texCoord19_g119.z , texCoord19_g119.w , texCoord29_g119.x , texCoord29_g119.y));
				float2 texCoord34_g119 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g119 = (float4(texCoord29_g119.z , texCoord29_g119.w , texCoord34_g119.x , texCoord34_g119.y));
				float4 break30_g119 = ( _Custom12_Mask_V == 0.0 ? appendResult38_g119 : appendResult39_g119 );
				float ifLocalVar20_g119 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g119 )
				ifLocalVar20_g119 = ( temp_output_25_0_g119 == 0.0 ? break30_g119.x : break30_g119.y );
				else if( 2.0 == temp_output_25_0_g119 )
				ifLocalVar20_g119 = break30_g119.z;
				else if( 2.0 < temp_output_25_0_g119 )
				ifLocalVar20_g119 = ( temp_output_25_0_g119 == 4.0 ? 0.0 : break30_g119.w );
				float2 appendResult101 = (float2(( break92.x + ifLocalVar20_g117 ) , ( break92.y + ifLocalVar20_g119 )));
				#ifdef _CUSTOM_MASK_ON
				float2 staticSwitch94 = appendResult101;
				#else
				float2 staticSwitch94 = appendResult93;
				#endif
				float2 Mask_UV103 = staticSwitch94;
				#ifdef _MASK_NOISE_ON
				float2 staticSwitch197 = ( Mask_UV103 + Noise_A178 );
				#else
				float2 staticSwitch197 = Mask_UV103;
				#endif
				float2 panner44_g123 = ( 1.0 * _Time.y * appendResult49_g123 + staticSwitch197);
				float cos55_g123 = cos( ( ( ( _Rotator_Mask / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g123 = sin( ( ( ( _Rotator_Mask / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g123 = mul( panner44_g123 - float2( 0.5,0.5 ) , float2x2( cos55_g123 , -sin55_g123 , sin55_g123 , cos55_g123 )) + float2( 0.5,0.5 );
				float2 break52_g123 = rotator55_g123;
				float2 break54_g123 = rotator55_g123;
				float clampResult60_g123 = clamp( break54_g123.x , 0.0 , 1.0 );
				float clampResult50_g123 = clamp( break54_g123.y , 0.0 , 1.0 );
				float2 appendResult53_g123 = (float2(( (float)(int)_U_Clamp_Mask == 0.0 ? break52_g123.x : clampResult60_g123 ) , ( (float)(int)_V_Clamp_Mask == 0.0 ? break52_g123.y : clampResult50_g123 )));
				float4 tex2DNode27_g123 = tex2D( _Mask_Texture, appendResult53_g123 );
				float Mask_R108 = tex2DNode27_g123.r;
				float temp_output_51_0_g121 = _Soft_Disslove;
				float4 texCoord28_g121 = IN.ase_texcoord3;
				texCoord28_g121.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_53_0_g121 = ( _Custom_DissloveCustom2_x == 0.0 ? ( _Disslove_Factor + 0.001 ) : ( 0.0 == 0.0 ? texCoord28_g121.z : (1.0 + (IN.ase_color.a - 0.0) * (0.0 - 1.0) / (1.0 - 0.0)) ) );
				float temp_output_2_0_g121 = ( ( temp_output_51_0_g121 + 1.0 ) * temp_output_53_0_g121 );
				float2 appendResult49_g120 = (float2(_U_Speed_Disslove , _V_Speed_Disslove));
				float2 texCoord6_g76 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g76 = _Tex_TS_Disslove;
				float2 appendResult1_g76 = (float2(break18_g76.x , break18_g76.y));
				float2 appendResult3_g76 = (float2(break18_g76.z , break18_g76.w));
				float2 break125 = ( ( texCoord6_g76 * appendResult1_g76 ) + appendResult3_g76 );
				float2 appendResult128 = (float2(break125.x , break125.y));
				float temp_output_25_0_g100 = _xyzw_Disslove_U;
				float4 texCoord19_g100 = IN.ase_texcoord2;
				texCoord19_g100.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g100 = IN.ase_texcoord3;
				texCoord29_g100.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g100 = (float4(texCoord19_g100.z , texCoord19_g100.w , texCoord29_g100.x , texCoord29_g100.y));
				float2 texCoord34_g100 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g100 = (float4(texCoord29_g100.z , texCoord29_g100.w , texCoord34_g100.x , texCoord34_g100.y));
				float4 break30_g100 = ( _Custom12_Diosslve_U == 0.0 ? appendResult38_g100 : appendResult39_g100 );
				float ifLocalVar20_g100 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g100 )
				ifLocalVar20_g100 = ( temp_output_25_0_g100 == 0.0 ? break30_g100.x : break30_g100.y );
				else if( 2.0 == temp_output_25_0_g100 )
				ifLocalVar20_g100 = break30_g100.z;
				else if( 2.0 < temp_output_25_0_g100 )
				ifLocalVar20_g100 = ( temp_output_25_0_g100 == 4.0 ? 0.0 : break30_g100.w );
				float temp_output_25_0_g106 = _xyzw_Disslove_V;
				float4 texCoord19_g106 = IN.ase_texcoord2;
				texCoord19_g106.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g106 = IN.ase_texcoord3;
				texCoord29_g106.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g106 = (float4(texCoord19_g106.z , texCoord19_g106.w , texCoord29_g106.x , texCoord29_g106.y));
				float2 texCoord34_g106 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g106 = (float4(texCoord29_g106.z , texCoord29_g106.w , texCoord34_g106.x , texCoord34_g106.y));
				float4 break30_g106 = ( _Custom12_Diosslve_V == 0.0 ? appendResult38_g106 : appendResult39_g106 );
				float ifLocalVar20_g106 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g106 )
				ifLocalVar20_g106 = ( temp_output_25_0_g106 == 0.0 ? break30_g106.x : break30_g106.y );
				else if( 2.0 == temp_output_25_0_g106 )
				ifLocalVar20_g106 = break30_g106.z;
				else if( 2.0 < temp_output_25_0_g106 )
				ifLocalVar20_g106 = ( temp_output_25_0_g106 == 4.0 ? 0.0 : break30_g106.w );
				float2 appendResult129 = (float2(( break125.x + ifLocalVar20_g100 ) , ( break125.y + ifLocalVar20_g106 )));
				#ifdef _CUSTOM_UV_DISSLOVE_ON
				float2 staticSwitch130 = appendResult129;
				#else
				float2 staticSwitch130 = appendResult128;
				#endif
				float2 Disslove_UV135 = staticSwitch130;
				#ifdef _DISSLOVE_NOISE_ON
				float2 staticSwitch200 = ( Disslove_UV135 + Noise_A178 );
				#else
				float2 staticSwitch200 = Disslove_UV135;
				#endif
				float2 panner44_g120 = ( 1.0 * _Time.y * appendResult49_g120 + staticSwitch200);
				float cos55_g120 = cos( ( ( ( _Rotator_Disslove / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g120 = sin( ( ( ( _Rotator_Disslove / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g120 = mul( panner44_g120 - float2( 0.5,0.5 ) , float2x2( cos55_g120 , -sin55_g120 , sin55_g120 , cos55_g120 )) + float2( 0.5,0.5 );
				float2 break52_g120 = rotator55_g120;
				float2 break54_g120 = rotator55_g120;
				float clampResult60_g120 = clamp( break54_g120.x , 0.0 , 1.0 );
				float clampResult50_g120 = clamp( break54_g120.y , 0.0 , 1.0 );
				float2 appendResult53_g120 = (float2(( (float)(int)_U_Clamp_Disslove == 0.0 ? break52_g120.x : clampResult60_g120 ) , ( (float)(int)_V_Clamp_Disslove == 0.0 ? break52_g120.y : clampResult50_g120 )));
				float4 tex2DNode27_g120 = tex2D( _Disslove_Texture, appendResult53_g120 );
				float Disslove_Tex138 = tex2DNode27_g120.r;
				float temp_output_57_0_g121 = saturate( pow( Disslove_Tex138 , _Disslove_Tex_Power ) );
				float temp_output_4_0_g121 = saturate( ( ( ( temp_output_57_0_g121 / 1.0 ) + ( 0.0 == 0.0 ? temp_output_57_0_g121 : 1.0 ) ) / 2.0 ) );
				float smoothstepResult21_g121 = smoothstep( ( temp_output_2_0_g121 - temp_output_51_0_g121 ) , temp_output_2_0_g121 , temp_output_4_0_g121);
				float SoftA144 = smoothstepResult21_g121;
				float temp_output_44_0_g121 = _Wide_Disslove;
				float temp_output_3_0_g121 = ( temp_output_53_0_g121 * ( 1.0 + temp_output_44_0_g121 ) );
				float StingA145 = step( ( temp_output_3_0_g121 - temp_output_44_0_g121 ) , temp_output_4_0_g121 );
				#ifdef _DISSLOVE_SOFTSTING_ON
				float staticSwitch157 = StingA145;
				#else
				float staticSwitch157 = SoftA144;
				#endif
				float Disslove_A166 = staticSwitch157;
				float temp_output_82_0 = ( IN.ase_color.a * Main_AR75 * _Color.a * Mask_R108 * Disslove_A166 );
				float4 screenPos = IN.ase_texcoord5;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth202 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth202 = abs( ( screenDepth202 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _Depth_Fade_Distance ) );
				#ifdef _DEPTH_FADE_ON
				float staticSwitch203 = ( temp_output_82_0 * saturate( pow( distanceDepth202 , abs( 2.0 ) ) ) );
				#else
				float staticSwitch203 = temp_output_82_0;
				#endif
				
				float Alpha = staticSwitch203;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					#ifdef _ALPHATEST_SHADOW_ON
						clip(Alpha - AlphaClipThresholdShadow);
					#else
						clip(Alpha - AlphaClipThreshold);
					#endif
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif
				return 0;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask 0
			AlphaToMask Off

			HLSLPROGRAM
			
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 100801
			#define REQUIRE_DEPTH_TEXTURE 1

			
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _DEPTH_FADE_ON
			#pragma shader_feature_local _MAIN_NOISE_ON
			#pragma shader_feature_local _CUSTOM_MAIN_ON
			#pragma shader_feature_local _CUSTOM_NOISE_INTENSITY_ON
			#pragma shader_feature_local _MASK_NOISE_ON
			#pragma shader_feature_local _CUSTOM_MASK_ON
			#pragma shader_feature_local _DISSLOVE_SOFTSTING_ON
			#pragma shader_feature_local _DISSLOVE_NOISE_ON
			#pragma shader_feature_local _CUSTOM_UV_DISSLOVE_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_color : COLOR;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Tex_ST_Mask;
			float4 _Color_Wide;
			float4 _Color;
			float4 _Refine_Main;
			float4 _Tex_ST_Main;
			float4 _Tex_TS_Disslove;
			float4 _Tex_TS_Noise;
			float _Custom12_Mask_U;
			float _xyzw_Mask_V;
			float _Custom12_Mask_V;
			float _Rotator_Mask;
			float _V_Clamp_Mask;
			float _Soft_Disslove;
			float _Custom_DissloveCustom2_x;
			float _Disslove_Factor;
			float _U_Speed_Disslove;
			float _xyzw_Mask_U;
			float _V_Speed_Disslove;
			float _xyzw_Disslove_U;
			float _Custom12_Diosslve_U;
			float _xyzw_Disslove_V;
			float _Custom12_Diosslve_V;
			float _Rotator_Disslove;
			float _V_Clamp_Disslove;
			float _Disslove_Tex_Power;
			float _Wide_Disslove;
			float _U_Clamp_Disslove;
			float _ZTest_Mode;
			float _U_Speed_Mask;
			float _AR_Main;
			float _RGB_Src;
			float _Cull_Mode;
			float _ZWrite_Mode;
			float _RGB_Dst;
			float _U_Clamp_Main;
			float _U_Speed_Main;
			float _V_Speed_Main;
			float _xyzw_Main_U;
			float _Custom12_Main_U;
			float _V_Speed_Mask;
			float _xyzw_Main_V;
			float _U_Speed_Noise;
			float _V_Speed_Noise;
			float _Rotator_Noise;
			float _Noise_Intensity;
			float _xyzw_Noise_Intensity;
			float _Custom12_Noise_Intensity;
			float _Rotator_Main;
			float _V_Clamp_Main;
			float _U_Clamp_Mask;
			float _Custom12_Main_V;
			float _Depth_Fade_Distance;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _Main_Texture;
			sampler2D _Noise_Texture;
			sampler2D _Mask_Texture;
			sampler2D _Disslove_Texture;
			uniform float4 _CameraDepthTexture_TexelSize;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord5 = screenPos;
				
				o.ase_color = v.ase_color;
				o.ase_texcoord2 = v.ase_texcoord;
				o.ase_texcoord3 = v.ase_texcoord1;
				o.ase_texcoord4.xy = v.ase_texcoord2.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord4.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				o.clipPos = TransformWorldToHClip( positionWS );
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_color = v.ase_color;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord2 = v.ase_texcoord2;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 appendResult49_g122 = (float2(_U_Speed_Main , _V_Speed_Main));
				float2 texCoord6_g113 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g113 = _Tex_ST_Main;
				float2 appendResult1_g113 = (float2(break18_g113.x , break18_g113.y));
				float2 appendResult3_g113 = (float2(break18_g113.z , break18_g113.w));
				float2 break64 = ( ( texCoord6_g113 * appendResult1_g113 ) + appendResult3_g113 );
				float2 appendResult65 = (float2(break64.x , break64.y));
				float temp_output_25_0_g118 = _xyzw_Main_U;
				float4 texCoord19_g118 = IN.ase_texcoord2;
				texCoord19_g118.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g118 = IN.ase_texcoord3;
				texCoord29_g118.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g118 = (float4(texCoord19_g118.z , texCoord19_g118.w , texCoord29_g118.x , texCoord29_g118.y));
				float2 texCoord34_g118 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g118 = (float4(texCoord29_g118.z , texCoord29_g118.w , texCoord34_g118.x , texCoord34_g118.y));
				float4 break30_g118 = ( _Custom12_Main_U == 0.0 ? appendResult38_g118 : appendResult39_g118 );
				float ifLocalVar20_g118 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g118 )
				ifLocalVar20_g118 = ( temp_output_25_0_g118 == 0.0 ? break30_g118.x : break30_g118.y );
				else if( 2.0 == temp_output_25_0_g118 )
				ifLocalVar20_g118 = break30_g118.z;
				else if( 2.0 < temp_output_25_0_g118 )
				ifLocalVar20_g118 = ( temp_output_25_0_g118 == 4.0 ? 0.0 : break30_g118.w );
				float temp_output_25_0_g116 = _xyzw_Main_V;
				float4 texCoord19_g116 = IN.ase_texcoord2;
				texCoord19_g116.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g116 = IN.ase_texcoord3;
				texCoord29_g116.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g116 = (float4(texCoord19_g116.z , texCoord19_g116.w , texCoord29_g116.x , texCoord29_g116.y));
				float2 texCoord34_g116 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g116 = (float4(texCoord29_g116.z , texCoord29_g116.w , texCoord34_g116.x , texCoord34_g116.y));
				float4 break30_g116 = ( _Custom12_Main_V == 0.0 ? appendResult38_g116 : appendResult39_g116 );
				float ifLocalVar20_g116 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g116 )
				ifLocalVar20_g116 = ( temp_output_25_0_g116 == 0.0 ? break30_g116.x : break30_g116.y );
				else if( 2.0 == temp_output_25_0_g116 )
				ifLocalVar20_g116 = break30_g116.z;
				else if( 2.0 < temp_output_25_0_g116 )
				ifLocalVar20_g116 = ( temp_output_25_0_g116 == 4.0 ? 0.0 : break30_g116.w );
				float2 appendResult71 = (float2(( break64.x + ifLocalVar20_g118 ) , ( break64.y + ifLocalVar20_g116 )));
				#ifdef _CUSTOM_MAIN_ON
				float2 staticSwitch63 = appendResult71;
				#else
				float2 staticSwitch63 = appendResult65;
				#endif
				float2 Main_UV72 = staticSwitch63;
				float2 appendResult49_g109 = (float2(_U_Speed_Noise , _V_Speed_Noise));
				float2 texCoord6_g107 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g107 = _Tex_TS_Noise;
				float2 appendResult1_g107 = (float2(break18_g107.x , break18_g107.y));
				float2 appendResult3_g107 = (float2(break18_g107.z , break18_g107.w));
				float2 panner44_g109 = ( 1.0 * _Time.y * appendResult49_g109 + ( ( texCoord6_g107 * appendResult1_g107 ) + appendResult3_g107 ));
				float cos55_g109 = cos( ( ( ( _Rotator_Noise / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g109 = sin( ( ( ( _Rotator_Noise / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g109 = mul( panner44_g109 - float2( 0.5,0.5 ) , float2x2( cos55_g109 , -sin55_g109 , sin55_g109 , cos55_g109 )) + float2( 0.5,0.5 );
				float2 break52_g109 = rotator55_g109;
				float2 break54_g109 = rotator55_g109;
				float clampResult60_g109 = clamp( break54_g109.x , 0.0 , 1.0 );
				float clampResult50_g109 = clamp( break54_g109.y , 0.0 , 1.0 );
				float2 appendResult53_g109 = (float2(( (float)0 == 0.0 ? break52_g109.x : clampResult60_g109 ) , ( (float)0 == 0.0 ? break52_g109.y : clampResult50_g109 )));
				float4 tex2DNode27_g109 = tex2D( _Noise_Texture, appendResult53_g109 );
				float temp_output_25_0_g110 = _xyzw_Noise_Intensity;
				float4 texCoord19_g110 = IN.ase_texcoord2;
				texCoord19_g110.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g110 = IN.ase_texcoord3;
				texCoord29_g110.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g110 = (float4(texCoord19_g110.z , texCoord19_g110.w , texCoord29_g110.x , texCoord29_g110.y));
				float2 texCoord34_g110 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g110 = (float4(texCoord29_g110.z , texCoord29_g110.w , texCoord34_g110.x , texCoord34_g110.y));
				float4 break30_g110 = ( _Custom12_Noise_Intensity == 0.0 ? appendResult38_g110 : appendResult39_g110 );
				float ifLocalVar20_g110 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g110 )
				ifLocalVar20_g110 = ( temp_output_25_0_g110 == 0.0 ? break30_g110.x : break30_g110.y );
				else if( 2.0 == temp_output_25_0_g110 )
				ifLocalVar20_g110 = break30_g110.z;
				else if( 2.0 < temp_output_25_0_g110 )
				ifLocalVar20_g110 = ( temp_output_25_0_g110 == 4.0 ? 0.0 : break30_g110.w );
				#ifdef _CUSTOM_NOISE_INTENSITY_ON
				float staticSwitch188 = ifLocalVar20_g110;
				#else
				float staticSwitch188 = ( tex2DNode27_g109.r * _Noise_Intensity );
				#endif
				float Noise_A178 = staticSwitch188;
				#ifdef _MAIN_NOISE_ON
				float2 staticSwitch193 = ( Main_UV72 + Noise_A178 );
				#else
				float2 staticSwitch193 = Main_UV72;
				#endif
				float2 panner44_g122 = ( 1.0 * _Time.y * appendResult49_g122 + staticSwitch193);
				float cos55_g122 = cos( ( ( ( _Rotator_Main / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g122 = sin( ( ( ( _Rotator_Main / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g122 = mul( panner44_g122 - float2( 0.5,0.5 ) , float2x2( cos55_g122 , -sin55_g122 , sin55_g122 , cos55_g122 )) + float2( 0.5,0.5 );
				float2 break52_g122 = rotator55_g122;
				float2 break54_g122 = rotator55_g122;
				float clampResult60_g122 = clamp( break54_g122.x , 0.0 , 1.0 );
				float clampResult50_g122 = clamp( break54_g122.y , 0.0 , 1.0 );
				float2 appendResult53_g122 = (float2(( (float)(int)_U_Clamp_Main == 0.0 ? break52_g122.x : clampResult60_g122 ) , ( (float)(int)_V_Clamp_Main == 0.0 ? break52_g122.y : clampResult50_g122 )));
				float4 tex2DNode27_g122 = tex2D( _Main_Texture, appendResult53_g122 );
				float Main_AR75 = ( (float)(int)_AR_Main == 0.0 ? tex2DNode27_g122.a : tex2DNode27_g122.r );
				float2 appendResult49_g123 = (float2(_U_Speed_Mask , _V_Speed_Mask));
				float2 texCoord6_g111 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g111 = _Tex_ST_Mask;
				float2 appendResult1_g111 = (float2(break18_g111.x , break18_g111.y));
				float2 appendResult3_g111 = (float2(break18_g111.z , break18_g111.w));
				float2 break92 = ( ( texCoord6_g111 * appendResult1_g111 ) + appendResult3_g111 );
				float2 appendResult93 = (float2(break92.x , break92.y));
				float temp_output_25_0_g117 = _xyzw_Mask_U;
				float4 texCoord19_g117 = IN.ase_texcoord2;
				texCoord19_g117.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g117 = IN.ase_texcoord3;
				texCoord29_g117.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g117 = (float4(texCoord19_g117.z , texCoord19_g117.w , texCoord29_g117.x , texCoord29_g117.y));
				float2 texCoord34_g117 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g117 = (float4(texCoord29_g117.z , texCoord29_g117.w , texCoord34_g117.x , texCoord34_g117.y));
				float4 break30_g117 = ( _Custom12_Mask_U == 0.0 ? appendResult38_g117 : appendResult39_g117 );
				float ifLocalVar20_g117 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g117 )
				ifLocalVar20_g117 = ( temp_output_25_0_g117 == 0.0 ? break30_g117.x : break30_g117.y );
				else if( 2.0 == temp_output_25_0_g117 )
				ifLocalVar20_g117 = break30_g117.z;
				else if( 2.0 < temp_output_25_0_g117 )
				ifLocalVar20_g117 = ( temp_output_25_0_g117 == 4.0 ? 0.0 : break30_g117.w );
				float temp_output_25_0_g119 = _xyzw_Mask_V;
				float4 texCoord19_g119 = IN.ase_texcoord2;
				texCoord19_g119.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g119 = IN.ase_texcoord3;
				texCoord29_g119.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g119 = (float4(texCoord19_g119.z , texCoord19_g119.w , texCoord29_g119.x , texCoord29_g119.y));
				float2 texCoord34_g119 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g119 = (float4(texCoord29_g119.z , texCoord29_g119.w , texCoord34_g119.x , texCoord34_g119.y));
				float4 break30_g119 = ( _Custom12_Mask_V == 0.0 ? appendResult38_g119 : appendResult39_g119 );
				float ifLocalVar20_g119 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g119 )
				ifLocalVar20_g119 = ( temp_output_25_0_g119 == 0.0 ? break30_g119.x : break30_g119.y );
				else if( 2.0 == temp_output_25_0_g119 )
				ifLocalVar20_g119 = break30_g119.z;
				else if( 2.0 < temp_output_25_0_g119 )
				ifLocalVar20_g119 = ( temp_output_25_0_g119 == 4.0 ? 0.0 : break30_g119.w );
				float2 appendResult101 = (float2(( break92.x + ifLocalVar20_g117 ) , ( break92.y + ifLocalVar20_g119 )));
				#ifdef _CUSTOM_MASK_ON
				float2 staticSwitch94 = appendResult101;
				#else
				float2 staticSwitch94 = appendResult93;
				#endif
				float2 Mask_UV103 = staticSwitch94;
				#ifdef _MASK_NOISE_ON
				float2 staticSwitch197 = ( Mask_UV103 + Noise_A178 );
				#else
				float2 staticSwitch197 = Mask_UV103;
				#endif
				float2 panner44_g123 = ( 1.0 * _Time.y * appendResult49_g123 + staticSwitch197);
				float cos55_g123 = cos( ( ( ( _Rotator_Mask / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g123 = sin( ( ( ( _Rotator_Mask / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g123 = mul( panner44_g123 - float2( 0.5,0.5 ) , float2x2( cos55_g123 , -sin55_g123 , sin55_g123 , cos55_g123 )) + float2( 0.5,0.5 );
				float2 break52_g123 = rotator55_g123;
				float2 break54_g123 = rotator55_g123;
				float clampResult60_g123 = clamp( break54_g123.x , 0.0 , 1.0 );
				float clampResult50_g123 = clamp( break54_g123.y , 0.0 , 1.0 );
				float2 appendResult53_g123 = (float2(( (float)(int)_U_Clamp_Mask == 0.0 ? break52_g123.x : clampResult60_g123 ) , ( (float)(int)_V_Clamp_Mask == 0.0 ? break52_g123.y : clampResult50_g123 )));
				float4 tex2DNode27_g123 = tex2D( _Mask_Texture, appendResult53_g123 );
				float Mask_R108 = tex2DNode27_g123.r;
				float temp_output_51_0_g121 = _Soft_Disslove;
				float4 texCoord28_g121 = IN.ase_texcoord3;
				texCoord28_g121.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_53_0_g121 = ( _Custom_DissloveCustom2_x == 0.0 ? ( _Disslove_Factor + 0.001 ) : ( 0.0 == 0.0 ? texCoord28_g121.z : (1.0 + (IN.ase_color.a - 0.0) * (0.0 - 1.0) / (1.0 - 0.0)) ) );
				float temp_output_2_0_g121 = ( ( temp_output_51_0_g121 + 1.0 ) * temp_output_53_0_g121 );
				float2 appendResult49_g120 = (float2(_U_Speed_Disslove , _V_Speed_Disslove));
				float2 texCoord6_g76 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 break18_g76 = _Tex_TS_Disslove;
				float2 appendResult1_g76 = (float2(break18_g76.x , break18_g76.y));
				float2 appendResult3_g76 = (float2(break18_g76.z , break18_g76.w));
				float2 break125 = ( ( texCoord6_g76 * appendResult1_g76 ) + appendResult3_g76 );
				float2 appendResult128 = (float2(break125.x , break125.y));
				float temp_output_25_0_g100 = _xyzw_Disslove_U;
				float4 texCoord19_g100 = IN.ase_texcoord2;
				texCoord19_g100.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g100 = IN.ase_texcoord3;
				texCoord29_g100.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g100 = (float4(texCoord19_g100.z , texCoord19_g100.w , texCoord29_g100.x , texCoord29_g100.y));
				float2 texCoord34_g100 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g100 = (float4(texCoord29_g100.z , texCoord29_g100.w , texCoord34_g100.x , texCoord34_g100.y));
				float4 break30_g100 = ( _Custom12_Diosslve_U == 0.0 ? appendResult38_g100 : appendResult39_g100 );
				float ifLocalVar20_g100 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g100 )
				ifLocalVar20_g100 = ( temp_output_25_0_g100 == 0.0 ? break30_g100.x : break30_g100.y );
				else if( 2.0 == temp_output_25_0_g100 )
				ifLocalVar20_g100 = break30_g100.z;
				else if( 2.0 < temp_output_25_0_g100 )
				ifLocalVar20_g100 = ( temp_output_25_0_g100 == 4.0 ? 0.0 : break30_g100.w );
				float temp_output_25_0_g106 = _xyzw_Disslove_V;
				float4 texCoord19_g106 = IN.ase_texcoord2;
				texCoord19_g106.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 texCoord29_g106 = IN.ase_texcoord3;
				texCoord29_g106.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult38_g106 = (float4(texCoord19_g106.z , texCoord19_g106.w , texCoord29_g106.x , texCoord29_g106.y));
				float2 texCoord34_g106 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float4 appendResult39_g106 = (float4(texCoord29_g106.z , texCoord29_g106.w , texCoord34_g106.x , texCoord34_g106.y));
				float4 break30_g106 = ( _Custom12_Diosslve_V == 0.0 ? appendResult38_g106 : appendResult39_g106 );
				float ifLocalVar20_g106 = 0;
				UNITY_BRANCH 
				if( 2.0 > temp_output_25_0_g106 )
				ifLocalVar20_g106 = ( temp_output_25_0_g106 == 0.0 ? break30_g106.x : break30_g106.y );
				else if( 2.0 == temp_output_25_0_g106 )
				ifLocalVar20_g106 = break30_g106.z;
				else if( 2.0 < temp_output_25_0_g106 )
				ifLocalVar20_g106 = ( temp_output_25_0_g106 == 4.0 ? 0.0 : break30_g106.w );
				float2 appendResult129 = (float2(( break125.x + ifLocalVar20_g100 ) , ( break125.y + ifLocalVar20_g106 )));
				#ifdef _CUSTOM_UV_DISSLOVE_ON
				float2 staticSwitch130 = appendResult129;
				#else
				float2 staticSwitch130 = appendResult128;
				#endif
				float2 Disslove_UV135 = staticSwitch130;
				#ifdef _DISSLOVE_NOISE_ON
				float2 staticSwitch200 = ( Disslove_UV135 + Noise_A178 );
				#else
				float2 staticSwitch200 = Disslove_UV135;
				#endif
				float2 panner44_g120 = ( 1.0 * _Time.y * appendResult49_g120 + staticSwitch200);
				float cos55_g120 = cos( ( ( ( _Rotator_Disslove / 360.0 ) * PI ) * 2.0 ) );
				float sin55_g120 = sin( ( ( ( _Rotator_Disslove / 360.0 ) * PI ) * 2.0 ) );
				float2 rotator55_g120 = mul( panner44_g120 - float2( 0.5,0.5 ) , float2x2( cos55_g120 , -sin55_g120 , sin55_g120 , cos55_g120 )) + float2( 0.5,0.5 );
				float2 break52_g120 = rotator55_g120;
				float2 break54_g120 = rotator55_g120;
				float clampResult60_g120 = clamp( break54_g120.x , 0.0 , 1.0 );
				float clampResult50_g120 = clamp( break54_g120.y , 0.0 , 1.0 );
				float2 appendResult53_g120 = (float2(( (float)(int)_U_Clamp_Disslove == 0.0 ? break52_g120.x : clampResult60_g120 ) , ( (float)(int)_V_Clamp_Disslove == 0.0 ? break52_g120.y : clampResult50_g120 )));
				float4 tex2DNode27_g120 = tex2D( _Disslove_Texture, appendResult53_g120 );
				float Disslove_Tex138 = tex2DNode27_g120.r;
				float temp_output_57_0_g121 = saturate( pow( Disslove_Tex138 , _Disslove_Tex_Power ) );
				float temp_output_4_0_g121 = saturate( ( ( ( temp_output_57_0_g121 / 1.0 ) + ( 0.0 == 0.0 ? temp_output_57_0_g121 : 1.0 ) ) / 2.0 ) );
				float smoothstepResult21_g121 = smoothstep( ( temp_output_2_0_g121 - temp_output_51_0_g121 ) , temp_output_2_0_g121 , temp_output_4_0_g121);
				float SoftA144 = smoothstepResult21_g121;
				float temp_output_44_0_g121 = _Wide_Disslove;
				float temp_output_3_0_g121 = ( temp_output_53_0_g121 * ( 1.0 + temp_output_44_0_g121 ) );
				float StingA145 = step( ( temp_output_3_0_g121 - temp_output_44_0_g121 ) , temp_output_4_0_g121 );
				#ifdef _DISSLOVE_SOFTSTING_ON
				float staticSwitch157 = StingA145;
				#else
				float staticSwitch157 = SoftA144;
				#endif
				float Disslove_A166 = staticSwitch157;
				float temp_output_82_0 = ( IN.ase_color.a * Main_AR75 * _Color.a * Mask_R108 * Disslove_A166 );
				float4 screenPos = IN.ase_texcoord5;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth202 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth202 = abs( ( screenDepth202 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _Depth_Fade_Distance ) );
				#ifdef _DEPTH_FADE_ON
				float staticSwitch203 = ( temp_output_82_0 * saturate( pow( distanceDepth202 , abs( 2.0 ) ) ) );
				#else
				float staticSwitch203 = temp_output_82_0;
				#endif
				
				float Alpha = staticSwitch203;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif
				return 0;
			}
			ENDHLSL
		}

	
	}
	
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18935
769;294;1529;1085;2210.395;-3480.91;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;136;-3093.471,2867.523;Inherit;False;1638.18;583.0864;Disslove;15;121;122;124;125;128;130;123;131;133;132;134;127;126;129;135;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;201;-2953.368,4278.7;Inherit;False;1777.706;636.79;Noise;14;182;176;183;181;184;185;177;191;187;190;186;189;188;178;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;121;-3043.471,2917.523;Inherit;False;Property;_Tex_TS_Disslove;Tex_TS_Disslove;33;0;Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;131;-3039.117,3094.37;Inherit;False;Property;_Custom12_Diosslve_U;Custom1/2_Diosslve_U;40;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;133;-3039.253,3171.975;Inherit;False;Property;_xyzw_Disslove_U;x/y/z/w_Disslove_U;41;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;182;-2903.368,4524.413;Inherit;False;Property;_Tex_TS_Noise;Tex_TS_Noise;55;0;Create;True;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;122;-2730.998,2921.787;Inherit;False;PandaUVSwitch2;-1;;76;6911afe0fd32ecb4b94c113ee4f145b9;0;3;16;FLOAT4;0,0,0,0;False;20;FLOAT2;0,0;False;22;FLOAT2;0,0;False;3;FLOAT2;0;FLOAT2;14;FLOAT2;15
Node;AmplifyShaderEditor.RangedFloatNode;132;-3036.384,3257.969;Inherit;False;Property;_Custom12_Diosslve_V;Custom1/2_Diosslve_V;42;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;134;-3035.452,3334.61;Inherit;False;Property;_xyzw_Disslove_V;x/y/z/w_Disslove_V;43;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;183;-2555.632,4648.425;Inherit;False;Property;_U_Speed_Noise;U_Speed_Noise;57;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;181;-2620.691,4526.413;Inherit;False;PandaUVSwitch2;-1;;107;6911afe0fd32ecb4b94c113ee4f145b9;0;3;16;FLOAT4;0,0,0,0;False;20;FLOAT2;0,0;False;22;FLOAT2;0,0;False;3;FLOAT2;0;FLOAT2;14;FLOAT2;15
Node;AmplifyShaderEditor.RangedFloatNode;184;-2550.632,4721.425;Inherit;False;Property;_V_Speed_Noise;V_Speed_Noise;56;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;124;-2741.081,3263.513;Inherit;False;CustomSwitch2;-1;;106;b07fd8766bfae0940a797ebfc2ed9309;0;2;33;FLOAT;0;False;25;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;125;-2370.417,2936.684;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;185;-2555.028,4799.49;Inherit;False;Property;_Rotator_Noise;Rotator_Noise;58;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;123;-2734.412,3098.671;Inherit;False;CustomSwitch2;-1;;100;b07fd8766bfae0940a797ebfc2ed9309;0;2;33;FLOAT;0;False;25;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;176;-2604.339,4328.7;Inherit;True;Property;_Noise_Texture;Noise_Texture;54;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleAddOpNode;127;-2385.108,3234.492;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;187;-1962.539,4497.257;Inherit;False;Property;_Noise_Intensity;Noise_Intensity;59;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;126;-2313.179,3113.02;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;191;-2118.567,4764.869;Inherit;False;Property;_xyzw_Noise_Intensity;x/y/z/w_Noise_Intensity;62;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;190;-2110.127,4669.916;Inherit;False;Property;_Custom12_Noise_Intensity;Custom1/2_Noise_Intensity;61;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;177;-2203.178,4378.36;Inherit;False;PandaTex2;-1;;109;0608b7ab181f5b8408213912f15b169b;0;8;28;SAMPLER2D;0;False;30;INT;0;False;33;INT;0;False;38;INT;0;False;22;FLOAT2;0,0;False;20;FLOAT;0;False;21;FLOAT;0;False;19;FLOAT;0;False;4;COLOR;31;FLOAT;34;FLOAT;36;FLOAT;39
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;186;-1755.887,4403.008;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;189;-1858.867,4700.919;Inherit;False;CustomSwitch2;-1;;110;b07fd8766bfae0940a797ebfc2ed9309;0;2;33;FLOAT;0;False;25;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;129;-2086.932,3209.837;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;128;-2142.984,2941.25;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;78;-3069.492,-428.7294;Inherit;False;1577.305;842.6222;Main_UV;17;18;68;66;62;61;14;34;64;67;16;17;70;69;71;65;63;72;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;104;-3105.087,1261.77;Inherit;False;1604.151;839.6219;Mask_UV;17;90;92;93;96;97;98;99;100;101;102;95;91;88;87;89;94;103;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;130;-1954.674,2946.665;Inherit;False;Property;_Custom_UV_Disslove;Custom_UV_Disslove;39;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;188;-1584.744,4554.353;Inherit;False;Property;_Custom_Noise_Intensity;Custom_Noise_Intensity;60;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;18;-3016.192,-378.7294;Inherit;False;Property;_Tex_ST_Main;Tex_ST_Main;7;0;Create;True;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;102;-3051.787,1311.77;Inherit;False;Property;_Tex_ST_Mask;Tex_ST_Mask;21;0;Create;True;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;87;-3051.241,1911.392;Inherit;False;Property;_Custom12_Mask_V;Custom1/2_Mask_V;30;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;135;-1679.29,2955.578;Inherit;False;Disslove_UV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;14;-2763.143,-272.217;Inherit;False;PandaUVSwitch2;-1;;113;6911afe0fd32ecb4b94c113ee4f145b9;0;3;16;FLOAT4;0,0,0,0;False;20;FLOAT2;0,0;False;22;FLOAT2;0,0;False;3;FLOAT2;0;FLOAT2;14;FLOAT2;15
Node;AmplifyShaderEditor.RangedFloatNode;61;-3006.979,58.0497;Inherit;False;Property;_Custom12_Main_U;Custom1/2_Main_U;16;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;90;-2798.738,1418.282;Inherit;False;PandaUVSwitch2;-1;;111;6911afe0fd32ecb4b94c113ee4f145b9;0;3;16;FLOAT4;0,0,0,0;False;20;FLOAT2;0,0;False;22;FLOAT2;0,0;False;3;FLOAT2;0;FLOAT2;14;FLOAT2;15
Node;AmplifyShaderEditor.RangedFloatNode;88;-3043.574,1823.549;Inherit;False;Property;_xyzw_Mask_U;x/y/z/w_Mask_U;29;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-3012.646,297.8929;Inherit;False;Property;_xyzw_Main_V;x/y/z/w_Main_V;19;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-3015.646,220.8927;Inherit;False;Property;_Custom12_Main_V;Custom1/2_Main_V;18;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-3050.241,1985.391;Inherit;False;Property;_xyzw_Mask_V;x/y/z/w_Mask_V;31;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;149;-3102.5,3477.15;Inherit;False;1349.369;762.5176;Disslove_Tex;12;138;118;137;141;139;143;117;140;142;198;199;200;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;91;-3042.574,1748.549;Inherit;False;Property;_Custom12_Mask_U;Custom1/2_Mask_U;28;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;178;-1399.662,4394.28;Inherit;False;Noise_A;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-3005.979,133.0495;Inherit;False;Property;_xyzw_Main_U;x/y/z/w_Main_U;17;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;67;-2775.546,226.6208;Inherit;False;CustomSwitch2;-1;;116;b07fd8766bfae0940a797ebfc2ed9309;0;2;33;FLOAT;0;False;25;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;198;-3084.025,3897.669;Inherit;False;178;Noise_A;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;98;-2804.472,1752.277;Inherit;False;CustomSwitch2;-1;;117;b07fd8766bfae0940a797ebfc2ed9309;0;2;33;FLOAT;0;False;25;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;34;-2768.877,61.7776;Inherit;False;CustomSwitch2;-1;;118;b07fd8766bfae0940a797ebfc2ed9309;0;2;33;FLOAT;0;False;25;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;137;-3084.966,3741.981;Inherit;False;135;Disslove_UV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;92;-2491.44,1414.108;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;64;-2455.845,-276.3918;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FunctionNode;97;-2811.141,1917.12;Inherit;False;CustomSwitch2;-1;;119;b07fd8766bfae0940a797ebfc2ed9309;0;2;33;FLOAT;0;False;25;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;199;-2889.434,3880.274;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;100;-2408.627,1896.207;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;69;-2386.161,39.34502;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;99;-2421.756,1729.844;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;70;-2373.032,205.707;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;101;-2179.937,1727.427;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;139;-2653.84,3719.301;Inherit;False;Property;_U_Clamp_Disslove;U_Clamp_Disslove;34;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;200;-2859.304,3745.382;Inherit;False;Property;_Disslove_Noise;Disslove_Noise;53;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;93;-2264.008,1418.674;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;140;-2651.939,3792.806;Inherit;False;Property;_V_Clamp_Disslove;V_Clamp_Disslove;35;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;143;-2641.495,4084.758;Inherit;False;Property;_Rotator_Disslove;Rotator_Disslove;38;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;71;-2144.341,36.92754;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;142;-2641.908,4005.864;Inherit;False;Property;_V_Speed_Disslove;V_Speed_Disslove;37;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;117;-2659.86,3527.15;Inherit;True;Property;_Disslove_Texture;Disslove_Texture;32;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.DynamicAppendNode;65;-2228.413,-271.8257;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;141;-2649.732,3933.919;Inherit;False;Property;_U_Speed_Disslove;U_Speed_Disslove;36;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;174;-1612.459,3599.848;Inherit;False;892.1874;624.1919;Comment;8;150;154;151;153;152;144;145;146;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;63;-1966.02,-278.6943;Inherit;False;Property;_Custom_Main;Custom_Main;15;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;94;-1999.615,1405.805;Inherit;False;Property;_Custom_Mask;Custom_Mask;27;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;118;-2245.794,3648.564;Inherit;False;PandaTex2;-1;;120;0608b7ab181f5b8408213912f15b169b;0;8;28;SAMPLER2D;0;False;30;INT;0;False;33;INT;0;False;38;INT;0;False;22;FLOAT2;0,0;False;20;FLOAT;0;False;21;FLOAT;0;False;19;FLOAT;0;False;4;COLOR;31;FLOAT;34;FLOAT;36;FLOAT;39
Node;AmplifyShaderEditor.RangedFloatNode;153;-1514.258,4014.09;Inherit;False;Property;_Soft_Disslove;Soft_Disslove;47;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;72;-1716.188,-289.4415;Inherit;False;Main_UV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;152;-1543.206,3931.87;Inherit;False;Property;_Disslove_Factor;Disslove_Factor;45;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;79;-3182.641,423.8155;Inherit;False;1695.067;821.6796;Main_Texture;16;42;40;74;75;31;54;50;73;60;56;58;59;57;192;193;194;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;150;-1562.459,3854.747;Inherit;False;Property;_Disslove_Tex_Power;Disslove_Tex_Power;44;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;115;-3096.505,2122.364;Inherit;False;1306.226;741.2112;Mask_Texture;12;105;108;86;111;112;85;114;113;110;195;196;197;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;138;-1977.131,3665.44;Inherit;False;Disslove_Tex;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;154;-1559.427,4108.04;Inherit;False;Property;_Wide_Disslove;Wide_Disslove;49;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;151;-1537.735,3777.266;Inherit;False;Property;_Custom_DissloveCustom2_x;Custom_Disslove(Custom2_x);46;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;103;-1724.936,1407.822;Inherit;False;Mask_UV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;105;-3085.042,2264.633;Inherit;False;103;Mask_UV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;192;-3185.658,816.6299;Inherit;False;178;Noise_A;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;175;-1249.804,3675.674;Inherit;False;PandaDis2;-1;;121;48aee430982695446b7a145521c90377;0;10;49;FLOAT;1;False;48;FLOAT;1;False;52;FLOAT;0;False;54;FLOAT;0;False;45;FLOAT;0;False;47;FLOAT;1;False;44;FLOAT;0;False;46;FLOAT;1;False;50;FLOAT;0;False;51;FLOAT;0;False;3;FLOAT;0;FLOAT;43;FLOAT;56
Node;AmplifyShaderEditor.GetLocalVarNode;73;-3189.829,630.5568;Inherit;False;72;Main_UV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;195;-3071.01,2435.966;Inherit;False;178;Noise_A;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;196;-2876.419,2418.571;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;173;-640.5925,3433.607;Inherit;False;918.2754;777.6426;Comment;9;147;158;157;166;161;156;163;159;160;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;144;-949.2712,3649.848;Inherit;False;SoftA;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;194;-2991.067,799.2352;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;145;-944.2712,3733.848;Inherit;False;StingA;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;-2664.505,2727.575;Inherit;False;Property;_Rotator_Mask;Rotator_Mask;26;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-2657.308,659.783;Inherit;False;Property;_U_Clamp_Main;U_Clamp_Main;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;147;-590.5925,3490.116;Inherit;False;144;SoftA;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-2570.923,2580.935;Inherit;False;Property;_U_Speed_Mask;U_Speed_Mask;24;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;193;-2876.067,632.2352;Inherit;False;Property;_Main_Noise;Main_Noise;51;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;158;-582.6517,3616.203;Inherit;True;145;StingA;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-2648.37,1021.693;Inherit;False;Property;_V_Speed_Main;V_Speed_Main;12;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-2654.308,728.783;Inherit;False;Property;_V_Clamp_Main;V_Clamp_Main;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-2748.859,1098.545;Inherit;False;Property;_Rotator_Main;Rotator_Main;13;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-2654.308,798.783;Inherit;False;Property;_AR_Main;A/R_Main;10;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;197;-2817.385,2267.239;Inherit;False;Property;_Mask_Noise;Mask_Noise;52;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;113;-2566.923,2654.935;Inherit;False;Property;_V_Speed_Mask;V_Speed_Mask;25;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;110;-2592.665,2365.806;Inherit;False;Property;_U_Clamp_Mask;U_Clamp_Mask;22;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-2591.665,2429.806;Inherit;False;Property;_V_Clamp_Mask;V_Clamp_Mask;23;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;50;-2665.544,473.8155;Inherit;True;Property;_Main_Texture;Main_Texture;6;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TexturePropertyNode;85;-2621.141,2172.364;Inherit;True;Property;_Mask_Texture;Mask_Texture;20;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;59;-2652.006,944.3878;Inherit;False;Property;_U_Speed_Main;U_Speed_Main;11;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;157;-315.7542,3487.473;Inherit;False;Property;_Disslove_SoftSting;Disslove_Soft/Sting;48;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;205;-695.277,1124.071;Inherit;False;Property;_Depth_Fade_Distance;Depth_Fade_Distance;64;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;86;-2241.634,2174.827;Inherit;False;PandaTex2;-1;;123;0608b7ab181f5b8408213912f15b169b;0;8;28;SAMPLER2D;0;False;30;INT;0;False;33;INT;0;False;38;INT;0;False;22;FLOAT2;0,0;False;20;FLOAT;0;False;21;FLOAT;0;False;19;FLOAT;0;False;4;COLOR;31;FLOAT;34;FLOAT;36;FLOAT;39
Node;AmplifyShaderEditor.RangedFloatNode;208;-353.9424,1211.616;Inherit;False;Constant;_Float4;Float 4;63;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;31;-2298.687,532.7393;Inherit;False;PandaTex2;-1;;122;0608b7ab181f5b8408213912f15b169b;0;8;28;SAMPLER2D;0;False;30;INT;0;False;33;INT;0;False;38;INT;0;False;22;FLOAT2;0,0;False;20;FLOAT;0;False;21;FLOAT;0;False;19;FLOAT;0;False;4;COLOR;31;FLOAT;34;FLOAT;36;FLOAT;39
Node;AmplifyShaderEditor.RegisterLocalVarNode;166;-18.55079,3483.607;Inherit;False;Disslove_A;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;75;-1711.574,611.1704;Inherit;False;Main_AR;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;202;-452.0111,1105.819;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;210;-203.6226,1219.904;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;108;-2014.278,2194.806;Inherit;False;Mask_R;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;77;-474.1842,815.9544;Inherit;False;75;Main_AR;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;80;-437.5185,434.8552;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;84;-640.7612,432.4229;Inherit;False;Property;_Color;Color;5;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;167;-468.0584,932.9281;Inherit;False;166;Disslove_A;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;207;-73.94244,1170.616;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;109;-475.5585,698.3718;Inherit;False;108;Mask_R;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-85.88603,837.0698;Inherit;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;206;48.36645,1062.067;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;13;680.4169,-166.1904;Inherit;False;255;685;基础;7;5;8;212;211;6;7;9;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;204;159.0788,953.4288;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;727.0737,191.4666;Inherit;False;Property;_RGB_Dst;RGB_Dst;4;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.BlendMode;True;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;170;-112.0856,585.1337;Inherit;False;163;Disslove_Wide_Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;211;727.4489,261.0074;Inherit;False;Constant;_Alpha_Src;Alpha_Src;5;0;Create;True;0;0;1;UnityEngine.Rendering.BlendMode;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;719.4169,-38.19022;Inherit;False;Property;_ZWrite_Mode;ZWrite_Mode;1;1;[Enum];Create;True;0;3;Default;0;On;1;Off;2;0;True;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;95;-3055.087,1480.27;Inherit;False;Constant;_Polar_Center_Mask;Polar_Center_Mask;10;0;Create;True;0;0;0;False;0;False;0.5,0.5;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;172;131.9068,581.4635;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;6;729.4169,114.8096;Inherit;False;Property;_RGB_Src;RGB_Src;3;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.BlendMode;True;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;146;-946.2712,3820.848;Inherit;False;StingA1;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;169;387.626,434.518;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;39;-694.0614,-665.4959;Inherit;False;PandaNormalTex2;-1;;126;2dd4095dbdb427f48bbdb0ff00eed726;0;8;28;SAMPLER2D;_Sampler2839;False;40;FLOAT;0;False;46;INT;0;False;41;INT;0;False;44;FLOAT2;0,0;False;43;FLOAT;0;False;47;FLOAT;0;False;48;FLOAT;0;False;1;FLOAT3;31
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;160;-155.4088,3780.63;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;203;336.1594,841.0057;Inherit;False;Property;_Depth_Fade;Depth_Fade;63;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;726.4169,-116.1904;Inherit;False;Property;_Cull_Mode;Cull_Mode;0;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;17;-3017.192,-82.72899;Inherit;False;Constant;_Tong_UV_Main;Tong_UV_Main;11;0;Create;True;0;0;0;False;0;False;0.5,0.5;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;156;-580.39,3803.114;Inherit;True;146;StingA1;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;96;-3052.787,1607.771;Inherit;False;Constant;_Tong_UV_Mask;Tong_UV_Mask;11;0;Create;True;0;0;0;False;0;False;0.5,0.5;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;37;-700.129,-821.7222;Inherit;False;PandaParallax2;-1;;124;464bab118ae5fd8409031d087d8b0935;0;3;67;SAMPLER2D;0;False;63;FLOAT2;0,0;False;65;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;212;730.4489,337.0074;Inherit;False;Constant;_Alpha_Dst;Alpha_Dst;6;0;Create;True;0;0;1;UnityEngine.Rendering.BlendMode;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;163;20.68287,3774.71;Inherit;False;Disslove_Wide_Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;74;-1718.125,511.5316;Inherit;False;Main_Color;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;42;-2188.244,785.9662;Inherit;False;Property;_Refine_Main;Refine_Main;14;0;Create;True;0;0;0;False;0;False;1,1,2,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;719.417,30.80988;Inherit;False;Property;_ZTest_Mode;ZTest_Mode;2;1;[Enum];Create;True;0;6;Less_or_Equal;4;Greater_or_Equal;5;Always;8;Option4;3;Option5;4;Option6;5;1;UnityEngine.Rendering.CompareFunction;True;0;False;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;16;-3019.492,-210.2291;Inherit;False;Constant;_Polar_Center_Main;Polar_Center_Main;10;0;Create;True;0;0;0;False;0;False;0.5,0.5;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;159;-336.7819,3783.282;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;161;-575.1372,3999.25;Inherit;False;Property;_Color_Wide;Color_Wide;50;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;40;-2009.932,536.583;Inherit;False;PandaRefine2;-1;;125;de950ebac85405948aaba90935ff359f;0;2;9;FLOAT3;0,0,0;False;6;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-102.0101,244.6188;Inherit;False;5;5;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;76;-454.5171,249.2415;Inherit;False;74;Main_Color;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;249;789.9405,689.3557;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;248;789.9405,689.3557;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;247;789.9405,689.3557;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;246;789.9405,689.3557;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;Xxs/TY_1.0;2992e84f91cbeb14eab234972e07ea9d;True;Forward;0;1;Forward;8;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;0;True;5;False;False;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;True;True;2;5;True;6;10;True;7;1;1;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;1;True;8;True;3;True;9;True;True;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;False;0;Hidden/InternalErrorShader;0;0;Standard;22;Surface;1;637986492656330682;  Blend;0;637986493271357972;Two Sided;1;0;Cast Shadows;1;0;  Use Shadow Threshold;0;0;Receive Shadows;1;0;GPU Instancing;1;0;LOD CrossFade;0;0;Built-in Fog;0;0;DOTS Instancing;0;0;Meta Pass;0;0;Extra Pre Pass;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,-1;0;  Type;0;0;  Tess;16,False,-1;0;  Min;10,False,-1;0;  Max;25,False,-1;0;  Edge Length;16,False,-1;0;  Max Displacement;25,False,-1;0;Vertex Position,InvertActionOnDeselection;1;0;0;5;False;True;True;True;False;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;245;789.9405,689.3557;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;True;17;d3d9;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;nomrt;0;False;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;122;16;121;0
WireConnection;181;16;182;0
WireConnection;124;33;132;0
WireConnection;124;25;134;0
WireConnection;125;0;122;0
WireConnection;123;33;131;0
WireConnection;123;25;133;0
WireConnection;127;0;125;1
WireConnection;127;1;124;0
WireConnection;126;0;125;0
WireConnection;126;1;123;0
WireConnection;177;28;176;0
WireConnection;177;22;181;0
WireConnection;177;20;183;0
WireConnection;177;21;184;0
WireConnection;177;19;185;0
WireConnection;186;0;177;34
WireConnection;186;1;187;0
WireConnection;189;33;190;0
WireConnection;189;25;191;0
WireConnection;129;0;126;0
WireConnection;129;1;127;0
WireConnection;128;0;125;0
WireConnection;128;1;125;1
WireConnection;130;1;128;0
WireConnection;130;0;129;0
WireConnection;188;1;186;0
WireConnection;188;0;189;0
WireConnection;135;0;130;0
WireConnection;14;16;18;0
WireConnection;90;16;102;0
WireConnection;178;0;188;0
WireConnection;67;33;66;0
WireConnection;67;25;68;0
WireConnection;98;33;91;0
WireConnection;98;25;88;0
WireConnection;34;33;61;0
WireConnection;34;25;62;0
WireConnection;92;0;90;0
WireConnection;64;0;14;0
WireConnection;97;33;87;0
WireConnection;97;25;89;0
WireConnection;199;0;137;0
WireConnection;199;1;198;0
WireConnection;100;0;92;1
WireConnection;100;1;97;0
WireConnection;69;0;64;0
WireConnection;69;1;34;0
WireConnection;99;0;92;0
WireConnection;99;1;98;0
WireConnection;70;0;64;1
WireConnection;70;1;67;0
WireConnection;101;0;99;0
WireConnection;101;1;100;0
WireConnection;200;1;137;0
WireConnection;200;0;199;0
WireConnection;93;0;92;0
WireConnection;93;1;92;1
WireConnection;71;0;69;0
WireConnection;71;1;70;0
WireConnection;65;0;64;0
WireConnection;65;1;64;1
WireConnection;63;1;65;0
WireConnection;63;0;71;0
WireConnection;94;1;93;0
WireConnection;94;0;101;0
WireConnection;118;28;117;0
WireConnection;118;30;139;0
WireConnection;118;33;140;0
WireConnection;118;22;200;0
WireConnection;118;20;141;0
WireConnection;118;21;142;0
WireConnection;118;19;143;0
WireConnection;72;0;63;0
WireConnection;138;0;118;34
WireConnection;103;0;94;0
WireConnection;175;49;138;0
WireConnection;175;54;151;0
WireConnection;175;47;150;0
WireConnection;175;44;154;0
WireConnection;175;50;152;0
WireConnection;175;51;153;0
WireConnection;196;0;105;0
WireConnection;196;1;195;0
WireConnection;144;0;175;0
WireConnection;194;0;73;0
WireConnection;194;1;192;0
WireConnection;145;0;175;43
WireConnection;193;1;73;0
WireConnection;193;0;194;0
WireConnection;197;1;105;0
WireConnection;197;0;196;0
WireConnection;157;1;147;0
WireConnection;157;0;158;0
WireConnection;86;28;85;0
WireConnection;86;30;110;0
WireConnection;86;33;111;0
WireConnection;86;22;197;0
WireConnection;86;20;112;0
WireConnection;86;21;113;0
WireConnection;86;19;114;0
WireConnection;31;28;50;0
WireConnection;31;30;56;0
WireConnection;31;33;57;0
WireConnection;31;38;58;0
WireConnection;31;22;193;0
WireConnection;31;20;59;0
WireConnection;31;21;60;0
WireConnection;31;19;54;0
WireConnection;166;0;157;0
WireConnection;75;0;31;39
WireConnection;202;0;205;0
WireConnection;210;0;208;0
WireConnection;108;0;86;34
WireConnection;207;0;202;0
WireConnection;207;1;210;0
WireConnection;82;0;80;4
WireConnection;82;1;77;0
WireConnection;82;2;84;4
WireConnection;82;3;109;0
WireConnection;82;4;167;0
WireConnection;206;0;207;0
WireConnection;204;0;82;0
WireConnection;204;1;206;0
WireConnection;172;0;170;0
WireConnection;172;1;82;0
WireConnection;146;0;175;56
WireConnection;169;0;81;0
WireConnection;169;1;172;0
WireConnection;160;0;159;0
WireConnection;160;1;161;0
WireConnection;203;1;82;0
WireConnection;203;0;204;0
WireConnection;163;0;160;0
WireConnection;74;0;40;0
WireConnection;159;0;158;0
WireConnection;159;1;156;0
WireConnection;40;9;31;31
WireConnection;40;6;42;0
WireConnection;81;0;76;0
WireConnection;81;1;80;0
WireConnection;81;2;84;0
WireConnection;81;3;109;0
WireConnection;81;4;167;0
WireConnection;246;2;169;0
WireConnection;246;3;203;0
ASEEND*/
//CHKSM=0707EE5A259B8AC7F85616493103079921620F18
Shader "Unlit/rain"
{
	Properties
	{
		_CubeTex("Cube Tex", Cube) = ""{}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 reflectionDir : TEXCOORD0;
			};

			uniform samplerCUBE _CubeTex;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				float3 worldViewDir = WorldSpaceViewDir(v.vertex);
				o.reflectionDir = reflect(-worldViewDir, worldNormal);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = texCUBE(_CubeTex, i.reflectionDir);
				return col;
			}
			ENDCG
		}
	}
}
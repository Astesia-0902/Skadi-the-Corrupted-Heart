Shader "Unlit/sea"
{
    Properties
    {
      _Color01("Water Color01",color) = (1,1,1,1)
      _Color02("Water Color02",color) = (1,1,1,1)
      _Speed("Speed",range(0,5)) = 1
        
      [Header(Normal)]
      _Distort("Distort",range(0,1)) = 1
      _NormalTex("NormalTex",2D) = "white"{}
        
      [Header(Specular)]
      _SpecularColor("Specular Color",color) = (1,1,1,1)  
      _Specular("Specular",float) = 1
      _Smoothness("Smoothness",float) = 1
        
      [Header(Reflection)]
      _ReflectionTex("ReflectionTex",Cube) = "white"{}
        
      [Header(Caustic)]
     _CausticTex("CausticTex",2D) = "white"{}

      [Header(Foam)]
      _FoamTex("FoamTex",2D) = "white"{}
      _FoamColor("Foam Color",color) = (1,1,1,1)
      _FoamRange("FoamRange",range(0,5)) = 1
    }
   
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"//需要加一个URP管线
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        Pass
        {
            Name "Pass"
            Tags
            {

            }

            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite On
            HLSLPROGRAM

            // Pragmas
            #pragma target 2.0
            #pragma only_renderers gles gles3 glcore d3d11//只针对这几个平台
            #pragma multi_compile_instancing
            #pragma multi_compile_fog
            #pragma vertex vert
            #pragma fragment frag

            #define SHADERPASS SHADERPASS_UNLIT
            // Includes需要引入的文件，原来是include.cignc;
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing
            //顶点着色器输入部分
                struct Attributes
            {
                float4 positionOS : POSITION;
                float4 uv:TEXCOORD0;//.xy =foam
                float3 normalOS:TEXCOORD1;
            };
                //顶点着色器的输出部分
                struct Varyings
            {
                float4 positionCS : SV_POSITION;
                    float4 uv :TEXCOORD0;
                    float3 positionVS :TEXCOORD1;
                    float3 positionWS :TEXCOORD2;
                    float3 normalWS :TEXCOORD3;
                    float4 NormalUV : TEXCOORD4;
                    
            };
             CBUFFER_START(UnityPerMaterial)
            half4  _Color01;
            half _Speed;
            half4  _Color02;
            float4 _FoamTex_ST;
            half4 _SpecularColor;
            half _Specular,_Smoothness;
            half4 _FoamColor;
            half _FoamRange;
            half _Distort;
            float4 _NormalTex_ST;
            float4 _CausticTex_ST;
            CBUFFER_END
            TEXTURE2D(_FoamTex);SAMPLER(sampler_FoamTex);
            TEXTURECUBE(_ReflectionTex);SAMPLER(sampler_ReflectionTex);
            TEXTURE2D(_NormalTex);SAMPLER(sampler_NormalTex);
            TEXTURE2D(_CausticTex);SAMPLER(sampler_CausticTex);
            TEXTURE2D (_CameraDepthTexture);SAMPLER(sampler_CameraDepthTexture);
                       //v2f vert(appdata v)
            //顶点着色器 ，Attributes就相当于appdata
            Varyings vert(Attributes v)
            {
                Varyings o = (Varyings)0;
                                
                o.positionWS = TransformObjectToWorld(v.positionOS);
                o.positionVS = TransformWorldToView(o.positionWS);
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);

                float offset = _Time.y*_Speed; 
                
                o.uv.xy = o.positionWS.xz+offset;
                o.NormalUV.xy = TRANSFORM_TEX(v.uv,_NormalTex)+offset;
                o.NormalUV.zw = TRANSFORM_TEX(v.uv,_NormalTex)+offset*float2(-1.07,1.13);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                return o;
            }
         
            //片断着色器
            half4 frag(Varyings i) : SV_TARGET 
            {
                float2 screenUV = i.positionCS.xy/_ScreenParams.xy;
                //水的深度
                half depthTex =SAMPLE_TEXTURE2D(_CameraDepthTexture,sampler_CameraDepthTexture,screenUV);
                half depthScene = LinearEyeDepth(depthTex,_ZBufferParams);
                half depthWater= depthScene+i.positionVS.z;
                

                //水的颜色
                half4 waterColor = lerp(_Color01,_Color02,depthWater);
                
                
                //水的焦散
                float4 depthVS = 1;
                depthVS.xy = i.positionVS.xy *depthScene/-i.positionVS.z;
                depthVS.z= depthScene;
                float3 depthWS = mul(unity_CameraToWorld,depthVS);
                float2 causticUV01 = depthWS.xz*_CausticTex_ST.xy+depthWS.y*0.2 +_Time.y*_Speed;
                float2 causticUV02 = depthWS.xz*_CausticTex_ST.xy +depthWS.y*0.1+_Time.y*_Speed;
                half4 causticTex01 = SAMPLE_TEXTURE2D(_CausticTex,sampler_CausticTex,causticUV01);
                half4 causticTex02 = SAMPLE_TEXTURE2D(_CausticTex,sampler_CausticTex,causticUV02*float2(-1.07,1.43));
                half4 causticTex = causticTex01*causticTex02;
                half4 caustic = min(causticTex01,causticTex02);
                
                //水的扭曲
                half4 NormalTex01 = SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex,i.NormalUV.xy);
                half4 NormalTex02 = SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex,i.NormalUV.zw);
                half4 NormalTex = NormalTex01 *NormalTex02;

                float2 distortUV = lerp(screenUV,NormalTex,_Distort);
                half4 opaqueTex = SAMPLE_TEXTURE2D(_CameraDepthTexture,sampler_CameraDepthTexture,distortUV);

                //水的高光
                //blinnphong
                half3 N = lerp(normalize(i.normalWS),NormalTex,_Distort);
                 N = NormalTex;
                Light Light =  GetMainLight();
                half3 L =Light.direction;
                half V = normalize(_WorldSpaceCameraPos.xyz-i.positionWS.xyz);
                half3 H = normalize(L+V);
                half NdotH = dot(N,H);
                half4 specular = _SpecularColor *_Specular*pow(saturate(dot(N,H)),_Smoothness);

                //水的反射
                
                half3 reflectionUV = reflect(-V,N);
                half4 reflectionTex =SAMPLE_TEXTURECUBE(_ReflectionTex,sampler_ReflectionTex,reflectionUV);
                half fresnel = pow(1-saturate(dot(N,V)),3);
                half4 reflection = reflectionTex * fresnel;
                
                //水的泡沫
                half foamRange = depthWater *_FoamRange;
                half foamTex = SAMPLE_TEXTURE2D(_FoamTex,sampler_FoamTex,i.uv.xy);
                half foamMask = step(foamRange,foamTex);
                half4 foam = foamMask *_FoamColor;
                half4 c;
                c =caustic+ foam +specular*0.1+waterColor*opaqueTex;
                c.a=0.5;


                return c;
            }

            ENDHLSL
        }
        
    }
    FallBack "Hidden/Shader Graph/FallbackError"

}

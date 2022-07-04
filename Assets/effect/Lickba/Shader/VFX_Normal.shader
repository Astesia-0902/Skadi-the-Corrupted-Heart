Shader "Lickba/VFX_Normal"
{
    Properties
    {
        //基础设置
        [Enum(Normal,0,Fire,1,Outline,2)]_Models ("模式选择",int) = 0
        [Enum(UnityEngine.Rendering.BlendMode)]_BlendModeSrc("混合模式Src",Int) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]_BlendModeDst("混合模式Dst",Int) = 10
        [Enum(UnityEngine.Rendering.CullMode)]_CullMode("剔除模式",Int) = 2
        [Toggle(_DoubFace)]_DoubFace("双面开关",int) = 0
        [Enum(Off,0,On,1)]_Zwrite("深度写入",float) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)]_Ztest("Ztest", Float) = 4
        //[Toggle(_ENABLE_FOG_TRANS)]_enableFog("启用场景雾",int) = 0
        [HideInInspector]_BlendTemp("BlendTemp",float) = 0
        _AlphaClip("AlphaClip", Range( 0, 1 )) = 0
        _MainAlpha("MainAlpha", Range( 0 , 10)) = 1
        
        //主纹理
        [HDR]_Color0 ("Base Color", Color) = (1,1,1,1)
        [HDR]_BackColor ("Base Color", Color) = (0,0,0,1)
        _Main_Tex ("MainTex", 2D) = "white" {}
        _Main_u ("Mtex_U_Speed", Float) = 0
        _Main_v ("MTex_V_Speed", Float) = 0
        [Toggle]_CustomdataMainTexUV("CustomdataMainTexUV", int) = 0
        
        //遮罩
        [Toggle(_Mask_ON)]_Mask_ON("Mask_ON",int) = 0
        _Mask_Texture ("MaskTex", 2D) = "white" {}
        _Mask_u ("Mask_U_Speed", Float) = 0
        _Mask_v ("Mask_V_Speed", Float) = 0
        [Toggle]_MaskTexAR("MaskTexAR", Float) = 0
        [Toggle]_CustomdataMaskUV("CustomdataMaskUV", int) = 0
        
        //溶解
        [Toggle(_Diss_ON)]_Diss_ON("Diss_ON",int) = 0
        _DissTex ("DissTex", 2D) = "white" {}
        _Diss_U_Speed ("Diss_U_Speed", Float) = 0
        _Diss_V_Speed ("Diss_V_Speed", Float) = 0
        _Diss_Pow ("溶解程度", Range(0 , 1)) = 0.6096441
        _Diss_Hard ("溶解软硬度", Range(0 , 1)) = 0.5
        _Diss_Width ("溶解宽度", Range(0 , 1)) = 0
        [HDR]_WidthColor ("溶解颜色" , Color) = (1,1,1,1)
        [Toggle(_DoubleDiss_ON)]_DoubleDiss_ON("亮边溶解开关",int) = 0
        [Toggle]_DissTexAR("DissTexAR", Float) = 1
        [Toggle]_CustomdataDis("CustomdataDis", int) = 0
        
        //扰动
        [Toggle(_Dist_ON)]_Dist_ON("Dist_ON",int) = 0
        _DistortTex("DistortTex", 2D) = "white" {}
        [Toggle]_DistortTexAR("DistortTexAR", Float) = 1
        _DistortFactor("DistortFactor", Range( 0 , 1)) = 0
        _DistortTexUSpeed("DistortTexUSpeed", Float) = 0
		_DistortTexVSpeed("DistortTexVSpeed", Float) = 0
        [Toggle]_DistortMainTex("DistortMainTex", int) = 0
		[Toggle]_DistortMaskTex("DistortMaskTex", int) = 0
		[Toggle]_DistortDissTex("DistortDissTex", int) = 0
        [Toggle]_CustomdataDist("_CustomdataDist", int) = 0
        
        
    }
    
    
    SubShader
    {
        //增加标签
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        
        }
        LOD 0 

        Pass
        {
            Name "Forward"
            Tags { "LightMode"="ForwardBase" }
            //渲染模式
            Blend [_BlendModeSrc] [_BlendModeDst]           //混合模式
            ZTest [_Ztest]
            ZWrite [_Zwrite]              //Z深度开关
            Cull [_CullMode]              //剔除模式
            
            
            CGPROGRAM                                                      // 使用HLSL语言
            // 全局宏
            // #pragma multi_compile _ _Mask_ON    //遮罩宏
            // #pragma multi_compile _ _Diss_ON    //溶解宏
            // #pragma multi_compile _ _DoubleDiss //亮边溶解宏
            
            // 本地局部宏
            #pragma shader_feature_local _ _Mask_ON    //遮罩宏
            #pragma shader_feature_local _ _Diss_ON    //溶解宏
            #pragma shader_feature_local _ _DoubleDiss //亮边溶解宏
            #pragma shader_feature_local _ _Dist_ON    //扰动宏
            
            #pragma vertex vert
            #pragma fragment frag
            //#pragma multi_compile_fog
            //#pragma multi_compile _ _ENABLE_FOG_TRANS   //雾效宏

            #include "UnityCG.cginc"          // 增加函数库

            //常量缓冲区（空间较小）不适合放纹理类的大数据，适合存放float,half之类不占空间的数据
            CBUFFER_START(UnityPerMaterial)
                int _Models;
                int _DoubFace;
                int _CustomdataMainTexUV;
                int _CustomdataMaskUV;
                half _MainAlpha;
                half _AlphaClip;
            
                //主贴图
                half4 _Color0;
                half4 _BackColor;
                half4 _Main_Tex_ST;
                half _Main_u;
                half _Main_v;
            
                //遮罩
                half4 _Mask_Texture_ST;
                half _Mask_u;
                half _Mask_v;
                half _MaskTexAR;
            
                //溶解
                int _DoubleDiss_ON;
                int _CustomdataDis;
                half4 _DissTex_ST;
                half _Diss_Pow;
                half _Diss_Hard;
                half _Diss_Width;
                half4 _WidthColor;
                half _Diss_U_Speed;
                half _Diss_V_Speed;
                half _DissTexAR;  

                //扰动
                half4 _DistortTex_ST;
                half _DistortDissTex;   //扰动溶解图开关
                half _DistortMaskTex;   //扰动遮罩图开关
                half _DistortMainTex;   //扰动主贴图开关
                half _DistortFactor;    //扰动强度
                half _DistortTexVSpeed; 
			    half _DistortTexUSpeed;
			    half _DistortTexAR;     //是否使用R通道

                
            
            CBUFFER_END
            
            //贴图采样
            sampler2D _Main_Tex;
            float4 sampler_Main_Tex;
            sampler2D _Mask_Texture;
            float4 sampler_Mask_Texture;
            sampler2D _DissTex;
            float4 sampler_DissTex;
            sampler2D _DistortTex;
            float4 sampler_DistortTex;
            /*
            TEXTURE2D(_Main_Tex);
            SAMPLER(sampler_Main_Tex);
            TEXTURE2D(_Mask_Texture);
            SAMPLER(sampler_Mask_Texture);
            TEXTURE2D(_DissTex);
            SAMPLER(sampler_DissTex);
            TEXTURE2D(_DistortTex);
            SAMPLER(sampler_DistortTex);
            */
            
            struct appdata
            {
                float4 positionOS : POSITION;                               // 顶点输入
                float2 uv : TEXCOORD0;                                      // uv输入
                float4 color : COLOR;                                       // 顶点颜色输入
                float3 normal : NORMAL;                                     // 法线输入

                float4 customData1 : TEXCOORD1;                             //第二套UV输入
                float4 customData2 : TEXCOORD2;                             //第三套UV输入
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                
                float3 worldPos : TEXCOOR1;
                float4 texcoord2 : TEXCOORD2;

                float4 Data1 : TEXCOORD3;
                float4 Data2 : TEXCOORD4;
            };
            
            v2f vert(appdata v)
            {
                v2f o;                                                      //定义输出
                o.Data1 = v.customData1;
                o.Data2 = v.customData2; 
                o.uv = v.uv;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                o.texcoord2.xyz = worldNormal;
                o.texcoord2.w = 0;
                o.worldPos = mul(unity_ObjectToWorld, v.positionOS);
                o.vertex = UnityObjectToClipPos(v.positionOS);
                //o.vertex = UnityObjectToClipPos(v.positionOS.xyz);        //读取顶点方式改变
                o.color = v.color;                                          //读取顶点颜色
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half3 Color;
                half Alpha = _MainAlpha;

                //主贴图
                half2 uv_MainTex = i.uv.xy * _Main_Tex_ST.xy + _Main_Tex_ST.zw; //主贴图UV
                //扰动影响主贴图
                //是否打开扰动
                #ifdef _Dist_ON
                {
                    half2 distUVSpeed = (half2(_DistortTexUSpeed , _DistortTexVSpeed));
                    half2 uv_DistTex = i.uv * _DistortTex_ST.xy + _DistortTex_ST.zw;
                    half2 distUVMove = ( 1.0 * _Time.y *  distUVSpeed + uv_DistTex);
                    half4 tex2dDist = tex2D( _DistortTex, distUVMove);
                    half Dist1 = (( _DistortTexAR == 0.0 ? tex2dDist.a : tex2dDist.r) * _DistortFactor);
                    uv_MainTex = ( _DistortMainTex == 0.0 ? uv_MainTex : ( uv_MainTex + Dist1 ));
                }
                #endif
                //是否打开双面
                half4 lerp2 = lerp(_Color0,(lerp( _Color0 , _BackColor , (1.0 + (sign( (dot( (normalize(i.texcoord2.xyz)) , (normalize( _WorldSpaceCameraPos.xyz - i.worldPos )))) ) - -1.0) * (0.0 - 1.0) / (1.0 - -1.0)))),_DoubFace);
                
                half4 tex2dMainTex = tex2D(_Main_Tex,(uv_MainTex + (_Time.y * half2(_Main_u, _Main_v)) + ((_CustomdataMainTexUV)?( i.Data1.xy):(0.0).xx))) * lerp2;
                
                Color = tex2dMainTex.rgb;
                Alpha = _MainAlpha * tex2dMainTex.a;
                    
                //是否打开遮罩
                #ifdef _Mask_ON
                {
                    half2 uv_MaskTex = i.uv.xy * _Mask_Texture_ST.xy + _Mask_Texture_ST.zw; //遮罩贴图UV
                    //扰动影响遮罩贴图
                    #ifdef _Dist_ON
                    {
                        half2 distUVSpeed = (half2(_DistortTexUSpeed , _DistortTexVSpeed));
                        half2 uv_DistTex = i.uv * _DistortTex_ST.xy + _DistortTex_ST.zw;
                        half2 distUVMove = ( 1.0 * _Time.y *  distUVSpeed + uv_DistTex);
                        half4 tex2dDist = tex2D( _DistortTex,distUVMove);
                        half Dist1 = (( _DistortTexAR == 0.0 ? tex2dDist.a : tex2dDist.r) * _DistortFactor);
                        uv_MaskTex = ( _DistortMaskTex == 0.0 ? uv_MaskTex : ( uv_MaskTex + Dist1 ));
                    }
                    #endif
                    
                    half4 maskTex = tex2D(_Mask_Texture,(uv_MaskTex + (_Time.y * half2(_Mask_u, _Mask_v)) + ((_CustomdataMaskUV) ? (i.Data2.xy) : (0.0).xx)));

                    Color = Color * maskTex.rgb;
                    Alpha = saturate( ( Alpha * ( _MaskTexAR == 0.0 ? maskTex.a : maskTex.r) ));

                }
                #endif

                //是否打开溶解
                #ifdef _Diss_ON
                {
                    half2 uv_DissTex = i.uv.xy * _DissTex_ST.xy + _DissTex_ST.zw; //遮罩贴图UV
                    
                    //扰动影响溶解贴图
                    #ifdef _Dist_ON
                    {
                        half2 distUVSpeed = (half2(_DistortTexUSpeed , _DistortTexVSpeed));
                        half2 uv_DistTex = i.uv * _DistortTex_ST.xy + _DistortTex_ST.zw;
                        half2 distUVMove = ( 1.0 * _Time.y *  distUVSpeed + uv_DistTex);
                        half4 tex2dDist = tex2D( _DistortTex, distUVMove);
                        half Dist1 = (( _DistortTexAR == 0.0 ? tex2dDist.a : tex2dDist.r) * _DistortFactor);
                        uv_DissTex = ( _DistortDissTex == 0.0 ? uv_DissTex : ( uv_DissTex + Dist1 ));
                    }
                    #endif
                    
                    half temp_output_275_0 = (-_Diss_Width + ((( _CustomdataDis )?( i.Data1.z ):( _Diss_Pow )) - 0.0) * (1.0 - -_Diss_Width) / (1.0 - 0.0));
				    half temp_output_277_0 = ( _Diss_Hard + 0.0001 );
				    half temp_output_272_0 = (-temp_output_277_0 + (( temp_output_275_0 + _Diss_Width ) - 0.0) * (1.0 - -temp_output_277_0) / (1.0 - 0.0));
    
                    half2 dissUVSpeed = (half2(_Diss_U_Speed , _Diss_V_Speed));
                    half2 dissUVMove = ( 1.0 * _Time.y *  dissUVSpeed + uv_DissTex);
                    half4 tex2dDiss = tex2D( _DissTex,dissUVMove);
                    half dissAR = ( _DissTexAR == 0.0 ? tex2dDiss.a : tex2dDiss.r);
                    half DissSmoothStep1 = smoothstep ( temp_output_272_0 , (temp_output_272_0 + temp_output_277_0 ) , dissAR);
                    half4 DissLerp1 = lerp( tex2dMainTex , _WidthColor , ( _WidthColor.a * ( 1.0 - DissSmoothStep1 ) * _MainAlpha));
                    half temp_output_270_0 = (-temp_output_277_0 + (temp_output_275_0 - 0.0) * (1.0 - -temp_output_277_0) / (1.0 - 0.0));
				    half DissSmoothStep2 = smoothstep( temp_output_270_0 , ( temp_output_270_0 + temp_output_277_0 ) , dissAR);
    
                    Color = DissLerp1.rgb;
                    Alpha = saturate( ( DissSmoothStep2 * Alpha));
                }
                #endif

                //最终输出
                Color = Color * i.color.rgb;
                Alpha = saturate(Alpha * i.color.a);
                clip ( Alpha - _AlphaClip );
                return half4( Color , Alpha );
                
            }
            
            ENDCG
        }
    }
    CustomEditor "VFX_GUI"
}

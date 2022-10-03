Shader "Unlit/UI"
{
    Properties
           {
               [Header(Base)]
               _MainTex("Tex",2D) = "white"{}
               _Color("color",COLOR) =(1,1,1,1) 
               [Header(Dissolve)]
               _Clip("Value",Range(0,1)) = 0
               _DissolveTex("DissolveTex(R)",2D) ="white"{}
               _RampTex("RampTex(RGB)",2D)="white"{}
               
           }
           SubShader
           {
               pass
               {
                   CGPROGRAM
       
                   #pragma vertex vert
                   #pragma fragment frag
                   #pragma multi_compile _ _DISSOLVE_ON
                   #include"UnityCG.cginc"
                   //输入结构
                   sampler2D  _MainTex;
                   fixed4  _Color;
                   fixed _Clip;
                   sampler2D _DissolveTex;float4 _DissolveTex_ST;//修改纹理的offset和tilling
                   sampler1D _RampTex;//2D换成1D性能增加
       
                   struct appdata{
                       float4 vertex :POSITION;
                       float4 uv : TEXCOORD;
                   };
                   //输出结构
                   struct v2f{
                       float4  pos:SV_POSITION;
                       float4 uv : TEXCOORD;
                       
                   };
                   //顶点着色器
                   v2f vert(appdata v){
                       v2f o;
                       o.pos = UnityObjectToClipPos(v.vertex);
                        o.uv.xy = v.uv.xy;
                       // o.uv.zw=v.uv*_DissolveTex_ST.xy+_DissolveTex_ST.zw;//改语义等于TRANSFORM_TEX
                       o.uv.zw = TRANSFORM_TEX(v.uv,_DissolveTex);
                      
                       //把两个uv集成到一个uv里面去
                       return o;
                   } 
                   //片段，优化准则之一：能在顶点着色器完成的事情不要在片段着色器去做
                   fixed4 frag(v2f i):SV_TARGET{
                       fixed4 color;
                       fixed4 tex = tex2D( _MainTex,i.uv.xy);//一般在片段采样
                 
                       fixed4 dissolvetex = tex2D(_DissolveTex,i.uv.zw);
                         clip(dissolvetex.b-_Clip);//对片段的舍弃只能在片段着色器
                       fixed dissolveValue = saturate((dissolvetex.b-_Clip)/(_Clip+0.1-_Clip));//等于smooth
                       fixed4 ramptex = tex1D(_RampTex,dissolveValue);
                      
                     
                       color = tex*ramptex +_Color;
               
                       return color;
                   }
                   ENDCG
        }
    }
}

Shader "BlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        CGINCLUDE
        #include "UnityCG.cginc"
        #pragma fragmentoption ARB_precision_hint_fastest
        #pragma enable_d3d11_debug_symbols
        
        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };
                    
        struct v2f
        {
            float4 grabPos : TEXCOORD0;
            float4 pos : SV_POSITION;
        };
        
        v2f vert (appdata v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.grabPos = ComputeGrabScreenPos(o.pos);
            return o;
        }
        
        ENDCG

        //////////////////////////////////////////
         //    Second pass for flip and horizontal blur  //
         ///////////////////////////////////////////
        GrabPass {
            "_MainTex"
        }
       Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma enable_d3d11_debug_symbols

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            
            fixed4 frag (v2f i) : SV_Target
            {
                sampler2D tex = _MainTex; 
                float _texelw = _MainTex_TexelSize.x;
                float2 uv = i.grabPos;  
                 
                float4 pix = mul(tex2D(tex, float2(uv.x + -3*_texelw, uv.y )),0.106595);   
                pix += mul(tex2D(tex, float2(uv.x + -2*_texelw, uv.y )),0.140367);   pix += mul(tex2D(tex, float2(uv.x + -1*_texelw, uv.y )),0.165569);   pix += mul(tex2D(tex, float2(uv.x + 0*_texelw, uv.y )),0.174938);   pix += mul(tex2D(tex, float2(uv.x + 1*_texelw, uv.y )),0.165569);   pix += mul(tex2D(tex, float2(uv.x + 2*_texelw, uv.y )),0.140367);   pix += mul(tex2D(tex, float2(uv.x + 3*_texelw, uv.y )),0.106595);   
                return pix;
            }
            ENDCG
        }
        //////////////////////////////////////////
         //    Third pass for vertical blur  //
         ///////////////////////////////////////////
        GrabPass {
            "_GrabTex6"
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _GrabTex6;
            float4 _GrabTex6_TexelSize;
            
            fixed4 frag (v2f i) : SV_Target
            {
                sampler2D tex = _GrabTex6; 
                float _texelw = _GrabTex6_TexelSize.x;
                float2 uv = i.grabPos; 

                float4 pix = mul(tex2D(tex, float2(uv.x, uv.y + -3 *_texelw)),0.106595);   
                pix += mul(tex2D(tex, float2(uv.x, uv.y + -3*_texelw)),0.106595);   pix += mul(tex2D(tex, float2(uv.x, uv.y + -2*_texelw)),0.140367);   pix += mul(tex2D(tex, float2(uv.x, uv.y + -1*_texelw)),0.165569);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 0*_texelw)),0.174938);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 1*_texelw)),0.165569);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 2*_texelw)),0.140367);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 3*_texelw)),0.106595);   

                return pix;
            }
            ENDCG
        }
    }
}

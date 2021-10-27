Shader "Shift"
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
            
            float gazeY;
            float gazeX;
            float aspectRatio; 
            
            fixed4 frag (v2f i) : SV_Target
            {
                sampler2D tex = _MainTex; 
                float _texelw = _MainTex_TexelSize.x;
                float2 uv = i.grabPos;
                uint sampleDistance = 15;

                int pixelsX = round((.5-gazeX)/_texelw);
                int pixelsY = round((.5-gazeY)/_texelw); 
                
                float shiftY = pixelsY * _texelw;
                float shiftX = pixelsX * _texelw;
                
                float2 newUV;
                newUV.x = uv.x + shiftX;
                newUV.y = uv.y + shiftY; 
                
                return tex2D(tex, newUV); 
            }
            ENDCG
        }
        
//        GrabPass {
//            "_BlurredTex"
//        }
//        Pass
//        {
//            CGPROGRAM
//            #pragma vertex vert
//            #pragma fragment frag
//
//            sampler2D _BlurredTex;
//            float4 _BlurredTex_TexelSize;
//
//            float gazeY;
//            float gazeX;
//            float aspectRatio;
//            float scotomaSize; 
//            
//            fixed4 frag (v2f i) : SV_Target
//            {
//                sampler2D tex = _BlurredTex; 
//                float _texelw = _BlurredTex_TexelSize.x;
//                float2 uv = i.grabPos;
//                uint sampleDistance = 15; 
//                
//                float distY = uv.y-gazeY;
//                float distX = (uv.x-gazeX)  / aspectRatio; 
//                if( sqrt( distY*distY + distX*distX ) < scotomaSize)
//                {
//                    float4 pix = mul(tex2D(tex, float2(uv.x, uv.y + -sampleDistance * 3 *_texelw)),0.106595);   
//                    pix += mul(tex2D(tex, float2(uv.x, uv.y + -sampleDistance * 2*_texelw)),0.140367);   pix += mul(tex2D(tex, float2(uv.x, uv.y + -sampleDistance * 1*_texelw)),0.165569);   /*pix += mul(tex2D(tex, float2(uv.x, uv.y + 0*_texelw)),0.174938);*/   pix += mul(tex2D(tex, float2(uv.x, uv.y + sampleDistance * 1*_texelw)),0.165569);   pix += mul(tex2D(tex, float2(uv.x, uv.y + sampleDistance * 2*_texelw)),0.140367);   pix += mul(tex2D(tex, float2(uv.x, uv.y + sampleDistance * 3*_texelw)),0.106595);   
//
//                    return pix;
//                }
//
//                 return tex2D(tex, uv); 
//            }
//            ENDCG
//        }
    }
}
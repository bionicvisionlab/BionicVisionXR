Shader "ImagePreprocessingWithBlur"
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
        
        sampler2D _MainTex;
        half4 _MainTex_TexelSize;
        float _BlurSize;
        
         struct appdata
        {
            float4 vertex : POSITION;
            half2 uv : TEXCOORD0;
        };
                
        struct v2f
        {
            half2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        float lum(float3 color) {
            return color.r*.3 + color.g*.59 + color.b*.11;
        }
        ENDCG
        
        Pass
        { 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            float sobel(sampler2D tex, float2 uv){
                float _texelw = _MainTex_TexelSize.x;
                float3 Gx = tex2D(tex, float2(uv.x-_texelw, uv.y-_texelw)).rgb
                    + 2*tex2D(tex, float2(uv.x-_texelw, uv.y)).rgb
                    + tex2D(tex, float2(uv.x-_texelw, uv.y+_texelw)).rgb
                    + (-1)*tex2D(tex, float2(uv.x+_texelw, uv.y-_texelw)).rgb
                    + (-2)*tex2D(tex, float2(uv.x+_texelw, uv.y)).rgb
                    + (-1)*tex2D(tex, float2(uv.x+_texelw, uv.y+_texelw)).rgb;

                float3 Gy = tex2D(tex, float2(uv.x-_texelw, uv.y-_texelw) ).rgb
                    + 2*tex2D(tex, float2(uv.x, uv.y-_texelw)).rgb
                    + tex2D(tex, float2(uv.x+_texelw, uv.y-_texelw)).rgb
                    + (-1)*tex2D(tex, float2(uv.x-_texelw, uv.y+_texelw)).rgb
                    + (-2)*tex2D(tex, float2(uv.x, uv.y+_texelw)).rgb
                    + (-1)*tex2D(tex, float2(uv.x+_texelw, uv.y+_texelw)).rgb;

                float Gvx = max(max(max(Gx.r, Gx.g), Gx.b), lum(Gx));
                float Gvy = max(max(max(Gy.r, Gy.g), Gy.b), lum(Gy));
                float val = sqrt(Gvx*Gvx + Gvy*Gvy);

                return val;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 sobelCol = sobel(_MainTex, i.uv);
                return sobelCol;
            } 
            ENDCG
        }
        
         GrabPass {
            "_GrabTex"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            sampler2D _GrabTex;
            float4 _GrabTex_TexelSize;
            
            float contr(sampler2D tex, float2 uv) {
                float _texelw = _GrabTex_TexelSize.x;
                const int dist = 1;
                float4 col = float4(0,0,0,0);
                float lowval = 1.0;
                float highval = 0.0;

                for (int i = -dist; i <= dist; ++i) {
                    for (int j = -dist; j <= dist; ++j) {
                        if (length(float2(i, j)) > dist) {
                            float4 pix = tex2D(tex, float2(uv.x+i*_texelw, uv.y+j*_texelw));
                            lowval = min(lowval , lum(pix.rgb));
                            highval= max(highval , lum(pix.rgb));
                        }
                    }
                }

                if (lum(col) - lowval > 0.1 || highval - lum(col) > 0.1) {
                    col.rgb = float3(1.0, 1.0, 1.0);
                }

                return col;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 contrCol = contr(_GrabTex, i.uv);    
                return contrCol;
            }
            
            ENDCG
        } 
        
        ///////////////////////////////////////////
         // Third pass for horizontal blur //
         ///////////////////////////////////////////
        GrabPass {
            "_GrabTex2"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _GrabTex2;
            float4 _GrabTex2_TexelSize;
                  
            fixed4 frag (v2f i) : SV_Target
            {
                sampler2D tex = _GrabTex2; 
                float _texelw = _GrabTex2_TexelSize.x;
                float2 uv = i.uv; 
               

                float4 pix = mul(tex2D(tex, float2(uv.x + -3*_texelw, uv.y )),0.106595);   
                pix += mul(tex2D(tex, float2(uv.x + -2*_texelw, uv.y )),0.140367);   pix += mul(tex2D(tex, float2(uv.x + -1*_texelw, uv.y )),0.165569);   pix += mul(tex2D(tex, float2(uv.x + 0*_texelw, uv.y )),0.174938);   pix += mul(tex2D(tex, float2(uv.x + 1*_texelw, uv.y )),0.165569);   pix += mul(tex2D(tex, float2(uv.x + 2*_texelw, uv.y )),0.140367);   pix += mul(tex2D(tex, float2(uv.x + 3*_texelw, uv.y )),0.106595);   

                return pix;
            }
            ENDCG
        }
        
        ///////////////////////////////////////////
         // Third pass for vertical blur and electrode grab //
         ///////////////////////////////////////////
        GrabPass {
            "_GrabTex3"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 5.0

            sampler2D _GrabTex3;
            float4 _GrabTex3_TexelSize;
           
            struct Electrode
            {
               float x;
               float y; 
               float current; 
            };
            
            uniform RWStructuredBuffer<Electrode> electrodesBuffer : register(u2);
            uint numberElectrodes;
            uint xResolution; 
            uint yResolution;
            float amplitude; 
            
            uint pixelNumberX( float screenPos){
                 return ceil(xResolution * screenPos);
            }  
            uint pixelNumberY( float screenPos){
                 return ceil(yResolution * screenPos);
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                sampler2D tex = _GrabTex3; 
                float _texelw = _GrabTex3_TexelSize.x;
                float2 uv = i.uv; 
               
                float4 pix = mul(tex2D(tex, float2(uv.x, uv.y + -3*_texelw)),0.106595);   
                pix += mul(tex2D(tex, float2(uv.x, uv.y + -2*_texelw)),0.140367);   pix += mul(tex2D(tex, float2(uv.x, uv.y + -1*_texelw)),0.165569);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 0*_texelw)),0.174938);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 1*_texelw)),0.165569);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 2*_texelw)),0.140367);   pix += mul(tex2D(tex, float2(uv.x, uv.y + 3*_texelw)),0.106595);   

                for(int currentElectrode = 0; currentElectrode < numberElectrodes; currentElectrode++){
                    if( pixelNumberX(i.uv.x) == pixelNumberX(electrodesBuffer[currentElectrode].x) 
                    && pixelNumberY(i.uv.y) == pixelNumberY(electrodesBuffer[currentElectrode].y)){
                        electrodesBuffer[currentElectrode].current = amplitude * pix[3]; 
                    }
                }

                
                return pix;
            }
            ENDCG
        }
    } 
}

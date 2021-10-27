Shader "ImagePreprocessing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;


            float lum(float3 color) {
            return color.r*.3 + color.g*.59 + color.b*.11;
            }

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

        ///////////////////////////////////////////
        // Second pass for the Edge ehancement   //
        ///////////////////////////////////////////
        GrabPass {
            "_GrabTex"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 5.0
            #pragma enable_d3d11_debug_symbols

            #include "UnityCG.cginc"

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

            sampler2D _GrabTex;
            float4 _GrabTex_TexelSize;


            float lum(float3 color) {
            return color.r*.3 + color.g*.59 + color.b*.11;
            }

            float contr(sampler2D tex, float2 uv) {
                float _texelw = _GrabTex_TexelSize.x;
                const int dist = 1;
                float4 col = tex2D(tex, uv);
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
                return (int) (xResolution * screenPos);
            }  
            uint pixelNumberY( float screenPos){
                return (int) (yResolution * screenPos);
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 contrCol = contr(_GrabTex, i.grabPos);
                
                for(int currentElectrode = 0; currentElectrode < numberElectrodes; currentElectrode++){
                    if( pixelNumberX(i.grabPos.x) == pixelNumberX(electrodesBuffer[currentElectrode].x) 
                    && pixelNumberY(i.grabPos.y) == pixelNumberY(electrodesBuffer[currentElectrode].y)){
                        electrodesBuffer[currentElectrode].current = amplitude * contrCol[3]; 
                    }
                }
                
                return contrCol;
            }
            ENDCG
        }
    }
}

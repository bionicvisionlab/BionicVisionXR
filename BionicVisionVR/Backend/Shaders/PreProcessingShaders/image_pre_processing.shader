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
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma target 5.0
            
            
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
            int invert; 

            float lum(float3 color) {
            if(invert==1)
                return .5-(color.r*.3 + color.g*.59 + color.b*.11);
            return color.r*.3 + color.g*.59 + color.b*.11;
            }
           
            struct Electrode
            {
                int electrodeNumber; 
                float x;
                float y; 
                float current; 
            };
            
            uniform RWStructuredBuffer<Electrode> electrodesBuffer : register(u2);
            uint numberElectrodes;
            uint xResolution; 
            uint yResolution;
            float amplitude;
            float gazeShiftX;
            float gazeShiftY; 
            
            uint pixelNumberX( float screenPos){
                return (int) (xResolution * screenPos);
            }  
            uint pixelNumberY( float screenPos){
                return (int) (yResolution * screenPos);
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float _texelw = _MainTex_TexelSize.x;
                
                int pixelsX = round((.5-gazeShiftX)/_texelw);
                int pixelsY = round((.5-gazeShiftY)/_texelw); 
                
                float shiftY = pixelsY * _texelw;
                float shiftX = pixelsX * _texelw;
                
                float2 newUV;
                newUV.x = i.uv.x - shiftX;
                newUV.y = i.uv.y - shiftY;

                float lumCol = lum(tex2D(_MainTex, newUV).rgb);
                
                for(int currentElectrode = 0; currentElectrode < numberElectrodes; currentElectrode++){
                    // electrodesBuffer[currentElectrode].current = 0; 
                    if( pixelNumberX(i.uv.x) == pixelNumberX(electrodesBuffer[currentElectrode].x) 
                    && pixelNumberY(i.uv.y) == pixelNumberY(electrodesBuffer[currentElectrode].y)){
                        electrodesBuffer[currentElectrode].current = 1 * lumCol; 
                    }
                }
                
                return fixed4(lumCol, lumCol, lumCol, lumCol); ;
            }
            ENDCG
        }
    }
}

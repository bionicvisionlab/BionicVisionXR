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


            float lum(float3 color) {
            return color.r*.3 + color.g*.59 + color.b*.11;
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
                float lumCol = lum(tex2D(_MainTex, i.uv).rgb);
                
                for(int currentElectrode = 0; currentElectrode < numberElectrodes; currentElectrode++){
                    if( pixelNumberX(i.uv.x) == pixelNumberX(electrodesBuffer[currentElectrode].x) 
                    && pixelNumberY(i.uv.y) == pixelNumberY(electrodesBuffer[currentElectrode].y)){
                        electrodesBuffer[currentElectrode].current = amplitude * lumCol; 
                    }
                }
                
                return fixed4(lumCol, lumCol, lumCol, lumCol); ;
            }
            ENDCG
        }
    }
}

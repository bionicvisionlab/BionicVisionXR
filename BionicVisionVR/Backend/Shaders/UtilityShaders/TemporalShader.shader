Shader "TemporalShader"
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

            #pragma target 5.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                return o;
            }
            
            struct TemporalVariables
            {
                float ca;
                float bp;
            };

            uniform RWStructuredBuffer<TemporalVariables> tempVariables : register(u1);
            uint xResolution;
            uint yResolution;
            float tau_ca;
            float tau_bp;
            float ca_scale;
            float amplitude; 
            float dt;
            
            uint pixelNumber(float screenPosX, float screenPosY){
                return (int) (xResolution * screenPosX) + (xResolution * (int) (yResolution * screenPosY));
            }  
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.grabPos);
                
                float c = tempVariables[pixelNumber(i.grabPos.x, i.grabPos.y)].ca; // last frames charge value 
                c -= dt * tau_ca * c;
                c += dt * amplitude * col.x; //Stimulus
                
                c = c<0 ? 0 : c;
                

                float b = tempVariables[pixelNumber(i.grabPos.x, i.grabPos.y)].bp; // takes last frame's brightness value 
                b -= dt * tau_bp * tempVariables[pixelNumber(i.grabPos.x, i.grabPos.y)].bp;
                b -= dt * ca_scale * tempVariables[pixelNumber(i.grabPos.x, i.grabPos.y)].ca;
                b += dt * amplitude * col.x;
                
                
                b = b<0 ? 0 : b;
                b = b>1 ? 1 : b; 
                
                tempVariables[pixelNumber(i.grabPos.x, i.grabPos.y)].bp = b;
                tempVariables[pixelNumber(i.grabPos.x, i.grabPos.y)].ca = c; 
                return fixed4(b, b, b, b); 
            }
            ENDCG
        }
    }
}

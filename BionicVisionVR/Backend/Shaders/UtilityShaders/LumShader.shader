Shader "LumShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }


            float lum(float3 color) {
                return color.r*.3 + color.g*.59 + color.b*.11;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
              float lumValue = lum(tex2D(_MainTex, i.uv).rgb);
                return fixed4(lumValue, lumValue, lumValue, lumValue);
            }
            ENDCG
        }
    }
}

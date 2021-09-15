Shader "ScoreboardModel"
{
    Properties
    {
        _GrabTex2 ("Texture", 2D) = "white" {}
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
            #pragma enable_d3d11_debug_symbols
            
            #include "UnityCG.cginc"

            //**//
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
            
            sampler2D _GrabTex2;
       
       // Variables passed in from RunShaderWithBuffer
           struct SimulationVariables
            {
               float r1; 
               float r2;
               float r3;
               float ca;
               float r4a;
               float r4b;
               float r4c;
               float t;  
            };
            
            struct AxonSegment
            {
                int x;
                int y;  
                float brightnessContribution;
            };
            
            struct Electrode
            {
                float x;
                float y; 
                float current; 
            };
            
            uniform RWStructuredBuffer<SimulationVariables> simulationVariables : register(u1); 
            uniform RWStructuredBuffer<Electrode> electrodesBuffer : register(u2);
            StructuredBuffer<AxonSegment> axonContributionBuffer;
            StructuredBuffer<int> axonIdxStartBuffer;
            StructuredBuffer<int> axonIdxEndBuffer;
            StructuredBuffer<int> idxStartBuffer;
            StructuredBuffer<int> idxEndBuffer; 
            uint xElectrodes;
            uint yElectrodes; 
            uint numberElectrodes; 
            uint resolution1D;  
            float implant_fov; 
            float headset_fov; 
            float degreeToMicronMultiplier; 
            float rho;
            float amplitudeMultiplier;
            float threshold;
            float xPixelSize;
            float yPixelSize;  
            float dt; 
            uint frameDur; 
            uint timeStepsBetweenPulses;
            uint posStart;
            uint posEnd;
            uint negStart;
            uint negEnd; 
            uint xResolution;
            uint yResolution; 
            float minimumScreenPositionX;
            float maximumScreenPositionX;
            float minimumScreenPositionY;
            float maximumScreenPositionY;
            
            
            #define tau1 .00042f
            #define tau2   .00045f
            #define tau3   .0002625f
            #define eps   0.0f 
            #define tau_ca   3.0f
            #define beta   0.83f 
//30.555f

            float screenPosToDegree(float screenPos)
            {
                return (screenPos - 0.5f) * headset_fov; 
            }
            
             float degreeToMicron(float degree)
            {
               
                float sign = degree >= 0 ? 1.0f : -1.0f;
                degree = abs(degree); 
                float micron = 0.268f * degree + 3.427e-4f * (float) pow(degree,2) - 8.3309e-6f * (float) pow(degree,3);
                micron = 1e3f * micron; 
        
                return micron * sign;
            }
            
            float screenPosToMicron(float screenPos)
            {
                return degreeToMicron(screenPosToDegree(screenPos)); 
            }

            float distance2(float2 x1y1, float2 x2y2){
                float screenToRetinaConversion = 288 * headset_fov; 
                /*float xDist = screenPosToMicron(x1y1.x) - screenPosToMicron(x2y2.x); 
                float yDist = screenPosToMicron(x1y1.y) - screenPosToMicron(x2y2.y);*/
                float xDist = (x1y1.x - x2y2.x) * screenToRetinaConversion;
                float yDist = (x1y1.y - x2y2.y) * screenToRetinaConversion;
                float xDist2 = xDist * xDist;
                float yDist2 = yDist * yDist;
                float dist2 =  xDist2 + yDist2; 
                if(dist2 == 0.0){
                    dist2=.00000000000001; 
                }
                return dist2;
            } 
            
             uint pixelNumberX( float screenPos){
                return (int) (xResolution * screenPos);
            }  
            
            uint pixelNumberY( float screenPos){
                return (int) (yResolution * screenPos);
            }  
            
            fixed4 frag (v2f i) : SV_Target
            {
                if(i.grabPos.x < minimumScreenPositionX || i.grabPos.x > maximumScreenPositionX || i.grabPos.y < minimumScreenPositionY || i.grabPos.y > maximumScreenPositionY) {
                     discard; 
                 }           
                fixed4 col = tex2D(_GrabTex2, i.grabPos); 
               
                 // Get current pixel being shaded's one dimensional number
                uint xPixel = pixelNumberX(i.grabPos.x);
                uint yPixel = pixelNumberY(i.grabPos.y);
                uint loc1D = xPixel + ((xResolution * implant_fov/headset_fov)+1) * yPixel;  // add +1 for pixel at each end of simulation (ie. -20 deg and 20 deg)
               

                float dist2;
                float gauss;
                float amplitude = 0.0f; 
                float amp = 0.0f; 
                float maxR3 = 0; 

                for(uint j=0; j<numberElectrodes; j++){
                        dist2 = distance2(float2(i.grabPos.x, i.grabPos.y), float2(electrodesBuffer[j].x, 1 - electrodesBuffer[j].y /* screen pos y is inverted vs cartesian coord */));
                        gauss = exp(-dist2 / (2 * rho * rho));
                        amplitude = amplitude + (gauss * electrodesBuffer[j].current);  
                }
                
                for(int l=0; l<frameDur; l++){
                    if(simulationVariables[loc1D].t >= timeStepsBetweenPulses){
                        simulationVariables[loc1D].t = 0; 
                    }
                    amp = 0.0f; 
                    if((simulationVariables[loc1D].t >= posStart) && (simulationVariables[loc1D].t <= posEnd)){
                        amp = amplitudeMultiplier * amplitude;
                    }
                    if((simulationVariables[loc1D].t >= negStart) && (simulationVariables[loc1D].t <= negEnd)){
                        amp = -amplitudeMultiplier * amplitude; 
                    }

                    // Fast ganglion cell response (convolve with gamma function
                    // that has decay constant tau1):
                    simulationVariables[loc1D].r1 = simulationVariables[loc1D].r1 + dt * (-amp - simulationVariables[loc1D].r1) / tau1;
                   
                    // Charge accumulation is now also a gamma function (over 3 seconds)
                    // so that brightness can recover after a while:
                    simulationVariables[loc1D].ca = simulationVariables[loc1D].ca + dt * (eps * max(amp, 0) - simulationVariables[loc1D].ca) / tau_ca;
                    simulationVariables[loc1D].r2 = simulationVariables[loc1D].r2 + dt * (simulationVariables[loc1D].ca - simulationVariables[loc1D].r2) / tau2;
                    
                    // Half-rectification and power nonlinearity:
                    simulationVariables[loc1D].r3 = pow(max(simulationVariables[loc1D].r1 - eps * simulationVariables[loc1D].r2, 0), beta);
                
                    if(simulationVariables[loc1D].r3 > maxR3){
                        maxR3 = simulationVariables[loc1D].r3;
                    }
                
                    simulationVariables[loc1D].t++; 
                }
                
                if(maxR3 < threshold){
                    maxR3 = 0; 
                }

                col.r =  maxR3;
                col.b = maxR3;
                col.g = maxR3;
                col.a = 1.0; 
               
                return col; 
            }

            ENDCG
        }

    }
}

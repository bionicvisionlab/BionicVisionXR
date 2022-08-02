Shader "3Dpercept"
{
    Properties
    {
        _GrabTex4 ("Texture", 2D) = "white" {}
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
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 5.0
            

            #include "UnityCG.cginc"      

            sampler2D _GrabTex4;
       
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
                float x;
                float y;
                float z; 
                float brightnessContribution;
            };
            
            struct Electrode
            {
                float x;
                float y;
                float z; 
                float current; 
            };
            
            uniform RWStructuredBuffer<SimulationVariables> simulationVariables : register(u1); 
            uniform RWStructuredBuffer<Electrode> electrodesBuffer : register(u2);
            uniform RWStructuredBuffer<int> debugBuffer : register(u3); 
            
            StructuredBuffer<float> axonSegmentGaussToElectrodesBuffer;
            StructuredBuffer<AxonSegment> axonContributionBuffer;
            StructuredBuffer<int> axonIdxStartBuffer;
            StructuredBuffer<int> axonIdxEndBuffer;
            StructuredBuffer<int> idxStartBuffer;
            StructuredBuffer<int> idxEndBuffer; 
            
            uint debugMode; 
            uint showElectrodes; 
            uint numberAxonTraces;
            uint specificTrace;

            uint rasterizeGroup;
            uint numberRastGroups;
             
            uint numberElectrodes; 
            uint xResolution;
            uint yResolution;   
            float implant_fov; 
            float headset_fov; 
            float degreeToMicronMultiplier; 
            float rho;
            float amplitudeMultiplier;
            float threshold;
            float xPixelSizeDivBy2;
            float yPixelSizeDivBy2;  
            float dt; 
            uint frameDur; 
            uint timeStepsBetweenPulses;
            uint posStart;
            uint posEnd;
            uint negStart;
            uint negEnd; 
            uint simulatedColumns; 
            float minimumScreenPositionX;
            float maximumScreenPositionX;
            float minimumScreenPositionY;
            float maximumScreenPositionY;
            uint axonBufferLength; 
        
            
            #define tau1 .00042f
            #define tau2   .00045f
            #define tau3   .0002625f
            #define eps   0.0f 
            #define tau_ca   3.0f
            #define beta   0.83f 
            #define electrodeThreshold .1
            #define numberPixelsForElectrodeDisplay 0
//30.555f

            uint pixelNumberX( float screenPos){
                return ceil(xResolution * screenPos);
            }  
            
            uint pixelNumberY( float screenPos){
                return ceil(yResolution * screenPos);
            }  
                      
            fixed4 frag (v2f i) : SV_Target
            {
                if(debugMode == 1){ 
                
                    if(showElectrodes == 1){
                        for(int currentElectrode = 0; currentElectrode < numberElectrodes; currentElectrode++){
                            if(pixelNumberX(i.grabPos.x) >= pixelNumberX(electrodesBuffer[currentElectrode].x) - numberPixelsForElectrodeDisplay
                            && pixelNumberX(i.grabPos.x) <= pixelNumberX(electrodesBuffer[currentElectrode].x) + numberPixelsForElectrodeDisplay
                            && pixelNumberY(i.grabPos.y) >= pixelNumberY(electrodesBuffer[currentElectrode].y) - numberPixelsForElectrodeDisplay
                            && pixelNumberY(i.grabPos.y) <= pixelNumberY(electrodesBuffer[currentElectrode].y) + numberPixelsForElectrodeDisplay){
                                return fixed4(1, 0, 0, 1); 
                            }
                        }
                    }
                    
                    //Traces specific axon bundle that starts at pixel
                    for(uint r = axonIdxStartBuffer[specificTrace]; r < axonIdxEndBuffer[specificTrace]; r++){
                            if(i.grabPos.x < axonContributionBuffer[r].x +.005 && i.grabPos.x > axonContributionBuffer[r].x -.005){
                                if(i.grabPos.y < axonContributionBuffer[r].y +.005 && i.grabPos.y > axonContributionBuffer[r].y -.005){
                                    return fixed4(1, 1, 1, 1); 
                                }
                            }
                        }
                
                    //Traces axon bundles starting at equally spaced intervals
                    for(int q=0; q<numberAxonTraces; q++){
                        for(uint p = axonIdxStartBuffer[0 + (q * round(axonBufferLength/numberAxonTraces))]; p < axonIdxEndBuffer[0 + (q * round(axonBufferLength/numberAxonTraces))]; p++){
                            if(i.grabPos.x < axonContributionBuffer[p].x +.005 && i.grabPos.x > axonContributionBuffer[p].x -.005){
                                if(i.grabPos.y < axonContributionBuffer[p].y +.005 && i.grabPos.y > axonContributionBuffer[p].y -.005){
                                    return fixed4(1, 1, 1, 1); 
                                }
                            }
                        }
                    }
                }
                
                if(!(i.grabPos.x >= minimumScreenPositionX) || !(i.grabPos.x <= maximumScreenPositionX) || !(i.grabPos.y >= minimumScreenPositionY) || !(i.grabPos.y <= maximumScreenPositionY) ) {
                     return fixed4(0,0,0,0); 
                 }           
                fixed4 col = tex2D(_GrabTex4, i.grabPos); 
               
                // Get current pixel being shaded's one dimensional number
                uint xPixel = pixelNumberX(i.grabPos.x - minimumScreenPositionX)-1;
                uint yPixel = pixelNumberY(i.grabPos.y - minimumScreenPositionY)-1;
                uint loc1D = xPixel + (int(xResolution * implant_fov/headset_fov)+1) * yPixel;  // add +1 for pixel at each end of simulation (ie. -20 deg and 20 deg)
              
               
                int axon_idx_start = axonIdxStartBuffer[loc1D]; 
                int axon_idx_end = axonIdxEndBuffer[loc1D]; 
                float brightest_segment = 0.0f; 
                
               for(int m = axon_idx_start; m < axon_idx_end; m++){ // for each axon segment affecting this pixel...
                    float gauss;
                    float segment_brightness = 0.0f; 
                    float amp = 0.0f; 
                    
                    for(uint j=0; j<numberElectrodes; j++){ // for each electrode
                            if(electrodesBuffer[j].current > electrodeThreshold){
                                gauss = axonSegmentGaussToElectrodesBuffer[m*(numberElectrodes*numberRastGroups) + j + (numberElectrodes * rasterizeGroup)]; 
                                segment_brightness = segment_brightness + gauss * electrodesBuffer[j].current * axonContributionBuffer[m].brightnessContribution;
                            }
                    }
                    
                    if(abs(segment_brightness) > abs(brightest_segment)){
                        brightest_segment = segment_brightness;
                    }
                }
                
                if( abs(brightest_segment) < threshold){
                    brightest_segment = 0 ; 
                }

                //brightest_segment = 0.0f; 
                col.r =  brightest_segment;
                col.b = brightest_segment;
                col.g = brightest_segment;
                col.a = 1.0; 
                
                return col; 
            }
            ENDCG
        }
        
    }
}

Shader "Custom/SphereFresnel"
{
    Properties
    {
        _Power("Power", Range(0, 10)) = 1
        _DitherPattern("Dither Pattern", 2D) = "white" {}
        _NoisePattern("Noise Pattern", 2D) = "white" {}
        _NoiseIntensity("Noise Intensity", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD2;
                
            };

            float _Power;
            sampler2D _DitherPattern;
            float4 _DitherPattern_TexelSize;
            sampler2D _NoisePattern;
            float _NoiseIntensity;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD2;
                float4 screenPosition :TEXCOORD3;
                
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.uv = v.uv;
                return o;
            }

            float rand(float2 co)
		    {
			    return frac((sin( dot(co.xy , float2(12.345 * _Time.w, 67.890 * _Time.w) )) * 12345.67890+_Time.w));
            }


            fixed4 frag (v2f i) : SV_Target
            {
                
                //dither
                float2 screenPos = i.screenPosition.xy / i.screenPosition.w;
                float2 ditherCoordinate = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy;
                float ditherValue = tex2D(_DitherPattern, ditherCoordinate).r;

                //fresnel
                float fresnel = pow(1 - saturate(dot(i.normal, i.viewDir)),_Power);
                
                //noise 
                float noise = step(tex2D(_NoisePattern, (screenPos * 3) + float2(rand(1), rand(1))).r, _NoiseIntensity);
                
                ditherValue = step(ditherValue, fresnel);
               
                return ditherValue * noise;
            }
            ENDCG
        }
    }
}

Shader "Custom/Shockwave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Tiling("Tiling", float) = 3
        _Speed("Speed", float) = 3
        _Width("Width", Range(0,1)) = 0.1
        _Falloff("Falloff", Range(0, 10)) = 4

        _DitherPattern("Dither Pattern", 2D) = "white" {}

        _NoisePattern("Noise Pattern", 2D) = "white" {}
        _NoiseIntensity("Noise Intensity", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }


        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite off
            
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
                float4 screenPosition :TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _DitherPattern;
            float4 _DitherPattern_TexelSize;

            float _Tiling;
            float _Speed;
            float _Width;
            float _Falloff;

            sampler2D _NoisePattern;
            float _NoiseIntensity;

             float rand(float2 co)
		    {
			    return frac((sin( dot(co.xy , float2(12.345 * _Time.w, 67.890 * _Time.w) )) * 12345.67890+_Time.w));
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * 2 - 1;
                o.screenPosition = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float radialGradient = 1 - length(i.uv);
                float radialGradientScaled = 1 - pow(length(i.uv),_Falloff);

                float time = _Time.y;
                float speed = 3.0;
                
                float scaledTime = time * _Speed;
                float3 output = step(frac(((radialGradient * _Tiling) + scaledTime)), _Width) * radialGradientScaled;

                float2 screenPos = i.screenPosition.xy / i.screenPosition.w;
                float2 ditherCoordinate = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy;
                
                float ditherValue = tex2D(_DitherPattern, ditherCoordinate).r;
                ditherValue = step(ditherValue, output);

                float noise = step(tex2D(_NoisePattern, (screenPos * 3) + float2(rand(1), rand(1))).r, _NoiseIntensity);

                return ditherValue * noise;
            }
            ENDCG   
        }
    }
}

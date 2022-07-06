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

            float InverseLerp(float a, float b, float t)
            {
                return(t-a)/(b-a);
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
                //float radialGradient = 1 - length(((i.uv * float2(2.0,2.0)) - float2(1.0, 1.0))) - _Threshold;

                float radialGradient = 1 - length(i.uv);
                float radialGradientScaled = 1 - pow(length(i.uv),_Falloff);

                radialGradient = InverseLerp(0, 1, radialGradient);

                float time = _Time.y;
                float speed = 3.0;
                
                float scaledTime = time * _Speed;
                float3 output = step(frac(((radialGradient * _Tiling) + scaledTime)), _Width) * radialGradientScaled;

                float2 screenPos = i.screenPosition.xy / i.screenPosition.w;
                float2 ditherCoordinate = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy;
                
                float ditherValue = tex2D(_DitherPattern, ditherCoordinate).r;
                ditherValue = step(ditherValue, output);

                //fixed4 col = tex2D(_MainTex, i.uv);
                return ditherValue;
            }
            ENDCG   
        }
    }
}

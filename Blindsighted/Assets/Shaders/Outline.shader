Shader "Unlit/Outline"
{
    Properties
    {
        _Size("Size", float) = 0
        _Threshold("Threshold", range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float3 normal : NORMAL;
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float fresnel : TEXCOORD1;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 vertex : SV_POSITION;
            };

            float _Size;
            float _Threshold;


            v2f vert (appdata v)
            {
                v2f o;
                float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.fresnel = 1-saturate(dot(v.normal, viewDir));
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float outline = pow(i.fresnel,_Size) > _Threshold;
                return float4(outline.xxx, 1);
            }
            ENDCG
        }
    }
}

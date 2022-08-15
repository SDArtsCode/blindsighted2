Shader "Custom/Halftone" {
	//show values to edit in inspector
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Step("Step", float) = 0
		[HDR] _Emission("Emission", color) = (0,0,0)

		_HalftonePattern("Halftone Pattern", 2D) = "white" {}
		_NoisePattern("Noise Pattern", 2D) = "white" {}
		_NoiseSpeed("Noise Speed", float) = 2

        _RemapInputMin ("Remap input min value", Range(0, 1)) = 0
        _RemapInputMax ("Remap input max value", Range(0, 1)) = 1
        _RemapOutputMin ("Remap output min value", Range(0, 1)) = 0
        _RemapOutputMax ("Remap output max value", Range(0, 1)) = 1
	}
		SubShader{
			
			
			//the material is completely non-transparent and is rendered at the same time as the other opaque geometry
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

		CGPROGRAM

		//the shader is a surface shader, meaning that it will be extended by unity in the background to have fancy lighting and other features
		//our surface shader function is called surf and we use our custom lighting model
		//fullforwardshadows makes sure unity adds the shadow passes the shader might need
		#pragma surface surf Halftone fullforwardshadows
		#pragma target 3.0

        //basic properties
		sampler2D _MainTex;
		half3 _Emission;

		float _Step;

        //shading properties
		sampler2D _HalftonePattern;
		sampler2D _NoisePattern;
		float4 _HalftonePattern_ST;
		float _NoiseSpeed;

        ///remapping values
        float _RemapInputMin;
        float _RemapInputMax;
        float _RemapOutputMin;
        float _RemapOutputMax;

        //struct that holds information that gets transferred from surface to lighting function
		struct HalftoneSurfaceOutput {
			fixed3 Albedo;
			float2 ScreenPos;
			half3 Emission;
			fixed Alpha;
			fixed3 Normal;
		};

        // This function remaps values from a input to a output range
        float map(float input, float inMin, float inMax, float outMin,  float outMax)
        {
            //inverse lerp with input range
            float relativeValue = (input - inMin) / (inMax - inMin);
            //lerp with output range
            return lerp(outMin, outMax, relativeValue);
        }

		//random float
		float rand(float2 co)
		{
			return frac((sin( dot(co.xy , float2(12.345 * _Time.w, 67.890 * _Time.w) )) * 12345.67890+_Time.w));
        }
           


		//our lighting function. Will be called once per light
		float4 LightingHalftone(HalftoneSurfaceOutput s, float3 lightDir, float atten) {
			//how much does the normal point towards the light?
			float towardsLight = dot(s.Normal, lightDir);
			//remap the value from -1 to 1 to between 0 and 1
			towardsLight = towardsLight * 0.5 + 0.5;
			//combine shadow and light and clamp the result between 0 and 1
			float lightIntensity = saturate(towardsLight * atten).r;

			float2 currentCoords = s.ScreenPos + float2(rand(1), rand(1));

			//get noise value
            float noiseValue = tex2D(_NoisePattern, currentCoords).r;
			float halftoneValue = tex2D(_HalftonePattern, currentCoords).r * noiseValue;

			halftoneValue = 1 - halftoneValue;

            //make lightness binary between fully lit and fully shadow based on halftone pattern (with a bit of antialiasing between)
            halftoneValue = map(halftoneValue, _RemapInputMin, _RemapInputMax, _RemapOutputMin, _RemapOutputMax);
            float halftoneChange = fwidth(halftoneValue) * 0.5;
			

			//lightIntensity = smoothstep(halftoneValue - halftoneChange, halftoneValue + halftoneChange, lightIntensity);
			
			lightIntensity = 1 - step(smoothstep(halftoneValue - halftoneChange, halftoneValue + halftoneChange, lightIntensity *_LightColor0.rgb), _Step);

			//intensity we calculated previously, diffuse color, light falloff and shadowcasting, color of t;
			//in case we want to make the shader transparent in the future - irrelevant right now


			return lightIntensity.xxxx;
		}

		//input struct which is automatically filled by unity
		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};

		//the surface shader function which sets parameters the lighting function then uses
		void surf(Input i, inout HalftoneSurfaceOutput o) {

			o.Albedo = float3(0,0,0);

			o.Emission = _Emission;

            //setup screenspace UVs for lighing function
			float aspect = _ScreenParams.x / _ScreenParams.y;
			o.ScreenPos = i.screenPos.xy / i.screenPos.w;
			o.ScreenPos = TRANSFORM_TEX(o.ScreenPos, _HalftonePattern);
			o.ScreenPos.x = o.ScreenPos.x * aspect;
		}
		ENDCG
	}
		FallBack "Standard"
}
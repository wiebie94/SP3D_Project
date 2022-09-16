Shader "Custom/LightsDissolve"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        [HDR] _EmissionColor("EmissionColor", Color) = (0,0,0)
        _NoiseTex ("Noise texture", 2D) = "white" {}
        _Vanishing ("Vanishing", Range(0,1)) = 0.0

        _LightBandColor ("LightBandColor", color) = (0,0,0,0)
        _LightBandThreshold ("LightBandThreshold", Range(0,1)) = 0.0


    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "IgnoreProjector" = "True" "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0 alpha:fade

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _EmissionColor;


        sampler2D _NoiseTex;
        fixed4 _LightBandColor;
        half _LightBandThreshold;
        half _Vanishing;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            half diss = tex2D(_NoiseTex, IN.uv_NoiseTex).r;
            clip(diss - _Vanishing);    //wenn kleiner als 0 wird der Pixel verworfen

            // Albedo comes from a texture tinted by color 
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
        
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            o.Alpha = c.a;
            //o.Emission = _EmissionColor*(_LightBandColor * step(diss - _Vanishing, _LightBandThreshold)) * 4;      //step prueft ob der threshold groesser ist wenn ja wird step() = 1, ansonsten  step() = 0
            o.Emission = _EmissionColor +(_LightBandColor * step(diss - _Vanishing, _LightBandThreshold)) * 20;
            //o.Emission = _EmissionColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

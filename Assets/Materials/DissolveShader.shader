Shader "Custom/NewSurfaceShader"
{
Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _SpecGlossMap("Roughness Map", 2D) = "white" {}
        _NoiseTex ("Noise texture", 2D) = "white" {}
    
    
        // Für Dissolve-Effekt
        _Vanishing ("Vanishing", Range(0,1)) = 0.0
        _LightBandColor ("LightBandColor", color) = (0,0,0,0)
        _LightBandThreshold ("LightBandThreshold", Range(0,1)) = 0.0
}
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf StandardSpecular
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0 alpha:fade
     
        sampler2D _MainTex;
        sampler2D _SpecGlossMap;
        sampler2D _NoiseTex;
 
        struct Input {
            float2 uv_MainTex;
            float2 uv_SpecGlossMap;
            float2 uv_NoiseTex;

            float3 viewDir;
        };

        fixed4 _LightBandColor;
        half _LightBandThreshold;
        half _Vanishing;
     
        half _Glossiness;
        fixed4 _Color;
 
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
 
        void surf (Input IN, inout SurfaceOutputStandardSpecular o) {


            half diss = tex2D(_NoiseTex, IN.uv_NoiseTex).r;
            clip(diss - _Vanishing);    //wenn kleiner als 0 wird der Pixel verworfen

            
            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            // Metallic and smoothness come from slider variables
            fixed4 cSpec = tex2D(_SpecGlossMap, IN.uv_SpecGlossMap);
            
            o.Albedo = c.rgb;
            o.Smoothness = _Glossiness * cSpec.a;
            o.Specular = cSpec.rgb;
                   
            o.Alpha = c.a;
             o.Emission = (_LightBandColor * step(diss - _Vanishing, _LightBandThreshold)) * 4;
        }
     
        ENDCG
    }
//    FallBack "Diffuse"
}
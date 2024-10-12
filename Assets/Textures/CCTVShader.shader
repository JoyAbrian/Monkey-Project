Shader "Custom/CCTVShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5 // Intensity of scanlines
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.2        // Intensity of noise
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _ScanlineIntensity;
        float _NoiseIntensity;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Sample the texture and multiply by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Convert to grayscale using luminance approximation (weighted sum of RGB)
            float grayscale = dot(c.rgb, float3(0.3, 0.59, 0.11));

            // Add scanlines effect by multiplying with a sine wave based on the vertical UV coordinate
            float scanlineEffect = sin(IN.uv_MainTex.y * 800) * _ScanlineIntensity;
            grayscale *= 1.0 - scanlineEffect;

            // Add noise effect (simulate analog signal noise)
            float randomNoise = frac(sin(dot(IN.uv_MainTex * _NoiseIntensity, float2(12.9898, 78.233))) * 43758.5453);
            grayscale += (randomNoise - 0.5) * _NoiseIntensity;

            // Apply grayscale color to albedo
            o.Albedo = grayscale.xxx;

            // Set other PBR parameters
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
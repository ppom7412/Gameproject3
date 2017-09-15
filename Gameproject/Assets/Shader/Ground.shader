Shader "Custom/Ground" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Metallic ("Metallic", Range(0, 1)) = 0.0
		_Glossiness ("Smoothness", Range(0, 1)) = 0.0
		_Emission ("Emission", Range(0, 1)) = 0.0
        _MainTex ("Base (RGB)", 2D) = "white" {}

    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-2" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
   		fixed4 _Color;
   		half _Metallic;
   		half _Glossiness;
   		half _Emission;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            //half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
            o.Smoothness = _Glossiness;
            o.Emission = _Emission;
            o.Metallic = _Metallic;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}

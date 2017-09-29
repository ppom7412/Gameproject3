Shader "Custom/Circle" {
	Properties{
		_BackGruondColor("BackGruond Color", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_CircleColor("Circle Color", Color) = (1,1,1,1)
		_MaxRadius("Max Radius", Range(0, 100)) = 1
		_CreatePoint("Circle CreatePoint", Vector) = (0,0,0,0)
		_Cutoff     ("Cutoff"      , Range(0, 1)) = 0.2
	}
	SubShader{

		// 배경묘사
		Tags{ "RenderType"="Opaque" }

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		half _Glossiness;
		half _Metallic;
		half3 _BackGruondColor;

		struct Input {
		float3 worldPos;
		};
		void surf(Input IN, inout SurfaceOutputStandard o) {
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Albedo = _BackGruondColor;
		}
		ENDCG

		// 원의 묘사
		Tags {
		"Queue"      = "AlphaTest"
		"RenderType" = "TransparentCutout"
		}

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alphatest:_Cutoff
		#pragma target 3.0

		struct Input {
		float3 worldPos;
		};

		half _MaxRadius;
		half3 _CircleColor;
		fixed3 _CreatePoint;

		//float3 testPoint[1] = {0};

		void surf(Input IN, inout SurfaceOutputStandard o) {

			float radius = 1 + (float)_Time * 60;
			float maxRadius = _MaxRadius;


			float dist = distance(_CreatePoint, IN.worldPos);

			// 원 생성
			if (radius < maxRadius && radius < dist && dist < radius + 0.03) {
				o.Albedo = _CircleColor;
				o.Alpha = 0.3;
			}
			else {
				o.Alpha = 0.1;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}

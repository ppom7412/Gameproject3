Shader "Custom/Circle" {
	Properties{
		_BackGruondColor("BackGruondColor", Color) = (1,1,1,1)
		_CircleColor("CircleColor", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader{
		Tags{ "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
#pragma surface surf Standard fullforwardshadows
#pragma target 3.0

		struct Input {
		float3 worldPos;
	};
		half _Glossiness;
		half _Metallic;
		fixed4 _BackGruondColor;
		fixed4 _CircleColor;

	// 원이 한개
	void surf(Input IN, inout SurfaceOutputStandard o) {

		
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		

		float dist = distance(fixed3(-5, 0, -5), IN.worldPos);
		float radius = 2 + _Time * 50;
		float maxRadius = 10;

		// 원 생성
		if (radius < maxRadius && radius < dist && dist < radius + 0.1) {
			o.Albedo = _CircleColor;
		}
		else {
			o.Albedo = _BackGruondColor;
		}

		
	}

	//// 원이 여러개
	//void surf(Input IN, inout SurfaceOutputStandard o) {
	//	float dist = distance(fixed3(0,0,0), IN.worldPos);	// (0,0,0) 을 중심으로 IN의 월드자표와의 거리
	//	float val = abs(sin(dist*1.0 - _Time * 50));		// sin 함수의 절대치 - _Time, Time의 크기가 퍼지는 속도, dist의 배율이 밀집도
	//	
	//	
	//	if (val > 0.98) {
	//		o.Albedo = fixed4(1, 1, 1, 1);
	//	}
	//	else {
	//		o.Albedo = fixed4(0,0, 0, 1);
	//	}
	//}
	ENDCG
	}
		FallBack "Diffuse"
}
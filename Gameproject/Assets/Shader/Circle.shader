Shader "Custom/Circle" {
	Properties{
		_BackGruondColor("BackGruond Color", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_CircleColor("Circle Color", Color) = (1,1,1,1)
		_EnemyCircleColor("EnemyCircle Color", Color) = (1,1,1,1)
		_MaxRadius("Max Radius", Range(0, 100)) = 1
		//_CreatePoint("Circle CreatePoint", Vector) = (0,0,0,0)
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
		half3 _EnemyCircleColor;
		fixed4 _CreatePoint;

		float dist;

		// 원 생성포인트 좌표,활성화여부,플레이어여부 배열및 그 길이
		int pointArrayLength = 10;
		uniform float activeArray[10];
		uniform float enemyArray[10];
		uniform float radiusArray[10];
		uniform float subRadiusArray[10];
		uniform fixed4 pointArray[10];

		void surf(Input IN, inout SurfaceOutputStandard o) {

			o.Alpha = 0.1;
			/*float radius = 1 + (float)_Time * 120;*/
			float maxRadius = _MaxRadius;

			for(int i = 0; i < 10; i++)
			{
				_CreatePoint = pointArray[i];
				dist = distance(_CreatePoint, IN.worldPos);
				if (activeArray[i] == 1.0f&&radiusArray[i] < maxRadius && radiusArray[i] < dist && dist < subRadiusArray[i] ) {
					if(enemyArray[i] == 0.0f)
					{
						o.Albedo = _CircleColor;
						o.Alpha = 0.3;
					}
					else if(enemyArray[i] == 1.0f)
					{
						o.Albedo = _EnemyCircleColor;
						o.Alpha = 0.3;
					}
				}
			
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}

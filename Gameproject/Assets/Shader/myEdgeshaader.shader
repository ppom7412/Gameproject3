// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/edge" {

	Properties{										// Inspector Properties 
		_MainTex("Main Tex",   2D) = "white" {}     // texture (default = white)
		_Color("Main Color", Color) = (1, 1, 1, 1)  // color (default = white)
		_EdgeSize("Edge Size",  Float) = 1          // Edge Size
	}

	SubShader{
		// Tags : http://docs.unity3d.com/ja/current/Manual/SL-SubShaderTags.html
			Tags{
			"RenderType" = "Opaque"
			"Queue" = "Geometry"
		}

		//------------------------------------------------------------
		Pass{                                           // 1Pass  表側を描画
			Cull         Back                             // Back(裏) は非表示
			ZTest        LEqual                           // 深度バッファと比較(同等,近距離)
			
			CGPROGRAM                                     // Cgコード開始
			#include "UnityCG.cginc"                      // 基本セット
			#pragma target 3.0                            // Direct3D 9 上の Shader Model 3.0 にコンパイル

			#pragma vertex        vertFunc                // バーテックスシェーダーに vertFunc を使用
			#pragma fragment    fragFunc                  // フラグメントシェーダーに fragFunc を使用

			// Cgコード内で、使用する宣言
			float4 _Color;                                // color
			sampler2D _MainTex;                           // texture

			float4 _MainTex_ST;                           // uv

			struct v2f {                                  // vertex シェーダーと fragment シェーダーの橋渡し
				float4 pos      : SV_POSITION;
				float2 uv       : TEXCOORD0;
			};

			v2f vertFunc(appdata_tan v) {                 // Vertex Shader
				v2f o;
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.pos = UnityObjectToClipPos(v.vertex);    // (UNITY_MATRIX_MVP = model*view*projection) *頂点
				return o;
			}

			float4 fragFunc(v2f i) : COLOR{              // Fragment Shader
				return tex2D(_MainTex, i.uv.xy);
			}

		ENDCG                                         // Cgコード終了
		}
		//------------------------------------------------------------
		Pass{                                           // 2Pass  裏側を描画

			Cull         Front                            // Front(表) は非表示
			ZTest        Less                             // 深度バッファと比較(近距離)

			CGPROGRAM                                     // Cgコード

			#include "UnityCG.cginc"                      // 基本セット
			#pragma target 3.0                            // Direct3D 9 上の Shader Model 3.0 にコンパイル

			#pragma vertex        vertFunc                // バーテックスシェーダーに vertFunc を使用
			#pragma fragment    fragFunc                  // フラグメントシェーダーに fragFunc を使用

													  // Cgコード内で、使用する宣言
			float4 _Color;                                // color
			sampler2D _MainTex;                           // texture
			float _EdgeSize;

			float4 _MainTex_ST;                           // uv

			struct v2f {                                  // vertex シェーダーと fragment シェーダーの橋渡し
			float4 pos      : SV_POSITION;
			float2 uv       : TEXCOORD0;
		};

		v2f vertFunc(appdata_tan v) {                 // Vertex Shader
			v2f o;
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			float4 pos = UnityObjectToClipPos(v.vertex);                               // 頂点
			float4 normal = normalize(UnityObjectToClipPos(float4(v.normal, 0)));    // 法線
			float  edgeScale = _EdgeSize * 0.002;                                       // Edge スケール係数
			float4 addpos = normal * pos.w * edgeScale;                                 // 頂点座標拡張方向とスケール
			o.pos = pos + addpos;
			return o;
		}

		float4 fragFunc(v2f i) : COLOR{               // Fragment Shader
			float4 col = tex2D(_MainTex, i.uv.xy);      // テクスチャーから得る場合
			col = float4(0, 0, 1, 1);                   // わかりやすく青くしてます。
			return col;
		}

		ENDCG                                         // Cgコード終了
		}

	}

		FallBack "Transparent/Cutout/Diffuse"             // サポートされるサブシェーダーがない場合に使用を試みるシェーダー
}
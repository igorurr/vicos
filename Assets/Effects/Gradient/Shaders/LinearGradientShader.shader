Shader "vicos/ui/gradient/linear"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
     
        Blend SrcAlpha OneMinusSrcAlpha
        
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
            #pragma debug
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "./Helpers/LinearGradient.cginc"
			
			
			// белый холст Unity.UI.Image, на который ляжет градиент
			sampler2D _MainTex;
			
			// у каждого типа градиента свои параметры, сюда записываются параметры текущего градиента
			float _GradientParams[64];
			
			// количество точек в градиенте
			int _GradientCountPoints;
			
			// точки градиента записанные подряд со всеми их данными
			// у разных точек разное количество данных, а значит и разный размер занимают точки
			float _GradientPoints[1024];
			
			// указатель на позицию i точки в массиве _GradientPoints
			// например _GradientPointsPositions[5] = 18, это значит, что данные 5 точки в массиве _GradientPoints
			// начинаются на 18 позиции
			float _GradientPointsPositions[512];
			
			// тип i точки в градиенте, см. GradientPointType в GradientConstants.cs
			float _GradientTypePoints[256];
			

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return CalculateColorFromPointLinearGradient(
				    i.uv,
                    _GradientParams,
                    _GradientCountPoints,
                    _GradientPoints,
                    _GradientPointsPositions,
                    _GradientTypePoints
				);
			}
			ENDCG
		}
	}
}

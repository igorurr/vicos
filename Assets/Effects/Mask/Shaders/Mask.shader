Shader "vicos/ui/mask"
{
	Properties
	{
		[PerRendererData] _MaskTex ("_MaskTex", 2D) = "white" {}
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
		
		GrabPass
        {
            "_BackgroundTexture"
        }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			// маска
            sampler2D _MaskTex;
			
			// текстура фона полученная GrabPassом
            sampler2D _BackgroundTexture;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                float4 grabPos : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                o.grabPos = ComputeGrabScreenPos(o.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
                half4 maskColor = tex2D(_MaskTex, i.uv);
                half4 bgColor = tex2Dproj(_BackgroundTexture, i.grabPos);
                
                return float4( 
                    bgColor.r, 
                    bgColor.g, 
                    bgColor.b, 
                    maskColor.a
                );
                
                //return 1 - bgcolor;
			}
			ENDCG
		}
	}
}

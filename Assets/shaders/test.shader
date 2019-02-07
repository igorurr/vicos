// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
 
 Shader "Custom/Mobile/test" {
 	Properties
 	{
 		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
 		
 		_Tex2 ("Tex2", 2D) = "white" {}
 		_SizeImg ("SizeImg", Vector) = (0,0,100,100)
 	}
          SubShader
          {
             Tags {
                 "Queue"="Transparent"
                 "RenderType"="Transparent"
              }
              LOD 100
     
              Blend SrcAlpha OneMinusSrcAlpha
         
              Pass
              {
              CGPROGRAM
              
              #pragma vertex vert
              #pragma fragment frag
 			 #pragma target 4.5
              
              #include "UnityCG.cginc"
 			
 			 RWStructuredBuffer<float4> buffer : register(u1);
 			 
               float _Segments[1000];
              
              struct appdata {
                 float4 vertex: POSITION;
                 float2 uv: TEXCOORD0;
              };
              
              struct v2f {
                 float4 vertex: POSITION;
                 float4 vertexObj: TEXCOORD1;
                 float2 uv: TEXCOORD0;
              };
              
              sampler2D _MainTex;
              float4 _MainTex_TexelSize;
              
              sampler2D _Tex2;
              fixed4 _Color;
              float _DeltaSizeX;
              float _DeltaSizeY;
              float _SizeX;
              float _SizeY;
              float PixelSnap;
              
              v2f vert(appdata v) {
                 v2f o;
                 o.vertex =  UnityObjectToClipPos(v.vertex);
                 o.vertexObj =  v.vertex;
                 o.uv = v.uv;
                 return o;
              }
              
              float4 frag(v2f i): SV_Target{
              
                 fixed4 col1 = tex2D(_MainTex, i.uv);
                 
                 float2 aa = i.vertexObj.xy * _MainTex_TexelSize.xy;
                 
                 fixed4 col2 = tex2D(_Tex2, aa);
              
 				buffer[0] = float4(_MainTex_TexelSize.z, _MainTex_TexelSize.w, 0, 0 );   //write value to buffer
              
                 float4 color = float4(
                     col2.r, 
                     col2.g, 
                     col2.b, 
                     col2.a
                 );
                 return color;
              }
              
              ENDCG
          }
      }
      //Fallback "Diffuse"
 }
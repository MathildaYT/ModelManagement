Shader "Custom/shaderTest" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}

		_Contrast("Contrast", Range(0, 4)) = 2
		_Brightness("Brightness", Range(0, 2)) = 1
		_NightVisionColor("Night Vision Color", Color) = (1, 1, 1, 1)
		_RandomValue("RandomValue", Float) = 0
		_distortion("distortion", Float) = 0.2
		_scale("scale", Float) = 0.8
		_VignetteTex("Vignette Texture", 2D) = "white" {}
		_ScanLineTileTex("Scan Line Tile Texture", 2D) = "white" {}
		_ScanLineTileAmount("Scan Line Tile Amount", Float) = 4.0
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_NoiseXSpeed("Noise X Speed", Float) = 100.0
			_NoiseYSpeed("Noise Y Speed", Float) = 100.0
	}
		SubShader{
			Pass {
				Tags { "RenderType" = "Opaque" }
				LOD 200
					CGPROGRAM
	#pragma vertex vert_img
	#pragma fragment frag 
	#pragma fragmentoption ARB_precision_hint_fastest//使用这个标志可以fp16的对像素进行运算
	#include "UnityCG.cginc"

					uniform sampler2D _MainTex;
				uniform sampler2D _ScanLineTileTex;//扫描线效果的贴图
				//噪波贴图基于两种颜色或材质的交互创建曲面的随机扰动
				//通过对两种颜色随机混合,生成噪波效果
				uniform sampler2D _NoiseTex;//噪波贴图
				uniform sampler2D _VignetteTex;//装饰图案，小插图，此处为晕影贴图
				fixed _Contrast;//对比度
				fixed _Brightness;//亮度
				fixed _RandomValue;//随机值，用在噪波贴图随机uv扰动
				fixed _distortion;//扭曲
				fixed _scale;//屏幕比例
				fixed _ScanLineTileAmount;//扫描线数量
				fixed _NoiseXSpeed;//噪波x方向速度
				fixed _NoiseYSpeed;//噪波y方向速度
				fixed4 _NightVisionColor;//夜视镜颜色

				struct Input {
					float2 uv_MainTex;
				};
				float2 barrelDistortion(float2 coord)
				{
					float2 h = coord.xy - float2(0.5, 0.5);
					float r2 = h.x * h.x + h.y * h.y;
					float f = 1.0 + r2 * (_distortion * sqrt(r2));

					return f * _scale * h + 0.5;
				}

				fixed4 frag(v2f_img i/*像素信息*/) : COLOR// 片元着色函数
				{
					half2 distortedUV = barrelDistortion(i.uv);  //桶形畸变uv
					fixed4 renderTex = tex2D(_MainTex, distortedUV);
					fixed4 vignetteTex = tex2D(_VignetteTex, distortedUV); //晕影贴图



					//扫描线uv 可控扫描线数量
					half2 scanLinesUV = half2(i.uv.x * _ScanLineTileAmount, i.uv.y * _ScanLineTileAmount);//_ScanLineTileAmount大小无限制
					fixed4 scanLineTex = tex2D(_ScanLineTileTex, scanLinesUV);
					//噪波贴图uv
					half2 noiseUV = half2(i.uv.x + (_RandomValue * _SinTime.z * _NoiseXSpeed),i.uv.y + (_Time.x * _NoiseYSpeed));
					fixed4 noiseTex = tex2D(_NoiseTex, noiseUV);



					//lum = luminosity 亮度
					fixed lum = dot(fixed3(0.299, 0.587, 0.114), renderTex.rgb);
					lum += _Brightness;//加上可自控的亮度
					//饱和度调为零，变成黑白效果，再与夜视镜颜色混合
					fixed4 finalColor = (lum * 2) + _NightVisionColor;//



					finalColor = pow(finalColor, _Contrast);//对比度
					finalColor *= vignetteTex;//与晕影贴图混合
					finalColor *= scanLineTex * noiseTex;

					return finalColor;
				}
				ENDCG
			}
		}
			FallBack "Diffuse"
}
Shader "Toony Colors Pro 2/Examples/Water/Realistic" {
	Properties {
		[TCP2HelpBox(Warning,Make sure that the Camera renders the depth texture for this material to work properly.    You can use the script __TCP2_CameraDepth__ for this.)] [TCP2HeaderHelp(BASE, Base Properties)] _HColor ("Highlight Color", Vector) = (0.6,0.6,0.6,1)
		_SColor ("Shadow Color", Vector) = (0.3,0.3,0.3,1)
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
		[TCP2Separator] _RampThreshold ("Ramp Threshold", Range(0, 1)) = 0.5
		_RampSmooth ("Ramp Smoothing", Range(0.001, 1)) = 0.1
		[TCP2Separator] [TCP2HeaderHelp(WATER)] _Color ("Water Color (RGB) Opacity (A)", Vector) = (0.5,0.5,0.5,1)
		[Header(Foam)] _FoamSpread ("Foam Spread", Range(0.01, 5)) = 2
		_FoamStrength ("Foam Strength", Range(0.01, 1)) = 0.8
		_FoamColor ("Foam Color (RGB) Opacity (A)", Vector) = (0.9,0.9,0.9,1)
		[NoScaleOffset] _FoamTex ("Foam (RGB)", 2D) = "white" {}
		_FoamSmooth ("Foam Smoothness", Range(0, 0.5)) = 0.02
		_FoamSpeed ("Foam Speed", Vector) = (2,2,2,2)
		[Header(Depth based Transparency)] [PowerSlider(5.0)] _DepthAlpha ("Depth Alpha", Range(0.01, 10)) = 0.5
		_DepthMinAlpha ("Depth Min Alpha", Range(0, 1)) = 0.5
		[Header(Waves Normal Map)] [TCP2HelpBox(Info,There are two normal maps blended. The tiling offsets affect each map uniformly.)] _BumpMap ("Normal Map", 2D) = "bump" {}
		[PowerSlider(2.0)] _BumpScale ("Normal Scale", Range(0.01, 2)) = 1
		_BumpSpeed ("Normal Speed", Vector) = (0.2,0.2,0.3,0.3)
		[Header(Vertex Waves Animation)] _WaveSpeed ("Speed", Float) = 2
		_WaveHeight ("Height", Float) = 0.1
		_WaveFrequency ("Frequency", Range(0, 10)) = 1
		[TCP2Separator] [TCP2HeaderHelp(SPECULAR, Specular)] _SpecColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_Shininess ("Roughness", Range(0, 10)) = 0.1
		[TCP2Separator] [TCP2HeaderHelp(REFLECTION, Reflection)] _ReflRoughness ("Reflection Roughness", Range(0, 1)) = 0
		_ReflStrength ("Reflection Strength", Range(0, 1)) = 1
		_ReflStrength ("Reflection Strength", Range(0, 1)) = 1
		[TCP2Separator] [TCP2HeaderHelp(RIM, Rim)] _RimColor ("Rim Color", Vector) = (0.8,0.8,0.8,0.6)
		_RimMin ("Rim Min", Range(0, 1)) = 0.5
		_RimMax ("Rim Max", Range(0, 1)) = 1
		[TCP2Separator] [TCP2HeaderHelp(TRANSPARENCY)] [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlendTCP2 ("Blending Source", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlendTCP2 ("Blending Dest", Float) = 10
		[TCP2Separator] [HideInInspector] __dummy__ ("unused", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//CustomEditor "TCP2_MaterialInspector_SG"
}
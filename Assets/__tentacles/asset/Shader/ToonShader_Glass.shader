Shader "ToonShader_Glass" {
	Properties {
		[TCP2HeaderHelp(Base)] _BaseColor ("Color", Vector) = (1,1,1,1)
		[TCP2ColorNoAlpha] _HColor ("Highlight Color", Vector) = (0.75,0.75,0.75,1)
		[TCP2ColorNoAlpha] _SColor ("Shadow Color", Vector) = (0.328,0.328,0.328,1)
		_BaseMap ("Albedo", 2D) = "white" {}
		_Alpha ("Alpha", Range(0, 1)) = 0.5
		[Header(Albedo HSV)] [HideInInspector] __BeginGroup_ShadowHSV ("Albedo HSV", Float) = 0
		_HSV_H ("Hue", Range(-180, 180)) = 0
		_HSV_S ("Saturation", Range(-1, 1)) = 0
		_HSV_V ("Value", Range(-1, 1)) = 0
		[HideInInspector] __EndGroup ("Albedo HSV", Float) = 0
		[TCP2Separator] [TCP2HeaderHelp(Rim Lighting)] [TCP2ColorNoAlpha] _RimColor ("Rim Color", Vector) = (0.8,0.8,0.8,0.5)
		_RimMin ("Rim Min", Range(0, 2)) = 0.5
		_RimMax ("Rim Max", Range(0, 2)) = 1
		[TCP2Separator] [TCP2HeaderHelp(Outline)] _OutlineWidth ("Width", Range(0, 5)) = 1
		_OutlineColorVertex ("Color", Vector) = (0,0,0,1)
		[TCP2OutlineNormalsGUI] __outline_gui_dummy__ ("_unused_", Float) = 0
		[TCP2Separator] [ToggleOff(_RECEIVE_SHADOWS_OFF)] _ReceiveShadowsOff ("Receive Shadows", Float) = 1
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}
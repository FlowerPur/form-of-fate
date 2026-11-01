Shader "PixelErase_Ordered"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _GridSize("Pixel Grid Size", Float) = 32
        _Progress("Progress", Range(0, 1)) = 0
        
        // UI свойства...
        [HideInInspector] _StencilComp("Stencil Comparison", Float) = 8
        [HideInInspector] _Stencil("Stencil ID", Float) = 0
        [HideInInspector] _StencilOp("Stencil Operation", Float) = 0
        [HideInInspector] _StencilWriteMask("Stencil Write Mask", Float) = 255
        [HideInInspector] _StencilReadMask("Stencil Read Mask", Float) = 255
        [HideInInspector] _ColorMask("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        
        Stencil {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off Lighting Off ZWrite Off ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 localPos : TEXCOORD1;
                float4 worldPosition : TEXCOORD2;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _GridSize;
            float _Progress;

            v2f vert(appdata_t v) {
                v2f o;
                o.worldPosition = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.localPos = v.vertex.xy * _GridSize; // Масштабируем сразу здесь
                o.color = v.color * _Color;
                return o;
            }

            // Матрица Байера 4x4 для упорядоченного дизеринга
            float GetBayer4x4(float2 uv) {
                float2 pos = floor(frac(uv / 4.0) * 4.0);
                int index = int(pos.x) + int(pos.y) * 4;
                float m[16] = {
                     0,  8,  2, 10,
                    12,  4, 14,  6,
                     3, 11,  1,  9,
                    15,  7, 13,  5
                };
                return m[index] / 16.0;
            }

            fixed4 frag(v2f IN) : SV_Target {
                half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
                
                // Получаем значение из матрицы на основе координат пикселей
                float threshold = GetBayer4x4(IN.localPos);

                // Если прогресс больше порога матрицы — убираем пиксель
                if (_Progress > threshold) {
                    discard; // Для UI лучше использовать discard или clip
                }

                return color;
            }
            ENDCG
        }
    }
}
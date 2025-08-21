Shader "Custom/ScrollingRainbowLine"
{
    Properties
    {
        _Tint          ("Tint (RGBA)", Color) = (1,1,1,1)
        _RainbowRepeat ("Rainbow Repeat", Float) = 1.0
        _ScrollSpeed   ("Scroll Speed",   Float) = 0.6
        _EdgeSoftness  ("Edge Softness",  Range(0,0.5)) = 0.12
        _Glow          ("Glow Boost",     Range(0,3)) = 0.0
        _TilingU       ("Tiling U", Float) = 1.0
        _TilingV       ("Tiling V", Float) = 1.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        LOD 100

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Tint;
            float  _RainbowRepeat;
            float  _ScrollSpeed;
            float  _EdgeSoftness;
            float  _Glow;
            float  _TilingU;
            float  _TilingV;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            
            float3 HSVtoRGB(float3 hsv)
            {
                float H = hsv.x;
                float S = hsv.y;
                float V = hsv.z;

                float3 K = float3(1.0, 2.0/3.0, 1.0/3.0);
                float3 p = abs(frac(H + K) * 6.0 - 3.0);
                float3 a = saturate(p - 1.0);


                float3 rgb = V * (1.0 + (a - 1.0) * S);
                return rgb;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = float2(v.uv.x * _TilingU, v.uv.y * _TilingV);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Scroll along U
                float u = i.uv.x + _Time.y * _ScrollSpeed;
                float v = i.uv.y;

                // Rainbow hue along U
                float hue = frac(u * _RainbowRepeat);
                float3 rainbow = HSVtoRGB(float3(hue, 1.0, 1.0));

                // Soft vertical edges
                float aIn  = smoothstep(0.0, _EdgeSoftness, v);
                float aOut = smoothstep(0.0, _EdgeSoftness, 1.0 - v);
                float edgeMask = saturate(min(aIn, aOut));

                // Tint + optional glow boost
                float3 baseCol = rainbow * _Tint.rgb;
                baseCol += _Glow * baseCol;

                return fixed4(baseCol, _Tint.a * edgeMask);
            }
            ENDCG
        }
    }
    FallBack Off
}

Shader "Custom/WindowCurtain"
{
    Properties
    {
        _MaskSize ("MaskSize", Range(0, 1)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            fixed _MaskSize;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed min = 0.5 - 0.5 * _MaskSize;
                fixed max = 0.5 + 0.5 * _MaskSize;
                if(i.uv.x > min && i.uv.x < max && i.uv.y > min && i.uv.y < max)
                    discard;
                return fixed4(0.0, 0.0 ,0.0 ,0.0);
            }
            ENDCG
        }
    }
}

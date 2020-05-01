Shader "Hidden/PixelizationEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HRes ("Horizontal Resolution", Int) = 192
        _VRes ("Vertical Resolution", Int) = 128
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

            int _HRes;
            int _VRes;
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv.x = round(uv.x * _HRes) / _HRes;
                uv.y = round(uv.y * _VRes) / _VRes;
                fixed4 col = tex2D(_MainTex, uv);

                return col;
            }
            ENDCG
        }
    }
}

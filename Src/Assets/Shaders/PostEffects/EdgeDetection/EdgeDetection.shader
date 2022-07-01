Shader "MyShader/PostEffects/EdgeDetection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgesOnly ("Edges Only", Float) = 0.0
        _EdgeColor ("Edge Color", Color) = (0,0,0,1)
        _BackgroundColor ("Background Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            half4 _MainTex_TexelSize;
            fixed _EdgesOnly;
            fixed4 _EdgeColor;
            fixed4 _BackgroundColor;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv[9] : TEXCOORD0;
            };

            v2f vert (appdata_img v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                
                half2 uv = v.texcoord;
                o.uv[0] = uv + _MainTex_TexelSize.xy * half2(-1,-1);
                o.uv[1] = uv + _MainTex_TexelSize.xy * half2(0,-1);
                o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1,-1);
                o.uv[3] = uv + _MainTex_TexelSize.xy * half2(-1,0);
                o.uv[4] = uv + _MainTex_TexelSize.xy * half2(0,0);
                o.uv[5] = uv + _MainTex_TexelSize.xy * half2(1,0);
                o.uv[6] = uv + _MainTex_TexelSize.xy * half2(-1,1);
                o.uv[7] = uv + _MainTex_TexelSize.xy * half2(0,1);
                o.uv[8] = uv + _MainTex_TexelSize.xy * half2(1,1);

                return o;
            }

            fixed luminance(fixed4 color){
                return 0.2125 * color.r + 0.7154 * color.g + 0.0721 * color.b;
            }

            half Sobel(v2f i){
                const half Gx[9] = {-0,-1,-0,
                                    0, 0, 0,
                                    0, 1, 0};
                const half Gy[9] = {-0, 0, 0,
                                    -1, 0, 1,
                                    -0, 0, 0};
                half texColor;
                half edgeX = 0;
                half edgeY = 0;
                for(int it = 0;it<9;it++){
                    texColor = luminance(tex2D(_MainTex,i.uv[it]));
                    edgeX += texColor * Gx[it];
                    edgeY += texColor * Gy[it];
                }
                half edge = 1 - abs(edgeX) - abs(edgeY);
                return edge;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half edge = Sobel(i);
                fixed4 withEdgeColor = lerp(_EdgeColor,tex2D(_MainTex,i.uv[4]),edge);
                fixed4 onlyEdgeColor = lerp(_EdgeColor,_BackgroundColor,edge);
                return lerp(withEdgeColor,onlyEdgeColor,_EdgesOnly);
            }

            ENDCG
        }
    }
    FallBack Off
}

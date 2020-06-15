Shader "Custom/WindShader"
{
    Properties{
       _Color("Main Color", Color) = (1,1,1,1)
       _MainTex("Base (RGB)", 2D) = "white" {}
        _WindSpeed("Wind Speed", Range(0, 2)) = 0.1
        _Cutoff("Alpha cutoff", Range(0,1)) = 0.5
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200
            Cull Back

            CGPROGRAM
            #pragma surface surf Lambert alphatest:_Cutoff vertex:vert addshadow

            sampler2D _MainTex;
            uniform float _WindSpeed;
            fixed4 _Color;
            float CURVE_OFFSET;

            float2 Size = float2(256, 128);
            float2 Wave = float2(48, 5);

            float time = 1.0;

            float random(float2 xy)
            {
                return frac(sin(dot(xy, float2(12.9898, 78.233)))*43758.5453123);
            }

            void vert(inout appdata_full v)
            {
                float anim_Sequence = _Time * 0.1;
                float newValue = ((random(v.texcoord) - 0.5)*2) * anim_Sequence;

                v.vertex.x += sin(100 * newValue) / 100;
                v.vertex.y += sin(100 * newValue) / 100;

            }

            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) 
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;


            }

            ENDCG

    }

        Fallback "Diffuse"
}
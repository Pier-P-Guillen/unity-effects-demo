Shader "Custom/CellShader"
{
   Properties
   {
       _MainTex ("MainTex", 2D) = "white"{}
       [Toggle] _IsShaking("Is Shaking", Float) = 0
   }

   CGINCLUDE
   #include "UnityCG.cginc"

   float _IsShaking;

   // https://thebookofshaders.com/10/
   float rand(float2 st)
   {
      return frac(sin(dot(st, float2(12.9898, 78.233))) * 43758.5453);
   }

   float box(float2 st, float size)
   {
      size = 0.5 + size * 0.5;
      st = step(st, size) * step(1.0 - st, size);
      return st.x * st.y;
   }

   float box_size(float2 st, float n)
   {
      st = (floor(st * n) + 0.5) / n;
      float offs = rand(st) * 5;
      return (1 + sin(_Time.y * 3 + offs)) * 0.5;
   }

   float box_effect(float2 uv, float n)
   {
      float2 st = frac(uv * n);
      float size = box_size(uv, n);
      return box(st, size);
   }

   float4 colored_boxes(float2 uv)
   {
      float a = box_effect(uv, 5);
      float b = box_effect(uv, 8);
      float c = box_effect(uv, 13);

      float4 color1 = float4(0.106, 0.275, 0.478, 1);
      float4 color2 = float4(0.294, 0.482, 0.706, 1);
      float4 color3 = float4(0.047, 0.475, 0.988, 1);

      return (a * color1) + (b * color2) + (c * color3);
   }

   float circle(float2 st, float2 radius)
   {
      return step(radius, distance(0.5, st));
   }

   float4 distort(float2 uv)
   {
      float t = 2 * uv.y + sin(_Time.y * 5);
      float distort = sin(_Time.y * 5) * 0.1
                      * sin(5 * t) * (1 - (t - 1) * (t - 1));

      uv.x += distort; 
      return float4(circle(uv - float2(0, distort) * 0.3, 0.42),
                    circle(uv + float2(0, distort) * 0.3, 0.38),
                    circle(uv + float2(distort, 0) * 0.3, 0.40),
                    1);
   }

   float4 frag(v2f_img i) : SV_Target
   {
       float4 colorBox = colored_boxes(i.uv);
       float4 distortion = (_IsShaking == 0)
          ? float4(1, 1, 1, 1)
          : distort(i.uv) * 0.9 + 0.1;
       return colorBox * distortion;
   }
   ENDCG

   SubShader
   {
       Pass
       {
           CGPROGRAM
           #pragma vertex vert_img
           #pragma fragment frag
           ENDCG
       }
   }
}

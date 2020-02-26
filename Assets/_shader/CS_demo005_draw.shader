Shader "Custom/CS_demo005_draw"
{
    SubShader
    {
        // Tags { "RenderType"="Opaque" }
        // LOD 100

        Pass
        {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }

            CGPROGRAM
			#pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag
			// #pragma enable_d3d11_debug_symbols

            #include "UnityCG.cginc"

			struct Vert
			{
				float3 position;
				float3 color;
			};

			uniform StructuredBuffer<Vert> buffer;

            struct v2f
            {
                float4 pos : SV_POSITION;
				float3 col : COLOR;
            };

            v2f vert (uint id : SV_VertexID)
            {
				Vert vin = buffer[id];

                v2f o;
                o.pos = UnityObjectToClipPos(float4(vin.position, 1));
				o.col = vin.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.col, 1);
            }
            ENDCG
        }
    }
}

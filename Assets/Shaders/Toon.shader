Shader "Custom/Toon"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white" {}
        _Tint ("Tint", Color) = (1,1,1,1)
        _AlbedoAmount ("Albedo Amount", Range(0,1)) = 1
        _Color ("Main Color", Color) = (1,1,1,1)
        _ShadeColor ("Shade Color", Color) = (1,1,1,1)
        _ShadowColor ("Shadow Color", Color) = (1,1,1,1)
        _ShadowIntensity ("ShadowIntensity", Float) = 1
        _SpecColor ("Specular Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Float) = 10
    }
 
    SubShader
    {
        Pass 
        {
            Tags { "LightMode" = "ForwardBase"} 
 
            CGPROGRAM
 
            #pragma multi_compile_fwdbase
            #pragma vertex vert             
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform half4 _Color, _Tint;
            uniform half4 _ShadeColor;
            uniform half4 _ShadowColor;
            uniform float4 _LightColor0;
            uniform half4 _SpecColor;
            uniform float _Shininess;
            float _ShadowIntensity, _AlbedoAmount;
 
            struct vertInput
            {
                float4 pos : POSITION;
                float3 nor : NORMAL;
                float2 uv : TEXCOORD0;

            };  
 
            struct vertOutput
            {
                float4 pos : SV_POSITION;   
                float3 nor : NORMAL;            
                // half4 col : COLOR; 
                float4 uv : TEXCOORD0;  
                LIGHTING_COORDS(0,1)       
            };
 
            vertOutput vert(vertInput input)
            {
                vertOutput o;
 
                float4 normal = float4(input.nor, 0.0);
                float3 n = normalize(mul(unity_ObjectToWorld, normal));
                float3 l = _WorldSpaceLightPos0;
                float4 a = UNITY_LIGHTMODEL_AMBIENT * _Color;
                float3 v = normalize(_WorldSpaceCameraPos);
 
                float3 NdotL = max(0.0, dot(n, l));
                int shadingSteps = 4;
                float nDotL  = floor(max(0.0, dot(n, l))*shadingSteps)/shadingSteps;
 
                float3 d = NdotL * _LightColor0 * _Color;
                float3 r = reflect(-l, n);
                float RdotV = max(0.0, dot(r, v));
                float3 s = float3(0,0,0);
                if (dot(n, l) > 0.0) 
                    s = _LightColor0 * _SpecColor * pow(RdotV, _Shininess);
                float4 c = float4(d+a+s, 1.0);

                // o.col = nDotL * _Color + a;
                o.pos = UnityObjectToClipPos(input.pos);
                o.nor = n;
                o.uv.xy = TRANSFORM_TEX(input.uv, _MainTex);
                TRANSFER_VERTEX_TO_FRAGMENT(o);               

                return o;
            }

            float4 SaturateColor(float4 startColor, float _Saturation)
            {
                float4 outputColor = startColor;
                float3 intensity = dot(outputColor.rgb, float3(0.299,0.587,0.114));
                outputColor.rgb = lerp(intensity, outputColor.rgb, _Saturation);
                outputColor.rgb = intensity; 
                return outputColor;
            }
 
            half4 frag(vertOutput input) : COLOR
            {   
                int shadingSteps = 1000;
                float3 l = _WorldSpaceLightPos0;
                float nDotL = floor(max(0.0, dot(input.nor, l)+1)*shadingSteps)/shadingSteps;
                // nDotL = dot(input.nor, l)

                // float nDotL = max(0.0, dot(input.nor, l));
                float p = 0.4f;
                float nnDotL = dot(l, input.nor);
                // nDotL *= p;

                float attenuation = floor(LIGHT_ATTENUATION(input)*shadingSteps)/shadingSteps;
                attenuation = attenuation  * p + (1.0 - p);

                float3 AO = tex2D(_MainTex, input.uv.xy).rgb + (1-_AlbedoAmount);
                float3 negativeAO = lerp((float3(1,1,1) - tex2D(_MainTex, input.uv.xy).rgb) * _Tint ,float4(1,1,1,1), _AlbedoAmount);
                float3 shadedColor = lerp(_ShadeColor.rgb, _Color.rgb, nnDotL*0.5+0.5);
                float3 shadedAO = AO * shadedColor;
                float3 shadowedColor = float4(lerp(_ShadowColor, shadedAO, pow(attenuation, (_ShadowIntensity)) ), 0);
                float4 c = float4(shadowedColor,1);
                return c; 
            }
 
            ENDCG
        }
    }
    Fallback "VertexLit"
}
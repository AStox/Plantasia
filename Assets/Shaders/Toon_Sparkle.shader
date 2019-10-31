Shader "Custom/Toon_Sparkles"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _ShadeColor ("Shade Color", Color) = (1,1,1,1)
        _ShadowColor ("Shadow Color", Color) = (1,1,1,1)
        _SpecColor ("Specular Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Float) = 10
        [NoScaleOffset] _SparkleTex ("Sparkle", 2D) = "black" {}
        _SparkleScale ("Sparkle Scale", Range (0, 2)) = 0.1
        _SlideMultiplier ("Slide Multiplier", Range (0,2)) = 0.1
        _SparkleMovement ("Movement", Range (0,2)) = 1
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
            uniform half4 _Color;
            uniform half4 _ShadeColor;
            uniform half4 _ShadowColor;
            uniform float4 _LightColor0;
            uniform half4 _SpecColor;
            uniform float _Shininess;
            sampler2D _SparkleTex;
            float _SparkleScale;
            float _SlideMultiplier;
            float _SparkleMovement;
 
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
                float4 worldPos : TEXCOORD2;
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

                o.worldPos = mul(unity_ObjectToWorld, input.pos);      

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
                nDotL = nDotL * p + (1.0 - p);
                float nnDotL = dot(l, input.nor);
                // nDotL *= p;
                float4 a = UNITY_LIGHTMODEL_AMBIENT * _Color;
                float4 albedo = (tex2D(_MainTex, input.uv.xy).rgb * _Color.rgb,0);
                float4 shadowAlbedo = (tex2D(_MainTex, input.uv.xy).rgb * _ShadowColor.rgb,0);
                // float attenuation = LIGHT_ATTENUATION(input);
                float attenuation = floor(LIGHT_ATTENUATION(input)*shadingSteps)/shadingSteps;
                attenuation = attenuation  * p + (1.0 - p);
                float4 c = attenuation *  nDotL * _Color + a;

                float3 viewDir = input.worldPos - _WorldSpaceCameraPos.xyz;
                // float distance = smoothstep(_Sparkle
                viewDir = normalize(viewDir);
                
                float spec = (tex2D(_SparkleTex, input.worldPos.xz * _SparkleScale + viewDir.xz * _SlideMultiplier + _Time.xx * _SparkleMovement).r) * (tex2D(_SparkleTex, input.worldPos.xz * _SparkleScale - viewDir.xz * _SlideMultiplier - _Time.xx * _SparkleMovement).g);

                c = attenuation * nDotL * _LightColor0 * albedo + (1-attenuation) * nDotL * shadowAlbedo;
                float3 AO = tex2D(_MainTex, input.uv.xy).rgb;
                
                c = float4(lerp(_ShadowColor, AO * lerp(_ShadeColor.rgb, _Color.rgb, nnDotL*0.5+0.5), attenuation), 0)+(float4(spec,spec,spec,0)*5);
                return c; 
            }
 
            ENDCG
        }
    }
    Fallback "VertexLit"
}
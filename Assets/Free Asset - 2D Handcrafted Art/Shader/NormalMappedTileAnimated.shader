
Shader "Custom/NormalMappedTileAnimated_URP"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        [PerRendererData] _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MyNormalMap ("My Normal Map", 2D) = "bump" {}
        _EmissiveMap ("Emissive Map", 2D) = "black" {}

        [HideInInspector] _RendererColor ("Renderer Color", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)

        [Toggle(EMISSIVE_TEXTURE)]
        _EnabledEmissive ("Use Emissive?", Float) = 0

        [Space]
        _RandOffset ("Unique Offset", Range(0,1)) = 1
        _WindDir ("Wind Direction", Range(-1,1)) = 1
        _BendScale ("Bend Scale", Range(0,1)) = 1
        _SwayFreq ("Sway Freq", Range(0,20)) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "DisableBatching" = "True"
        }

        LOD 200
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM

            #pragma target 3.0
            #pragma vertex Vert
            #pragma fragment Frag

            #pragma shader_feature_local _ EMISSIVE_TEXTURE

            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_MyNormalMap);
            SAMPLER(sampler_MyNormalMap);

            TEXTURE2D(_EmissiveMap);
            SAMPLER(sampler_EmissiveMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float4 _RendererColor;
                float4 _Flip;

                float4 _MainTex_ST;
                float4 _MyNormalMap_ST;
                float4 _EmissiveMap_ST;

                float _EnabledEmissive;
                float _RandOffset;
                float _WindDir;
                float _BendScale;
                float _SwayFreq;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;

                float2 uv : TEXCOORD0;
                float2 normalUV : TEXCOORD1;
                float2 emissiveUV : TEXCOORD2;

                float3 positionWS : TEXCOORD3;
                float3 normalWS : TEXCOORD4;
                float4 tangentWS : TEXCOORD5;

                half4 color : COLOR;
                half fogFactor : TEXCOORD6;
                float4 shadowCoord : TEXCOORD7;

                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            // Preserve the original shader's approximate triangle-wave sway.
            float CubicSmooth(float x)
            {
                return x * x * (3.0 - 2.0 * x);
            }

            float TriangleWave(float x)
            {
                return abs(frac(x + _RandOffset + 0.5) * 2.0 - 1.0);
            }

            float SineApproximation(float x)
            {
                return CubicSmooth(TriangleWave(x));
            }

            // Wind deformation adapted from the original vertex function.
            float3 AnimateVertex(float3 positionOS)
            {
                float3 vPos = positionOS;
                float fLength = length(vPos);

                float fBF =
                    vPos.y * _BendScale *
                    ((SineApproximation(_Time.y * _SwayFreq) + 0.5) * 0.5);

                fBF += 1.0;
                fBF *= fBF;
                fBF = fBF * fBF - fBF;

                float3 vNewPos = vPos;
                vNewPos.x += _WindDir * fBF * saturate(vPos.y);

                float2 newXY = normalize(vNewPos.xy);
                vPos.xy = newXY * fLength;

                return vPos;
            }

            Varyings Vert(Attributes IN)
            {
                Varyings OUT = (Varyings)0;

                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                // Preserve SpriteRenderer flipping behavior.
                IN.positionOS.xy *= _Flip.xy;

                // Preserve wind animation.
                IN.positionOS.xyz = AnimateVertex(IN.positionOS.xyz);

                VertexPositionInputs positionInputs =
                    GetVertexPositionInputs(IN.positionOS.xyz);

                VertexNormalInputs normalInputs =
                    GetVertexNormalInputs(IN.normalOS, IN.tangentOS);

                OUT.positionCS = positionInputs.positionCS;
                OUT.positionWS = positionInputs.positionWS;
                OUT.normalWS = normalInputs.normalWS;

                OUT.tangentWS = float4(
                    normalInputs.tangentWS,
                    IN.tangentOS.w * GetOddNegativeScale()
                );

                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.normalUV = TRANSFORM_TEX(IN.uv, _MyNormalMap);
                OUT.emissiveUV = TRANSFORM_TEX(IN.uv, _EmissiveMap);

                // Apply object tint and SpriteRenderer tint.
                OUT.color = IN.color * _Color * _RendererColor;

                OUT.fogFactor = ComputeFogFactor(OUT.positionCS.z);
                OUT.shadowCoord =
                    TransformWorldToShadowCoord(OUT.positionWS);

                return OUT;
            }

            half4 Frag(Varyings IN, FRONT_FACE_TYPE face : FRONT_FACE_SEMANTIC)
                : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                half4 albedo = SAMPLE_TEXTURE2D(
                    _MainTex, sampler_MainTex, IN.uv
                ) * IN.color;

                // Preserve transparency.
                half alpha = albedo.a;

                half3 normalTS = UnpackNormal(
                    SAMPLE_TEXTURE2D(
                        _MyNormalMap,
                        sampler_MyNormalMap,
                        IN.normalUV
                    )
                );

                // Keep double-sided cards lit consistently on their back faces.
                if (!IS_FRONT_VFACE(face, true, false))
                {
                    normalTS.z = -normalTS.z;
                }

                half3 normalWS = normalize(IN.normalWS);
                half3 tangentWS = normalize(IN.tangentWS.xyz);

                half3 bitangentWS = normalize(
                    cross(normalWS, tangentWS) * IN.tangentWS.w
                );

                half3x3 tangentToWorld = half3x3(
                    tangentWS,
                    bitangentWS,
                    normalWS
                );

                normalWS = normalize(
                    TransformTangentToWorld(normalTS, tangentToWorld)
                );

                // Preserve the original black-pixel outline workaround.
                bool isBlack = length(albedo.rgb) < 0.001;

                if (isBlack)
                {
                    normalWS = normalize(IN.normalWS);
                    albedo.rgb = half3(0, 0, 0);
                }

                InputData inputData = (InputData)0;

                inputData.positionWS = IN.positionWS;
                inputData.normalWS = normalWS;
                inputData.viewDirectionWS =
                    GetWorldSpaceNormalizeViewDir(IN.positionWS);

                inputData.shadowCoord = IN.shadowCoord;
                inputData.fogCoord = IN.fogFactor;
                inputData.vertexLighting =
                    VertexLighting(IN.positionWS, normalWS);

                inputData.bakedGI = SampleSH(normalWS);
                inputData.normalizedScreenSpaceUV =
                    GetNormalizedScreenSpaceUV(IN.positionCS);

                inputData.shadowMask = half4(1, 1, 1, 1);

                SurfaceData surfaceData = (SurfaceData)0;

                surfaceData.albedo = albedo.rgb;
                surfaceData.metallic = 0.0;
                surfaceData.specular = half3(0.0, 0.0, 0.0);
                surfaceData.smoothness = 0.0;
                surfaceData.normalTS = normalTS;
                surfaceData.occlusion = 1.0;
                surfaceData.alpha = alpha;

                #if defined(EMISSIVE_TEXTURE)
                    surfaceData.emission = SAMPLE_TEXTURE2D(
                        _EmissiveMap,
                        sampler_EmissiveMap,
                        IN.emissiveUV
                    ).rgb;
                #else
                    surfaceData.emission = half3(0, 0, 0);
                #endif

                // Keep black pixels black instead of illuminating them.
                if (isBlack)
                {
                    surfaceData.albedo = half3(0, 0, 0);
                    surfaceData.emission = half3(0, 0, 0);
                }

                half4 color = UniversalFragmentPBR(
                    inputData,
                    surfaceData
                );

                color.rgb = MixFog(color.rgb, IN.fogFactor);
                color.a = alpha;

                return color;
            }

            ENDHLSL
        }
    }

    FallBack Off
}

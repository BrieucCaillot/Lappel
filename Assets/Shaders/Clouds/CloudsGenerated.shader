Shader "PBR Master"
{
    Properties
    {
        Vector4_F4396F31("RotateProjection", Vector) = (1, 0, 0, 0)
        Vector1_5FE82E3A("Noise Scale", Float) = 10
        Vector1_4F738604("Noise Speed", Float) = 0.1
        Vector1_F4059B8F("Noise Height", Float) = 2
        Vector4_C43B424F("Noise Remap", Vector) = (0, 1, -1, 1)
        Color_58B36FA2("Color Peak", Color) = (0, 0, 0, 0)
        Color_C9E01EB("Color Valley", Color) = (1, 1, 1, 0)
        Vector1_89FF875A("Noise Edge 1", Float) = 0
        Vector1_13F963C("Noise Edge 2", Float) = 1
        Vector1_CFF9FCE("Noise Power Strength", Float) = 1
        Vector1_65331915("Base Scale", Float) = 5
        Vector1_4E54445F("Base Speed", Float) = 0.2
        Vector1_3EAF4E50("Base Strength", Float) = 2
        Vector1_7DCD71B1("Emission Strength", Float) = 2
        Vector1_D98ACD8A("Curvature Radius", Float) = 1
        Vector1_C4C61918("Fresnel Power", Float) = 1
        Vector1_21501071("Fresnel Opacity", Float) = 1
        Vector1_E502573B("Fade Depth", Float) = 100
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "Queue"="Transparent+0"
        }
        
        Pass
        {
            Name "Universal Forward"
            Tags 
            { 
                "LightMode" = "UniversalForward"
            }
           
            // Render State
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZTest LEqual
            ZWrite On
            // ColorMask: <None>
            
        
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            // Pragmas
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
        
            // Keywords
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS _ADDITIONAL_OFF
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            // GraphKeywords: <None>
            
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define VARYINGS_NEED_POSITION_WS 
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_VIEWDIRECTION_WS
            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define FEATURES_GRAPH_VERTEX
            #pragma multi_compile_instancing
            #define SHADERPASS_FORWARD
            #define REQUIRE_DEPTH_TEXTURE
            
        
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
            float4 Vector4_F4396F31;
            float Vector1_5FE82E3A;
            float Vector1_4F738604;
            float Vector1_F4059B8F;
            float4 Vector4_C43B424F;
            float4 Color_58B36FA2;
            float4 Color_C9E01EB;
            float Vector1_89FF875A;
            float Vector1_13F963C;
            float Vector1_CFF9FCE;
            float Vector1_65331915;
            float Vector1_4E54445F;
            float Vector1_3EAF4E50;
            float Vector1_7DCD71B1;
            float Vector1_D98ACD8A;
            float Vector1_C4C61918;
            float Vector1_21501071;
            float Vector1_E502573B;
            CBUFFER_END
        
            // Graph Functions
            
            void Unity_Distance_float3(float3 A, float3 B, out float Out)
            {
                Out = distance(A, B);
            }
            
            void Unity_Divide_float(float A, float B, out float Out)
            {
                Out = A / B;
            }
            
            void Unity_Power_float(float A, float B, out float Out)
            {
                Out = pow(A, B);
            }
            
            void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }
            
            void Unity_Rotate_About_Axis_Degrees_float(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                Rotation = radians(Rotation);
            
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;
                
                Axis = normalize(Axis);
            
                float3x3 rot_mat = { one_minus_c * Axis.x * Axis.x + c,            one_minus_c * Axis.x * Axis.y - Axis.z * s,     one_minus_c * Axis.z * Axis.x + Axis.y * s,
                                          one_minus_c * Axis.x * Axis.y + Axis.z * s,   one_minus_c * Axis.y * Axis.y + c,              one_minus_c * Axis.y * Axis.z - Axis.x * s,
                                          one_minus_c * Axis.z * Axis.x - Axis.y * s,   one_minus_c * Axis.y * Axis.z + Axis.x * s,     one_minus_c * Axis.z * Axis.z + c
                                        };
            
                Out = mul(rot_mat,  In);
            }
            
            void Unity_Multiply_float(float A, float B, out float Out)
            {
                Out = A * B;
            }
            
            void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
            {
                Out = UV * Tiling + Offset;
            }
            
            
            float2 Unity_GradientNoise_Dir_float(float2 p)
            {
                // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }
            
            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            { 
                float2 p = UV * Scale;
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
            }
            
            void Unity_Add_float(float A, float B, out float Out)
            {
                Out = A + B;
            }
            
            void Unity_Saturate_float(float In, out float Out)
            {
                Out = saturate(In);
            }
            
            void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
            {
                RGBA = float4(R, G, B, A);
                RGB = float3(R, G, B);
                RG = float2(R, G);
            }
            
            void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            void Unity_Absolute_float(float In, out float Out)
            {
                Out = abs(In);
            }
            
            void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
            {
                Out = smoothstep(Edge1, Edge2, In);
            }
            
            void Unity_Add_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A + B;
            }
            
            void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
            {
                Out = lerp(A, B, T);
            }
            
            void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
            {
                Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
            }
            
            void Unity_Add_float4(float4 A, float4 B, out float4 Out)
            {
                Out = A + B;
            }
            
            void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
            {
                Out = A * B;
            }
            
            void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
            
            void Unity_Subtract_float(float A, float B, out float Out)
            {
                Out = A - B;
            }
        
            // Graph Vertex
            struct VertexDescriptionInputs
            {
                float3 ObjectSpaceNormal;
                float3 WorldSpaceNormal;
                float3 ObjectSpaceTangent;
                float3 ObjectSpacePosition;
                float3 WorldSpacePosition;
                float3 TimeParameters;
            };
            
            struct VertexDescription
            {
                float3 VertexPosition;
                float3 VertexNormal;
                float3 VertexTangent;
            };
            
            VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
            {
                VertexDescription description = (VertexDescription)0;
                float _Distance_4E95187D_Out_2;
                Unity_Distance_float3(SHADERGRAPH_OBJECT_POSITION, IN.WorldSpacePosition, _Distance_4E95187D_Out_2);
                float _Property_5289A3_Out_0 = Vector1_D98ACD8A;
                float _Divide_C8C9E17B_Out_2;
                Unity_Divide_float(_Distance_4E95187D_Out_2, _Property_5289A3_Out_0, _Divide_C8C9E17B_Out_2);
                float _Power_3564875B_Out_2;
                Unity_Power_float(_Divide_C8C9E17B_Out_2, 3, _Power_3564875B_Out_2);
                float3 _Multiply_8B78CB96_Out_2;
                Unity_Multiply_float(IN.WorldSpaceNormal, (_Power_3564875B_Out_2.xxx), _Multiply_8B78CB96_Out_2);
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float3 _Multiply_BF237FFB_Out_2;
                Unity_Multiply_float(IN.ObjectSpaceNormal, (_Divide_FD292D7C_Out_2.xxx), _Multiply_BF237FFB_Out_2);
                float _Property_C4F6DD24_Out_0 = Vector1_F4059B8F;
                float3 _Multiply_2245EB49_Out_2;
                Unity_Multiply_float(_Multiply_BF237FFB_Out_2, (_Property_C4F6DD24_Out_0.xxx), _Multiply_2245EB49_Out_2);
                float3 _Add_1CC2221B_Out_2;
                Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_2245EB49_Out_2, _Add_1CC2221B_Out_2);
                float3 _Add_84FD76EF_Out_2;
                Unity_Add_float3(_Multiply_8B78CB96_Out_2, _Add_1CC2221B_Out_2, _Add_84FD76EF_Out_2);
                description.VertexPosition = _Add_84FD76EF_Out_2;
                description.VertexNormal = IN.ObjectSpaceNormal;
                description.VertexTangent = IN.ObjectSpaceTangent;
                return description;
            }
            
            // Graph Pixel
            struct SurfaceDescriptionInputs
            {
                float3 WorldSpaceNormal;
                float3 TangentSpaceNormal;
                float3 WorldSpaceViewDirection;
                float3 WorldSpacePosition;
                float4 ScreenPosition;
                float3 TimeParameters;
            };
            
            struct SurfaceDescription
            {
                float3 Albedo;
                float3 Normal;
                float3 Emission;
                float Metallic;
                float Smoothness;
                float Occlusion;
                float Alpha;
                float AlphaClipThreshold;
            };
            
            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
            {
                SurfaceDescription surface = (SurfaceDescription)0;
                float4 _Property_76C72643_Out_0 = Color_58B36FA2;
                float4 _Property_5CDD73DE_Out_0 = Color_C9E01EB;
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float4 _Lerp_8C9CB85D_Out_3;
                Unity_Lerp_float4(_Property_76C72643_Out_0, _Property_5CDD73DE_Out_0, (_Divide_FD292D7C_Out_2.xxxx), _Lerp_8C9CB85D_Out_3);
                float _Property_31CF0ED1_Out_0 = Vector1_C4C61918;
                float _FresnelEffect_CAF843F1_Out_3;
                Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_31CF0ED1_Out_0, _FresnelEffect_CAF843F1_Out_3);
                float _Multiply_9B15027F_Out_2;
                Unity_Multiply_float(_Divide_FD292D7C_Out_2, _FresnelEffect_CAF843F1_Out_3, _Multiply_9B15027F_Out_2);
                float _Property_B64EC7E0_Out_0 = Vector1_21501071;
                float _Multiply_D2CAD4F6_Out_2;
                Unity_Multiply_float(_Multiply_9B15027F_Out_2, _Property_B64EC7E0_Out_0, _Multiply_D2CAD4F6_Out_2);
                float4 _Add_D7B9147C_Out_2;
                Unity_Add_float4(_Lerp_8C9CB85D_Out_3, (_Multiply_D2CAD4F6_Out_2.xxxx), _Add_D7B9147C_Out_2);
                float _Property_67307B68_Out_0 = Vector1_7DCD71B1;
                float4 _Multiply_A18B68D1_Out_2;
                Unity_Multiply_float(_Add_D7B9147C_Out_2, (_Property_67307B68_Out_0.xxxx), _Multiply_A18B68D1_Out_2);
                float _SceneDepth_F04F5ABA_Out_1;
                Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_F04F5ABA_Out_1);
                float4 _ScreenPosition_229B9C29_Out_0 = IN.ScreenPosition;
                float _Split_E684EE58_R_1 = _ScreenPosition_229B9C29_Out_0[0];
                float _Split_E684EE58_G_2 = _ScreenPosition_229B9C29_Out_0[1];
                float _Split_E684EE58_B_3 = _ScreenPosition_229B9C29_Out_0[2];
                float _Split_E684EE58_A_4 = _ScreenPosition_229B9C29_Out_0[3];
                float _Subtract_204ECEAB_Out_2;
                Unity_Subtract_float(_Split_E684EE58_A_4, 1, _Subtract_204ECEAB_Out_2);
                float _Subtract_72D1DC2F_Out_2;
                Unity_Subtract_float(_SceneDepth_F04F5ABA_Out_1, _Subtract_204ECEAB_Out_2, _Subtract_72D1DC2F_Out_2);
                float _Property_D62CAD98_Out_0 = Vector1_E502573B;
                float _Divide_13C1DE9A_Out_2;
                Unity_Divide_float(_Subtract_72D1DC2F_Out_2, _Property_D62CAD98_Out_0, _Divide_13C1DE9A_Out_2);
                float _Saturate_2EBD6E58_Out_1;
                Unity_Saturate_float(_Divide_13C1DE9A_Out_2, _Saturate_2EBD6E58_Out_1);
                surface.Albedo = (_Add_D7B9147C_Out_2.xyz);
                surface.Normal = IN.TangentSpaceNormal;
                surface.Emission = (_Multiply_A18B68D1_Out_2.xyz);
                surface.Metallic = 0;
                surface.Smoothness = 0.5;
                surface.Occlusion = 1;
                surface.Alpha = _Saturate_2EBD6E58_Out_1;
                surface.AlphaClipThreshold = 0;
                return surface;
            }
        
            // --------------------------------------------------
            // Structs and Packing
        
            // Generated Type: Attributes
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv1 : TEXCOORD1;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif
            };
        
            // Generated Type: Varyings
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS;
                float3 normalWS;
                float4 tangentWS;
                float3 viewDirectionWS;
                #if defined(LIGHTMAP_ON)
                float2 lightmapUV;
                #endif
                #if !defined(LIGHTMAP_ON)
                float3 sh;
                #endif
                float4 fogFactorAndVertexLight;
                float4 shadowCoord;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Generated Type: PackedVaryings
            struct PackedVaryings
            {
                float4 positionCS : SV_POSITION;
                #if defined(LIGHTMAP_ON)
                #endif
                #if !defined(LIGHTMAP_ON)
                #endif
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                float3 interp00 : TEXCOORD0;
                float3 interp01 : TEXCOORD1;
                float4 interp02 : TEXCOORD2;
                float3 interp03 : TEXCOORD3;
                float2 interp04 : TEXCOORD4;
                float3 interp05 : TEXCOORD5;
                float4 interp06 : TEXCOORD6;
                float4 interp07 : TEXCOORD7;
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Packed Type: Varyings
            PackedVaryings PackVaryings(Varyings input)
            {
                PackedVaryings output = (PackedVaryings)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionWS;
                output.interp01.xyz = input.normalWS;
                output.interp02.xyzw = input.tangentWS;
                output.interp03.xyz = input.viewDirectionWS;
                #if defined(LIGHTMAP_ON)
                output.interp04.xy = input.lightmapUV;
                #endif
                #if !defined(LIGHTMAP_ON)
                output.interp05.xyz = input.sh;
                #endif
                output.interp06.xyzw = input.fogFactorAndVertexLight;
                output.interp07.xyzw = input.shadowCoord;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
            
            // Unpacked Type: Varyings
            Varyings UnpackVaryings(PackedVaryings input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = input.positionCS;
                output.positionWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                output.tangentWS = input.interp02.xyzw;
                output.viewDirectionWS = input.interp03.xyz;
                #if defined(LIGHTMAP_ON)
                output.lightmapUV = input.interp04.xy;
                #endif
                #if !defined(LIGHTMAP_ON)
                output.sh = input.interp05.xyz;
                #endif
                output.fogFactorAndVertexLight = input.interp06.xyzw;
                output.shadowCoord = input.interp07.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                output.ObjectSpaceTangent =          input.tangentOS;
                output.ObjectSpacePosition =         input.positionOS;
                output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                output.TimeParameters =              _TimeParameters.xyz;
            
                return output;
            }
            
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
            {
                SurfaceDescriptionInputs output;
                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
            	// must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            	float3 unnormalizedNormalWS = input.normalWS;
                const float renormFactor = 1.0 / length(unnormalizedNormalWS);
            
            
                output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
            
            
                output.WorldSpaceViewDirection =     input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                output.WorldSpacePosition =          input.positionWS;
                output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
                output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
            #else
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            #endif
            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            
                return output;
            }
            
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBRForwardPass.hlsl"
        
            ENDHLSL
        }
        
        Pass
        {
            Name "ShadowCaster"
            Tags 
            { 
                "LightMode" = "ShadowCaster"
            }
           
            // Render State
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZTest LEqual
            ZWrite On
            // ColorMask: <None>
            
        
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            // Pragmas
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            #pragma multi_compile_instancing
        
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
            
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define VARYINGS_NEED_POSITION_WS 
            #define FEATURES_GRAPH_VERTEX
            #pragma multi_compile_instancing
            #define SHADERPASS_SHADOWCASTER
            #define REQUIRE_DEPTH_TEXTURE
            
        
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
            float4 Vector4_F4396F31;
            float Vector1_5FE82E3A;
            float Vector1_4F738604;
            float Vector1_F4059B8F;
            float4 Vector4_C43B424F;
            float4 Color_58B36FA2;
            float4 Color_C9E01EB;
            float Vector1_89FF875A;
            float Vector1_13F963C;
            float Vector1_CFF9FCE;
            float Vector1_65331915;
            float Vector1_4E54445F;
            float Vector1_3EAF4E50;
            float Vector1_7DCD71B1;
            float Vector1_D98ACD8A;
            float Vector1_C4C61918;
            float Vector1_21501071;
            float Vector1_E502573B;
            CBUFFER_END
        
            // Graph Functions
            
            void Unity_Distance_float3(float3 A, float3 B, out float Out)
            {
                Out = distance(A, B);
            }
            
            void Unity_Divide_float(float A, float B, out float Out)
            {
                Out = A / B;
            }
            
            void Unity_Power_float(float A, float B, out float Out)
            {
                Out = pow(A, B);
            }
            
            void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }
            
            void Unity_Rotate_About_Axis_Degrees_float(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                Rotation = radians(Rotation);
            
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;
                
                Axis = normalize(Axis);
            
                float3x3 rot_mat = { one_minus_c * Axis.x * Axis.x + c,            one_minus_c * Axis.x * Axis.y - Axis.z * s,     one_minus_c * Axis.z * Axis.x + Axis.y * s,
                                          one_minus_c * Axis.x * Axis.y + Axis.z * s,   one_minus_c * Axis.y * Axis.y + c,              one_minus_c * Axis.y * Axis.z - Axis.x * s,
                                          one_minus_c * Axis.z * Axis.x - Axis.y * s,   one_minus_c * Axis.y * Axis.z + Axis.x * s,     one_minus_c * Axis.z * Axis.z + c
                                        };
            
                Out = mul(rot_mat,  In);
            }
            
            void Unity_Multiply_float(float A, float B, out float Out)
            {
                Out = A * B;
            }
            
            void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
            {
                Out = UV * Tiling + Offset;
            }
            
            
            float2 Unity_GradientNoise_Dir_float(float2 p)
            {
                // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }
            
            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            { 
                float2 p = UV * Scale;
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
            }
            
            void Unity_Add_float(float A, float B, out float Out)
            {
                Out = A + B;
            }
            
            void Unity_Saturate_float(float In, out float Out)
            {
                Out = saturate(In);
            }
            
            void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
            {
                RGBA = float4(R, G, B, A);
                RGB = float3(R, G, B);
                RG = float2(R, G);
            }
            
            void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            void Unity_Absolute_float(float In, out float Out)
            {
                Out = abs(In);
            }
            
            void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
            {
                Out = smoothstep(Edge1, Edge2, In);
            }
            
            void Unity_Add_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A + B;
            }
            
            void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
            
            void Unity_Subtract_float(float A, float B, out float Out)
            {
                Out = A - B;
            }
        
            // Graph Vertex
            struct VertexDescriptionInputs
            {
                float3 ObjectSpaceNormal;
                float3 WorldSpaceNormal;
                float3 ObjectSpaceTangent;
                float3 ObjectSpacePosition;
                float3 WorldSpacePosition;
                float3 TimeParameters;
            };
            
            struct VertexDescription
            {
                float3 VertexPosition;
                float3 VertexNormal;
                float3 VertexTangent;
            };
            
            VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
            {
                VertexDescription description = (VertexDescription)0;
                float _Distance_4E95187D_Out_2;
                Unity_Distance_float3(SHADERGRAPH_OBJECT_POSITION, IN.WorldSpacePosition, _Distance_4E95187D_Out_2);
                float _Property_5289A3_Out_0 = Vector1_D98ACD8A;
                float _Divide_C8C9E17B_Out_2;
                Unity_Divide_float(_Distance_4E95187D_Out_2, _Property_5289A3_Out_0, _Divide_C8C9E17B_Out_2);
                float _Power_3564875B_Out_2;
                Unity_Power_float(_Divide_C8C9E17B_Out_2, 3, _Power_3564875B_Out_2);
                float3 _Multiply_8B78CB96_Out_2;
                Unity_Multiply_float(IN.WorldSpaceNormal, (_Power_3564875B_Out_2.xxx), _Multiply_8B78CB96_Out_2);
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float3 _Multiply_BF237FFB_Out_2;
                Unity_Multiply_float(IN.ObjectSpaceNormal, (_Divide_FD292D7C_Out_2.xxx), _Multiply_BF237FFB_Out_2);
                float _Property_C4F6DD24_Out_0 = Vector1_F4059B8F;
                float3 _Multiply_2245EB49_Out_2;
                Unity_Multiply_float(_Multiply_BF237FFB_Out_2, (_Property_C4F6DD24_Out_0.xxx), _Multiply_2245EB49_Out_2);
                float3 _Add_1CC2221B_Out_2;
                Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_2245EB49_Out_2, _Add_1CC2221B_Out_2);
                float3 _Add_84FD76EF_Out_2;
                Unity_Add_float3(_Multiply_8B78CB96_Out_2, _Add_1CC2221B_Out_2, _Add_84FD76EF_Out_2);
                description.VertexPosition = _Add_84FD76EF_Out_2;
                description.VertexNormal = IN.ObjectSpaceNormal;
                description.VertexTangent = IN.ObjectSpaceTangent;
                return description;
            }
            
            // Graph Pixel
            struct SurfaceDescriptionInputs
            {
                float3 TangentSpaceNormal;
                float3 WorldSpacePosition;
                float4 ScreenPosition;
            };
            
            struct SurfaceDescription
            {
                float Alpha;
                float AlphaClipThreshold;
            };
            
            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
            {
                SurfaceDescription surface = (SurfaceDescription)0;
                float _SceneDepth_F04F5ABA_Out_1;
                Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_F04F5ABA_Out_1);
                float4 _ScreenPosition_229B9C29_Out_0 = IN.ScreenPosition;
                float _Split_E684EE58_R_1 = _ScreenPosition_229B9C29_Out_0[0];
                float _Split_E684EE58_G_2 = _ScreenPosition_229B9C29_Out_0[1];
                float _Split_E684EE58_B_3 = _ScreenPosition_229B9C29_Out_0[2];
                float _Split_E684EE58_A_4 = _ScreenPosition_229B9C29_Out_0[3];
                float _Subtract_204ECEAB_Out_2;
                Unity_Subtract_float(_Split_E684EE58_A_4, 1, _Subtract_204ECEAB_Out_2);
                float _Subtract_72D1DC2F_Out_2;
                Unity_Subtract_float(_SceneDepth_F04F5ABA_Out_1, _Subtract_204ECEAB_Out_2, _Subtract_72D1DC2F_Out_2);
                float _Property_D62CAD98_Out_0 = Vector1_E502573B;
                float _Divide_13C1DE9A_Out_2;
                Unity_Divide_float(_Subtract_72D1DC2F_Out_2, _Property_D62CAD98_Out_0, _Divide_13C1DE9A_Out_2);
                float _Saturate_2EBD6E58_Out_1;
                Unity_Saturate_float(_Divide_13C1DE9A_Out_2, _Saturate_2EBD6E58_Out_1);
                surface.Alpha = _Saturate_2EBD6E58_Out_1;
                surface.AlphaClipThreshold = 0;
                return surface;
            }
        
            // --------------------------------------------------
            // Structs and Packing
        
            // Generated Type: Attributes
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif
            };
        
            // Generated Type: Varyings
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Generated Type: PackedVaryings
            struct PackedVaryings
            {
                float4 positionCS : SV_POSITION;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                float3 interp00 : TEXCOORD0;
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Packed Type: Varyings
            PackedVaryings PackVaryings(Varyings input)
            {
                PackedVaryings output = (PackedVaryings)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
            
            // Unpacked Type: Varyings
            Varyings UnpackVaryings(PackedVaryings input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = input.positionCS;
                output.positionWS = input.interp00.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                output.ObjectSpaceTangent =          input.tangentOS;
                output.ObjectSpacePosition =         input.positionOS;
                output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                output.TimeParameters =              _TimeParameters.xyz;
            
                return output;
            }
            
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
            {
                SurfaceDescriptionInputs output;
                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
            
            
                output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
            
            
                output.WorldSpacePosition =          input.positionWS;
                output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
            #else
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            #endif
            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            
                return output;
            }
            
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"
        
            ENDHLSL
        }
        
        Pass
        {
            Name "DepthOnly"
            Tags 
            { 
                "LightMode" = "DepthOnly"
            }
           
            // Render State
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZTest LEqual
            ZWrite On
            ColorMask 0
            
        
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            // Pragmas
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            #pragma multi_compile_instancing
        
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
            
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define VARYINGS_NEED_POSITION_WS 
            #define FEATURES_GRAPH_VERTEX
            #pragma multi_compile_instancing
            #define SHADERPASS_DEPTHONLY
            #define REQUIRE_DEPTH_TEXTURE
            
        
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
            float4 Vector4_F4396F31;
            float Vector1_5FE82E3A;
            float Vector1_4F738604;
            float Vector1_F4059B8F;
            float4 Vector4_C43B424F;
            float4 Color_58B36FA2;
            float4 Color_C9E01EB;
            float Vector1_89FF875A;
            float Vector1_13F963C;
            float Vector1_CFF9FCE;
            float Vector1_65331915;
            float Vector1_4E54445F;
            float Vector1_3EAF4E50;
            float Vector1_7DCD71B1;
            float Vector1_D98ACD8A;
            float Vector1_C4C61918;
            float Vector1_21501071;
            float Vector1_E502573B;
            CBUFFER_END
        
            // Graph Functions
            
            void Unity_Distance_float3(float3 A, float3 B, out float Out)
            {
                Out = distance(A, B);
            }
            
            void Unity_Divide_float(float A, float B, out float Out)
            {
                Out = A / B;
            }
            
            void Unity_Power_float(float A, float B, out float Out)
            {
                Out = pow(A, B);
            }
            
            void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }
            
            void Unity_Rotate_About_Axis_Degrees_float(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                Rotation = radians(Rotation);
            
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;
                
                Axis = normalize(Axis);
            
                float3x3 rot_mat = { one_minus_c * Axis.x * Axis.x + c,            one_minus_c * Axis.x * Axis.y - Axis.z * s,     one_minus_c * Axis.z * Axis.x + Axis.y * s,
                                          one_minus_c * Axis.x * Axis.y + Axis.z * s,   one_minus_c * Axis.y * Axis.y + c,              one_minus_c * Axis.y * Axis.z - Axis.x * s,
                                          one_minus_c * Axis.z * Axis.x - Axis.y * s,   one_minus_c * Axis.y * Axis.z + Axis.x * s,     one_minus_c * Axis.z * Axis.z + c
                                        };
            
                Out = mul(rot_mat,  In);
            }
            
            void Unity_Multiply_float(float A, float B, out float Out)
            {
                Out = A * B;
            }
            
            void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
            {
                Out = UV * Tiling + Offset;
            }
            
            
            float2 Unity_GradientNoise_Dir_float(float2 p)
            {
                // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }
            
            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            { 
                float2 p = UV * Scale;
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
            }
            
            void Unity_Add_float(float A, float B, out float Out)
            {
                Out = A + B;
            }
            
            void Unity_Saturate_float(float In, out float Out)
            {
                Out = saturate(In);
            }
            
            void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
            {
                RGBA = float4(R, G, B, A);
                RGB = float3(R, G, B);
                RG = float2(R, G);
            }
            
            void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            void Unity_Absolute_float(float In, out float Out)
            {
                Out = abs(In);
            }
            
            void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
            {
                Out = smoothstep(Edge1, Edge2, In);
            }
            
            void Unity_Add_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A + B;
            }
            
            void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
            
            void Unity_Subtract_float(float A, float B, out float Out)
            {
                Out = A - B;
            }
        
            // Graph Vertex
            struct VertexDescriptionInputs
            {
                float3 ObjectSpaceNormal;
                float3 WorldSpaceNormal;
                float3 ObjectSpaceTangent;
                float3 ObjectSpacePosition;
                float3 WorldSpacePosition;
                float3 TimeParameters;
            };
            
            struct VertexDescription
            {
                float3 VertexPosition;
                float3 VertexNormal;
                float3 VertexTangent;
            };
            
            VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
            {
                VertexDescription description = (VertexDescription)0;
                float _Distance_4E95187D_Out_2;
                Unity_Distance_float3(SHADERGRAPH_OBJECT_POSITION, IN.WorldSpacePosition, _Distance_4E95187D_Out_2);
                float _Property_5289A3_Out_0 = Vector1_D98ACD8A;
                float _Divide_C8C9E17B_Out_2;
                Unity_Divide_float(_Distance_4E95187D_Out_2, _Property_5289A3_Out_0, _Divide_C8C9E17B_Out_2);
                float _Power_3564875B_Out_2;
                Unity_Power_float(_Divide_C8C9E17B_Out_2, 3, _Power_3564875B_Out_2);
                float3 _Multiply_8B78CB96_Out_2;
                Unity_Multiply_float(IN.WorldSpaceNormal, (_Power_3564875B_Out_2.xxx), _Multiply_8B78CB96_Out_2);
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float3 _Multiply_BF237FFB_Out_2;
                Unity_Multiply_float(IN.ObjectSpaceNormal, (_Divide_FD292D7C_Out_2.xxx), _Multiply_BF237FFB_Out_2);
                float _Property_C4F6DD24_Out_0 = Vector1_F4059B8F;
                float3 _Multiply_2245EB49_Out_2;
                Unity_Multiply_float(_Multiply_BF237FFB_Out_2, (_Property_C4F6DD24_Out_0.xxx), _Multiply_2245EB49_Out_2);
                float3 _Add_1CC2221B_Out_2;
                Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_2245EB49_Out_2, _Add_1CC2221B_Out_2);
                float3 _Add_84FD76EF_Out_2;
                Unity_Add_float3(_Multiply_8B78CB96_Out_2, _Add_1CC2221B_Out_2, _Add_84FD76EF_Out_2);
                description.VertexPosition = _Add_84FD76EF_Out_2;
                description.VertexNormal = IN.ObjectSpaceNormal;
                description.VertexTangent = IN.ObjectSpaceTangent;
                return description;
            }
            
            // Graph Pixel
            struct SurfaceDescriptionInputs
            {
                float3 TangentSpaceNormal;
                float3 WorldSpacePosition;
                float4 ScreenPosition;
            };
            
            struct SurfaceDescription
            {
                float Alpha;
                float AlphaClipThreshold;
            };
            
            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
            {
                SurfaceDescription surface = (SurfaceDescription)0;
                float _SceneDepth_F04F5ABA_Out_1;
                Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_F04F5ABA_Out_1);
                float4 _ScreenPosition_229B9C29_Out_0 = IN.ScreenPosition;
                float _Split_E684EE58_R_1 = _ScreenPosition_229B9C29_Out_0[0];
                float _Split_E684EE58_G_2 = _ScreenPosition_229B9C29_Out_0[1];
                float _Split_E684EE58_B_3 = _ScreenPosition_229B9C29_Out_0[2];
                float _Split_E684EE58_A_4 = _ScreenPosition_229B9C29_Out_0[3];
                float _Subtract_204ECEAB_Out_2;
                Unity_Subtract_float(_Split_E684EE58_A_4, 1, _Subtract_204ECEAB_Out_2);
                float _Subtract_72D1DC2F_Out_2;
                Unity_Subtract_float(_SceneDepth_F04F5ABA_Out_1, _Subtract_204ECEAB_Out_2, _Subtract_72D1DC2F_Out_2);
                float _Property_D62CAD98_Out_0 = Vector1_E502573B;
                float _Divide_13C1DE9A_Out_2;
                Unity_Divide_float(_Subtract_72D1DC2F_Out_2, _Property_D62CAD98_Out_0, _Divide_13C1DE9A_Out_2);
                float _Saturate_2EBD6E58_Out_1;
                Unity_Saturate_float(_Divide_13C1DE9A_Out_2, _Saturate_2EBD6E58_Out_1);
                surface.Alpha = _Saturate_2EBD6E58_Out_1;
                surface.AlphaClipThreshold = 0;
                return surface;
            }
        
            // --------------------------------------------------
            // Structs and Packing
        
            // Generated Type: Attributes
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif
            };
        
            // Generated Type: Varyings
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Generated Type: PackedVaryings
            struct PackedVaryings
            {
                float4 positionCS : SV_POSITION;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                float3 interp00 : TEXCOORD0;
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Packed Type: Varyings
            PackedVaryings PackVaryings(Varyings input)
            {
                PackedVaryings output = (PackedVaryings)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
            
            // Unpacked Type: Varyings
            Varyings UnpackVaryings(PackedVaryings input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = input.positionCS;
                output.positionWS = input.interp00.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                output.ObjectSpaceTangent =          input.tangentOS;
                output.ObjectSpacePosition =         input.positionOS;
                output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                output.TimeParameters =              _TimeParameters.xyz;
            
                return output;
            }
            
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
            {
                SurfaceDescriptionInputs output;
                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
            
            
                output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
            
            
                output.WorldSpacePosition =          input.positionWS;
                output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
            #else
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            #endif
            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            
                return output;
            }
            
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"
        
            ENDHLSL
        }
        
        Pass
        {
            Name "Meta"
            Tags 
            { 
                "LightMode" = "Meta"
            }
           
            // Render State
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZTest LEqual
            ZWrite On
            // ColorMask: <None>
            
        
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            // Pragmas
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
        
            // Keywords
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            // GraphKeywords: <None>
            
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define VARYINGS_NEED_POSITION_WS 
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_VIEWDIRECTION_WS
            #define FEATURES_GRAPH_VERTEX
            #pragma multi_compile_instancing
            #define SHADERPASS_META
            #define REQUIRE_DEPTH_TEXTURE
            
        
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
            float4 Vector4_F4396F31;
            float Vector1_5FE82E3A;
            float Vector1_4F738604;
            float Vector1_F4059B8F;
            float4 Vector4_C43B424F;
            float4 Color_58B36FA2;
            float4 Color_C9E01EB;
            float Vector1_89FF875A;
            float Vector1_13F963C;
            float Vector1_CFF9FCE;
            float Vector1_65331915;
            float Vector1_4E54445F;
            float Vector1_3EAF4E50;
            float Vector1_7DCD71B1;
            float Vector1_D98ACD8A;
            float Vector1_C4C61918;
            float Vector1_21501071;
            float Vector1_E502573B;
            CBUFFER_END
        
            // Graph Functions
            
            void Unity_Distance_float3(float3 A, float3 B, out float Out)
            {
                Out = distance(A, B);
            }
            
            void Unity_Divide_float(float A, float B, out float Out)
            {
                Out = A / B;
            }
            
            void Unity_Power_float(float A, float B, out float Out)
            {
                Out = pow(A, B);
            }
            
            void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }
            
            void Unity_Rotate_About_Axis_Degrees_float(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                Rotation = radians(Rotation);
            
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;
                
                Axis = normalize(Axis);
            
                float3x3 rot_mat = { one_minus_c * Axis.x * Axis.x + c,            one_minus_c * Axis.x * Axis.y - Axis.z * s,     one_minus_c * Axis.z * Axis.x + Axis.y * s,
                                          one_minus_c * Axis.x * Axis.y + Axis.z * s,   one_minus_c * Axis.y * Axis.y + c,              one_minus_c * Axis.y * Axis.z - Axis.x * s,
                                          one_minus_c * Axis.z * Axis.x - Axis.y * s,   one_minus_c * Axis.y * Axis.z + Axis.x * s,     one_minus_c * Axis.z * Axis.z + c
                                        };
            
                Out = mul(rot_mat,  In);
            }
            
            void Unity_Multiply_float(float A, float B, out float Out)
            {
                Out = A * B;
            }
            
            void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
            {
                Out = UV * Tiling + Offset;
            }
            
            
            float2 Unity_GradientNoise_Dir_float(float2 p)
            {
                // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }
            
            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            { 
                float2 p = UV * Scale;
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
            }
            
            void Unity_Add_float(float A, float B, out float Out)
            {
                Out = A + B;
            }
            
            void Unity_Saturate_float(float In, out float Out)
            {
                Out = saturate(In);
            }
            
            void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
            {
                RGBA = float4(R, G, B, A);
                RGB = float3(R, G, B);
                RG = float2(R, G);
            }
            
            void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            void Unity_Absolute_float(float In, out float Out)
            {
                Out = abs(In);
            }
            
            void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
            {
                Out = smoothstep(Edge1, Edge2, In);
            }
            
            void Unity_Add_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A + B;
            }
            
            void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
            {
                Out = lerp(A, B, T);
            }
            
            void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
            {
                Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
            }
            
            void Unity_Add_float4(float4 A, float4 B, out float4 Out)
            {
                Out = A + B;
            }
            
            void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
            {
                Out = A * B;
            }
            
            void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
            
            void Unity_Subtract_float(float A, float B, out float Out)
            {
                Out = A - B;
            }
        
            // Graph Vertex
            struct VertexDescriptionInputs
            {
                float3 ObjectSpaceNormal;
                float3 WorldSpaceNormal;
                float3 ObjectSpaceTangent;
                float3 ObjectSpacePosition;
                float3 WorldSpacePosition;
                float3 TimeParameters;
            };
            
            struct VertexDescription
            {
                float3 VertexPosition;
                float3 VertexNormal;
                float3 VertexTangent;
            };
            
            VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
            {
                VertexDescription description = (VertexDescription)0;
                float _Distance_4E95187D_Out_2;
                Unity_Distance_float3(SHADERGRAPH_OBJECT_POSITION, IN.WorldSpacePosition, _Distance_4E95187D_Out_2);
                float _Property_5289A3_Out_0 = Vector1_D98ACD8A;
                float _Divide_C8C9E17B_Out_2;
                Unity_Divide_float(_Distance_4E95187D_Out_2, _Property_5289A3_Out_0, _Divide_C8C9E17B_Out_2);
                float _Power_3564875B_Out_2;
                Unity_Power_float(_Divide_C8C9E17B_Out_2, 3, _Power_3564875B_Out_2);
                float3 _Multiply_8B78CB96_Out_2;
                Unity_Multiply_float(IN.WorldSpaceNormal, (_Power_3564875B_Out_2.xxx), _Multiply_8B78CB96_Out_2);
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float3 _Multiply_BF237FFB_Out_2;
                Unity_Multiply_float(IN.ObjectSpaceNormal, (_Divide_FD292D7C_Out_2.xxx), _Multiply_BF237FFB_Out_2);
                float _Property_C4F6DD24_Out_0 = Vector1_F4059B8F;
                float3 _Multiply_2245EB49_Out_2;
                Unity_Multiply_float(_Multiply_BF237FFB_Out_2, (_Property_C4F6DD24_Out_0.xxx), _Multiply_2245EB49_Out_2);
                float3 _Add_1CC2221B_Out_2;
                Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_2245EB49_Out_2, _Add_1CC2221B_Out_2);
                float3 _Add_84FD76EF_Out_2;
                Unity_Add_float3(_Multiply_8B78CB96_Out_2, _Add_1CC2221B_Out_2, _Add_84FD76EF_Out_2);
                description.VertexPosition = _Add_84FD76EF_Out_2;
                description.VertexNormal = IN.ObjectSpaceNormal;
                description.VertexTangent = IN.ObjectSpaceTangent;
                return description;
            }
            
            // Graph Pixel
            struct SurfaceDescriptionInputs
            {
                float3 WorldSpaceNormal;
                float3 TangentSpaceNormal;
                float3 WorldSpaceViewDirection;
                float3 WorldSpacePosition;
                float4 ScreenPosition;
                float3 TimeParameters;
            };
            
            struct SurfaceDescription
            {
                float3 Albedo;
                float3 Emission;
                float Alpha;
                float AlphaClipThreshold;
            };
            
            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
            {
                SurfaceDescription surface = (SurfaceDescription)0;
                float4 _Property_76C72643_Out_0 = Color_58B36FA2;
                float4 _Property_5CDD73DE_Out_0 = Color_C9E01EB;
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float4 _Lerp_8C9CB85D_Out_3;
                Unity_Lerp_float4(_Property_76C72643_Out_0, _Property_5CDD73DE_Out_0, (_Divide_FD292D7C_Out_2.xxxx), _Lerp_8C9CB85D_Out_3);
                float _Property_31CF0ED1_Out_0 = Vector1_C4C61918;
                float _FresnelEffect_CAF843F1_Out_3;
                Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_31CF0ED1_Out_0, _FresnelEffect_CAF843F1_Out_3);
                float _Multiply_9B15027F_Out_2;
                Unity_Multiply_float(_Divide_FD292D7C_Out_2, _FresnelEffect_CAF843F1_Out_3, _Multiply_9B15027F_Out_2);
                float _Property_B64EC7E0_Out_0 = Vector1_21501071;
                float _Multiply_D2CAD4F6_Out_2;
                Unity_Multiply_float(_Multiply_9B15027F_Out_2, _Property_B64EC7E0_Out_0, _Multiply_D2CAD4F6_Out_2);
                float4 _Add_D7B9147C_Out_2;
                Unity_Add_float4(_Lerp_8C9CB85D_Out_3, (_Multiply_D2CAD4F6_Out_2.xxxx), _Add_D7B9147C_Out_2);
                float _Property_67307B68_Out_0 = Vector1_7DCD71B1;
                float4 _Multiply_A18B68D1_Out_2;
                Unity_Multiply_float(_Add_D7B9147C_Out_2, (_Property_67307B68_Out_0.xxxx), _Multiply_A18B68D1_Out_2);
                float _SceneDepth_F04F5ABA_Out_1;
                Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_F04F5ABA_Out_1);
                float4 _ScreenPosition_229B9C29_Out_0 = IN.ScreenPosition;
                float _Split_E684EE58_R_1 = _ScreenPosition_229B9C29_Out_0[0];
                float _Split_E684EE58_G_2 = _ScreenPosition_229B9C29_Out_0[1];
                float _Split_E684EE58_B_3 = _ScreenPosition_229B9C29_Out_0[2];
                float _Split_E684EE58_A_4 = _ScreenPosition_229B9C29_Out_0[3];
                float _Subtract_204ECEAB_Out_2;
                Unity_Subtract_float(_Split_E684EE58_A_4, 1, _Subtract_204ECEAB_Out_2);
                float _Subtract_72D1DC2F_Out_2;
                Unity_Subtract_float(_SceneDepth_F04F5ABA_Out_1, _Subtract_204ECEAB_Out_2, _Subtract_72D1DC2F_Out_2);
                float _Property_D62CAD98_Out_0 = Vector1_E502573B;
                float _Divide_13C1DE9A_Out_2;
                Unity_Divide_float(_Subtract_72D1DC2F_Out_2, _Property_D62CAD98_Out_0, _Divide_13C1DE9A_Out_2);
                float _Saturate_2EBD6E58_Out_1;
                Unity_Saturate_float(_Divide_13C1DE9A_Out_2, _Saturate_2EBD6E58_Out_1);
                surface.Albedo = (_Add_D7B9147C_Out_2.xyz);
                surface.Emission = (_Multiply_A18B68D1_Out_2.xyz);
                surface.Alpha = _Saturate_2EBD6E58_Out_1;
                surface.AlphaClipThreshold = 0;
                return surface;
            }
        
            // --------------------------------------------------
            // Structs and Packing
        
            // Generated Type: Attributes
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif
            };
        
            // Generated Type: Varyings
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS;
                float3 normalWS;
                float3 viewDirectionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Generated Type: PackedVaryings
            struct PackedVaryings
            {
                float4 positionCS : SV_POSITION;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                float3 interp00 : TEXCOORD0;
                float3 interp01 : TEXCOORD1;
                float3 interp02 : TEXCOORD2;
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Packed Type: Varyings
            PackedVaryings PackVaryings(Varyings input)
            {
                PackedVaryings output = (PackedVaryings)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionWS;
                output.interp01.xyz = input.normalWS;
                output.interp02.xyz = input.viewDirectionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
            
            // Unpacked Type: Varyings
            Varyings UnpackVaryings(PackedVaryings input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = input.positionCS;
                output.positionWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                output.viewDirectionWS = input.interp02.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                output.ObjectSpaceTangent =          input.tangentOS;
                output.ObjectSpacePosition =         input.positionOS;
                output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                output.TimeParameters =              _TimeParameters.xyz;
            
                return output;
            }
            
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
            {
                SurfaceDescriptionInputs output;
                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
            	// must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            	float3 unnormalizedNormalWS = input.normalWS;
                const float renormFactor = 1.0 / length(unnormalizedNormalWS);
            
            
                output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
            
            
                output.WorldSpaceViewDirection =     input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                output.WorldSpacePosition =          input.positionWS;
                output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
                output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
            #else
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            #endif
            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            
                return output;
            }
            
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/LightingMetaPass.hlsl"
        
            ENDHLSL
        }
        
        Pass
        {
            // Name: <None>
            Tags 
            { 
                "LightMode" = "Universal2D"
            }
           
            // Render State
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZTest LEqual
            ZWrite Off
            // ColorMask: <None>
            
        
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            // Pragmas
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            #pragma multi_compile_instancing
        
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
            
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define VARYINGS_NEED_POSITION_WS 
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_VIEWDIRECTION_WS
            #define FEATURES_GRAPH_VERTEX
            #pragma multi_compile_instancing
            #define SHADERPASS_2D
            #define REQUIRE_DEPTH_TEXTURE
            
        
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
            float4 Vector4_F4396F31;
            float Vector1_5FE82E3A;
            float Vector1_4F738604;
            float Vector1_F4059B8F;
            float4 Vector4_C43B424F;
            float4 Color_58B36FA2;
            float4 Color_C9E01EB;
            float Vector1_89FF875A;
            float Vector1_13F963C;
            float Vector1_CFF9FCE;
            float Vector1_65331915;
            float Vector1_4E54445F;
            float Vector1_3EAF4E50;
            float Vector1_7DCD71B1;
            float Vector1_D98ACD8A;
            float Vector1_C4C61918;
            float Vector1_21501071;
            float Vector1_E502573B;
            CBUFFER_END
        
            // Graph Functions
            
            void Unity_Distance_float3(float3 A, float3 B, out float Out)
            {
                Out = distance(A, B);
            }
            
            void Unity_Divide_float(float A, float B, out float Out)
            {
                Out = A / B;
            }
            
            void Unity_Power_float(float A, float B, out float Out)
            {
                Out = pow(A, B);
            }
            
            void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }
            
            void Unity_Rotate_About_Axis_Degrees_float(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                Rotation = radians(Rotation);
            
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;
                
                Axis = normalize(Axis);
            
                float3x3 rot_mat = { one_minus_c * Axis.x * Axis.x + c,            one_minus_c * Axis.x * Axis.y - Axis.z * s,     one_minus_c * Axis.z * Axis.x + Axis.y * s,
                                          one_minus_c * Axis.x * Axis.y + Axis.z * s,   one_minus_c * Axis.y * Axis.y + c,              one_minus_c * Axis.y * Axis.z - Axis.x * s,
                                          one_minus_c * Axis.z * Axis.x - Axis.y * s,   one_minus_c * Axis.y * Axis.z + Axis.x * s,     one_minus_c * Axis.z * Axis.z + c
                                        };
            
                Out = mul(rot_mat,  In);
            }
            
            void Unity_Multiply_float(float A, float B, out float Out)
            {
                Out = A * B;
            }
            
            void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
            {
                Out = UV * Tiling + Offset;
            }
            
            
            float2 Unity_GradientNoise_Dir_float(float2 p)
            {
                // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }
            
            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            { 
                float2 p = UV * Scale;
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
                float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
            }
            
            void Unity_Add_float(float A, float B, out float Out)
            {
                Out = A + B;
            }
            
            void Unity_Saturate_float(float In, out float Out)
            {
                Out = saturate(In);
            }
            
            void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
            {
                RGBA = float4(R, G, B, A);
                RGB = float3(R, G, B);
                RG = float2(R, G);
            }
            
            void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            void Unity_Absolute_float(float In, out float Out)
            {
                Out = abs(In);
            }
            
            void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
            {
                Out = smoothstep(Edge1, Edge2, In);
            }
            
            void Unity_Add_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A + B;
            }
            
            void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
            {
                Out = lerp(A, B, T);
            }
            
            void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
            {
                Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
            }
            
            void Unity_Add_float4(float4 A, float4 B, out float4 Out)
            {
                Out = A + B;
            }
            
            void Unity_SceneDepth_Eye_float(float4 UV, out float Out)
            {
                Out = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
            }
            
            void Unity_Subtract_float(float A, float B, out float Out)
            {
                Out = A - B;
            }
        
            // Graph Vertex
            struct VertexDescriptionInputs
            {
                float3 ObjectSpaceNormal;
                float3 WorldSpaceNormal;
                float3 ObjectSpaceTangent;
                float3 ObjectSpacePosition;
                float3 WorldSpacePosition;
                float3 TimeParameters;
            };
            
            struct VertexDescription
            {
                float3 VertexPosition;
                float3 VertexNormal;
                float3 VertexTangent;
            };
            
            VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
            {
                VertexDescription description = (VertexDescription)0;
                float _Distance_4E95187D_Out_2;
                Unity_Distance_float3(SHADERGRAPH_OBJECT_POSITION, IN.WorldSpacePosition, _Distance_4E95187D_Out_2);
                float _Property_5289A3_Out_0 = Vector1_D98ACD8A;
                float _Divide_C8C9E17B_Out_2;
                Unity_Divide_float(_Distance_4E95187D_Out_2, _Property_5289A3_Out_0, _Divide_C8C9E17B_Out_2);
                float _Power_3564875B_Out_2;
                Unity_Power_float(_Divide_C8C9E17B_Out_2, 3, _Power_3564875B_Out_2);
                float3 _Multiply_8B78CB96_Out_2;
                Unity_Multiply_float(IN.WorldSpaceNormal, (_Power_3564875B_Out_2.xxx), _Multiply_8B78CB96_Out_2);
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float3 _Multiply_BF237FFB_Out_2;
                Unity_Multiply_float(IN.ObjectSpaceNormal, (_Divide_FD292D7C_Out_2.xxx), _Multiply_BF237FFB_Out_2);
                float _Property_C4F6DD24_Out_0 = Vector1_F4059B8F;
                float3 _Multiply_2245EB49_Out_2;
                Unity_Multiply_float(_Multiply_BF237FFB_Out_2, (_Property_C4F6DD24_Out_0.xxx), _Multiply_2245EB49_Out_2);
                float3 _Add_1CC2221B_Out_2;
                Unity_Add_float3(IN.ObjectSpacePosition, _Multiply_2245EB49_Out_2, _Add_1CC2221B_Out_2);
                float3 _Add_84FD76EF_Out_2;
                Unity_Add_float3(_Multiply_8B78CB96_Out_2, _Add_1CC2221B_Out_2, _Add_84FD76EF_Out_2);
                description.VertexPosition = _Add_84FD76EF_Out_2;
                description.VertexNormal = IN.ObjectSpaceNormal;
                description.VertexTangent = IN.ObjectSpaceTangent;
                return description;
            }
            
            // Graph Pixel
            struct SurfaceDescriptionInputs
            {
                float3 WorldSpaceNormal;
                float3 TangentSpaceNormal;
                float3 WorldSpaceViewDirection;
                float3 WorldSpacePosition;
                float4 ScreenPosition;
                float3 TimeParameters;
            };
            
            struct SurfaceDescription
            {
                float3 Albedo;
                float Alpha;
                float AlphaClipThreshold;
            };
            
            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
            {
                SurfaceDescription surface = (SurfaceDescription)0;
                float4 _Property_76C72643_Out_0 = Color_58B36FA2;
                float4 _Property_5CDD73DE_Out_0 = Color_C9E01EB;
                float _Property_85B4EC7B_Out_0 = Vector1_89FF875A;
                float _Property_9F9ACE06_Out_0 = Vector1_13F963C;
                float4 _Property_6823FC42_Out_0 = Vector4_F4396F31;
                float _Split_879907B7_R_1 = _Property_6823FC42_Out_0[0];
                float _Split_879907B7_G_2 = _Property_6823FC42_Out_0[1];
                float _Split_879907B7_B_3 = _Property_6823FC42_Out_0[2];
                float _Split_879907B7_A_4 = _Property_6823FC42_Out_0[3];
                float3 _RotateAboutAxis_D8D18C71_Out_3;
                Unity_Rotate_About_Axis_Degrees_float(IN.WorldSpacePosition, (_Property_6823FC42_Out_0.xyz), _Split_879907B7_A_4, _RotateAboutAxis_D8D18C71_Out_3);
                float _Property_241F0D36_Out_0 = Vector1_4F738604;
                float _Multiply_AC6BA394_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_241F0D36_Out_0, _Multiply_AC6BA394_Out_2);
                float2 _TilingAndOffset_E462A1CA_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_AC6BA394_Out_2.xx), _TilingAndOffset_E462A1CA_Out_3);
                float _Property_D72707B4_Out_0 = Vector1_5FE82E3A;
                float _GradientNoise_37C6C6B9_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E462A1CA_Out_3, _Property_D72707B4_Out_0, _GradientNoise_37C6C6B9_Out_2);
                float2 _TilingAndOffset_E6F18654_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), float2 (0, 0), _TilingAndOffset_E6F18654_Out_3);
                float _GradientNoise_15FF6F8D_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_E6F18654_Out_3, _Property_D72707B4_Out_0, _GradientNoise_15FF6F8D_Out_2);
                float _Add_EF3C09FC_Out_2;
                Unity_Add_float(_GradientNoise_37C6C6B9_Out_2, _GradientNoise_15FF6F8D_Out_2, _Add_EF3C09FC_Out_2);
                float _Divide_93AE703A_Out_2;
                Unity_Divide_float(_Add_EF3C09FC_Out_2, 2, _Divide_93AE703A_Out_2);
                float _Saturate_61FAB9C4_Out_1;
                Unity_Saturate_float(_Divide_93AE703A_Out_2, _Saturate_61FAB9C4_Out_1);
                float _Property_C2F21EB2_Out_0 = Vector1_CFF9FCE;
                float _Power_48BEBFC8_Out_2;
                Unity_Power_float(_Saturate_61FAB9C4_Out_1, _Property_C2F21EB2_Out_0, _Power_48BEBFC8_Out_2);
                float4 _Property_622DDB88_Out_0 = Vector4_C43B424F;
                float _Split_FCFC229E_R_1 = _Property_622DDB88_Out_0[0];
                float _Split_FCFC229E_G_2 = _Property_622DDB88_Out_0[1];
                float _Split_FCFC229E_B_3 = _Property_622DDB88_Out_0[2];
                float _Split_FCFC229E_A_4 = _Property_622DDB88_Out_0[3];
                float4 _Combine_48125A24_RGBA_4;
                float3 _Combine_48125A24_RGB_5;
                float2 _Combine_48125A24_RG_6;
                Unity_Combine_float(_Split_FCFC229E_R_1, _Split_FCFC229E_G_2, 0, 0, _Combine_48125A24_RGBA_4, _Combine_48125A24_RGB_5, _Combine_48125A24_RG_6);
                float4 _Combine_69F7939F_RGBA_4;
                float3 _Combine_69F7939F_RGB_5;
                float2 _Combine_69F7939F_RG_6;
                Unity_Combine_float(_Split_FCFC229E_B_3, _Split_FCFC229E_A_4, 0, 0, _Combine_69F7939F_RGBA_4, _Combine_69F7939F_RGB_5, _Combine_69F7939F_RG_6);
                float _Remap_BBAB0013_Out_3;
                Unity_Remap_float(_Power_48BEBFC8_Out_2, _Combine_48125A24_RG_6, _Combine_69F7939F_RG_6, _Remap_BBAB0013_Out_3);
                float _Absolute_CE59A209_Out_1;
                Unity_Absolute_float(_Remap_BBAB0013_Out_3, _Absolute_CE59A209_Out_1);
                float _Smoothstep_C35DB9B0_Out_3;
                Unity_Smoothstep_float(_Property_85B4EC7B_Out_0, _Property_9F9ACE06_Out_0, _Absolute_CE59A209_Out_1, _Smoothstep_C35DB9B0_Out_3);
                float _Property_101A95E0_Out_0 = Vector1_4E54445F;
                float _Multiply_1E06710D_Out_2;
                Unity_Multiply_float(IN.TimeParameters.x, _Property_101A95E0_Out_0, _Multiply_1E06710D_Out_2);
                float2 _TilingAndOffset_7B4D8A9A_Out_3;
                Unity_TilingAndOffset_float((_RotateAboutAxis_D8D18C71_Out_3.xy), float2 (1, 1), (_Multiply_1E06710D_Out_2.xx), _TilingAndOffset_7B4D8A9A_Out_3);
                float _Property_ED35FB23_Out_0 = Vector1_65331915;
                float _GradientNoise_F539C3E3_Out_2;
                Unity_GradientNoise_float(_TilingAndOffset_7B4D8A9A_Out_3, _Property_ED35FB23_Out_0, _GradientNoise_F539C3E3_Out_2);
                float _Property_3BDF1E45_Out_0 = Vector1_3EAF4E50;
                float _Multiply_6946B78C_Out_2;
                Unity_Multiply_float(_GradientNoise_F539C3E3_Out_2, _Property_3BDF1E45_Out_0, _Multiply_6946B78C_Out_2);
                float _Add_C24E9A7F_Out_2;
                Unity_Add_float(_Smoothstep_C35DB9B0_Out_3, _Multiply_6946B78C_Out_2, _Add_C24E9A7F_Out_2);
                float _Add_4A630D02_Out_2;
                Unity_Add_float(1, _Property_3BDF1E45_Out_0, _Add_4A630D02_Out_2);
                float _Divide_FD292D7C_Out_2;
                Unity_Divide_float(_Add_C24E9A7F_Out_2, _Add_4A630D02_Out_2, _Divide_FD292D7C_Out_2);
                float4 _Lerp_8C9CB85D_Out_3;
                Unity_Lerp_float4(_Property_76C72643_Out_0, _Property_5CDD73DE_Out_0, (_Divide_FD292D7C_Out_2.xxxx), _Lerp_8C9CB85D_Out_3);
                float _Property_31CF0ED1_Out_0 = Vector1_C4C61918;
                float _FresnelEffect_CAF843F1_Out_3;
                Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_31CF0ED1_Out_0, _FresnelEffect_CAF843F1_Out_3);
                float _Multiply_9B15027F_Out_2;
                Unity_Multiply_float(_Divide_FD292D7C_Out_2, _FresnelEffect_CAF843F1_Out_3, _Multiply_9B15027F_Out_2);
                float _Property_B64EC7E0_Out_0 = Vector1_21501071;
                float _Multiply_D2CAD4F6_Out_2;
                Unity_Multiply_float(_Multiply_9B15027F_Out_2, _Property_B64EC7E0_Out_0, _Multiply_D2CAD4F6_Out_2);
                float4 _Add_D7B9147C_Out_2;
                Unity_Add_float4(_Lerp_8C9CB85D_Out_3, (_Multiply_D2CAD4F6_Out_2.xxxx), _Add_D7B9147C_Out_2);
                float _SceneDepth_F04F5ABA_Out_1;
                Unity_SceneDepth_Eye_float(float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0), _SceneDepth_F04F5ABA_Out_1);
                float4 _ScreenPosition_229B9C29_Out_0 = IN.ScreenPosition;
                float _Split_E684EE58_R_1 = _ScreenPosition_229B9C29_Out_0[0];
                float _Split_E684EE58_G_2 = _ScreenPosition_229B9C29_Out_0[1];
                float _Split_E684EE58_B_3 = _ScreenPosition_229B9C29_Out_0[2];
                float _Split_E684EE58_A_4 = _ScreenPosition_229B9C29_Out_0[3];
                float _Subtract_204ECEAB_Out_2;
                Unity_Subtract_float(_Split_E684EE58_A_4, 1, _Subtract_204ECEAB_Out_2);
                float _Subtract_72D1DC2F_Out_2;
                Unity_Subtract_float(_SceneDepth_F04F5ABA_Out_1, _Subtract_204ECEAB_Out_2, _Subtract_72D1DC2F_Out_2);
                float _Property_D62CAD98_Out_0 = Vector1_E502573B;
                float _Divide_13C1DE9A_Out_2;
                Unity_Divide_float(_Subtract_72D1DC2F_Out_2, _Property_D62CAD98_Out_0, _Divide_13C1DE9A_Out_2);
                float _Saturate_2EBD6E58_Out_1;
                Unity_Saturate_float(_Divide_13C1DE9A_Out_2, _Saturate_2EBD6E58_Out_1);
                surface.Albedo = (_Add_D7B9147C_Out_2.xyz);
                surface.Alpha = _Saturate_2EBD6E58_Out_1;
                surface.AlphaClipThreshold = 0;
                return surface;
            }
        
            // --------------------------------------------------
            // Structs and Packing
        
            // Generated Type: Attributes
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif
            };
        
            // Generated Type: Varyings
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS;
                float3 normalWS;
                float3 viewDirectionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Generated Type: PackedVaryings
            struct PackedVaryings
            {
                float4 positionCS : SV_POSITION;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                float3 interp00 : TEXCOORD0;
                float3 interp01 : TEXCOORD1;
                float3 interp02 : TEXCOORD2;
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };
            
            // Packed Type: Varyings
            PackedVaryings PackVaryings(Varyings input)
            {
                PackedVaryings output = (PackedVaryings)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionWS;
                output.interp01.xyz = input.normalWS;
                output.interp02.xyz = input.viewDirectionWS;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
            
            // Unpacked Type: Varyings
            Varyings UnpackVaryings(PackedVaryings input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = input.positionCS;
                output.positionWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                output.viewDirectionWS = input.interp02.xyz;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
            {
                VertexDescriptionInputs output;
                ZERO_INITIALIZE(VertexDescriptionInputs, output);
            
                output.ObjectSpaceNormal =           input.normalOS;
                output.WorldSpaceNormal =            TransformObjectToWorldNormal(input.normalOS);
                output.ObjectSpaceTangent =          input.tangentOS;
                output.ObjectSpacePosition =         input.positionOS;
                output.WorldSpacePosition =          TransformObjectToWorld(input.positionOS);
                output.TimeParameters =              _TimeParameters.xyz;
            
                return output;
            }
            
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
            {
                SurfaceDescriptionInputs output;
                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
            
            	// must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            	float3 unnormalizedNormalWS = input.normalWS;
                const float renormFactor = 1.0 / length(unnormalizedNormalWS);
            
            
                output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);
            
            
                output.WorldSpaceViewDirection =     input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                output.WorldSpacePosition =          input.positionWS;
                output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
                output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
            #else
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            #endif
            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            
                return output;
            }
            
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBR2DPass.hlsl"
        
            ENDHLSL
        }
        
    }
    CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
}

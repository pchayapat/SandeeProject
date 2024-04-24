// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Knife/Diamond"
{
	Properties
	{
		_Height("Height", 2D) = "white" {}
		_Scale("Scale", Float) = 0
		_RefPlane("Ref Plane", Float) = 0
		_Noise("Noise", 2D) = "white" {}
		[HDR]_NoiseColor("Noise Color", Color) = (0,0,0,0)
		_ColorTIling("Color TIling", Float) = 0
		[HDR]_ColorFront("Color Front", Color) = (0,0,0,0)
		_AlphaFresnelScale("Alpha  Fresnel Scale", Float) = 0
		_AlphaFresnelPower("Alpha Fresnel Power", Float) = 0
		_FresnelScale("Fresnel Scale", Float) = 0
		_FresnelPower("Fresnel Power", Float) = 0
		_InnerFresnelScale("Inner Fresnel Scale", Float) = 0
		_InnerFresnelPower("Inner Fresnel Power", Float) = 0
		_InnerFresnelColor("Inner Fresnel Color", Color) = (0,0,0,0)
		_Distortion("Distortion", Float) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_MainTex("MainTex", 2D) = "white" {}
		_Speed("Speed", Vector) = (0,0,0,0)
		_NoiseEmissionScale("Noise Emission Scale", Float) = 0
		_NoiseEmissionSpeed("Noise Emission Speed", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		GrabPass{ }
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float4 screenPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
			float3 viewDir;
			float3 worldPos;
		};

		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _Distortion;
		uniform sampler2D _Noise;
		uniform float2 _Speed;
		uniform sampler2D _Height;
		uniform float4 _Height_ST;
		uniform float _Scale;
		uniform float _RefPlane;
		uniform float _ColorTIling;
		uniform float4 _NoiseColor;
		uniform float _NoiseEmissionSpeed;
		uniform float _NoiseEmissionScale;
		uniform float4 _ColorFront;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _InnerFresnelColor;
		uniform float _InnerFresnelScale;
		uniform float _InnerFresnelPower;
		uniform float _FresnelScale;
		uniform float _FresnelPower;
		uniform float _AlphaFresnelScale;
		uniform float _AlphaFresnelPower;
		uniform float _Metallic;
		uniform float _Smoothness;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		inline float2 POM( sampler2D heightMap, float2 uvs, float2 dx, float2 dy, float3 normalWorld, float3 viewWorld, float3 viewDirTan, int minSamples, int maxSamples, float parallax, float refPlane, float2 tilling, float2 curv, int index )
		{
			float3 result = 0;
			int stepIndex = 0;
			int numSteps = ( int )lerp( (float)maxSamples, (float)minSamples, saturate( dot( normalWorld, viewWorld ) ) );
			float layerHeight = 1.0 / numSteps;
			float2 plane = parallax * ( viewDirTan.xy / viewDirTan.z );
			uvs.xy += refPlane * plane;
			float2 deltaTex = -plane * layerHeight;
			float2 prevTexOffset = 0;
			float prevRayZ = 1.0f;
			float prevHeight = 0.0f;
			float2 currTexOffset = deltaTex;
			float currRayZ = 1.0f - layerHeight;
			float currHeight = 0.0f;
			float intersection = 0;
			float2 finalTexOffset = 0;
			while ( stepIndex < numSteps + 1 )
			{
			 	currHeight = tex2Dgrad( heightMap, uvs + currTexOffset, dx, dy ).r;
			 	if ( currHeight > currRayZ )
			 	{
			 	 	stepIndex = numSteps + 1;
			 	}
			 	else
			 	{
			 	 	stepIndex++;
			 	 	prevTexOffset = currTexOffset;
			 	 	prevRayZ = currRayZ;
			 	 	prevHeight = currHeight;
			 	 	currTexOffset += deltaTex;
			 	 	currRayZ -= layerHeight;
			 	}
			}
			int sectionSteps = 2;
			int sectionIndex = 0;
			float newZ = 0;
			float newHeight = 0;
			while ( sectionIndex < sectionSteps )
			{
			 	intersection = ( prevHeight - prevRayZ ) / ( prevHeight - currHeight + currRayZ - prevRayZ );
			 	finalTexOffset = prevTexOffset + intersection * deltaTex;
			 	newZ = prevRayZ - intersection * layerHeight;
			 	newHeight = tex2Dgrad( heightMap, uvs + finalTexOffset, dx, dy ).r;
			 	if ( newHeight > newZ )
			 	{
			 	 	currTexOffset = finalTexOffset;
			 	 	currHeight = newHeight;
			 	 	currRayZ = newZ;
			 	 	deltaTex = intersection * deltaTex;
			 	 	layerHeight = intersection * layerHeight;
			 	}
			 	else
			 	{
			 	 	prevTexOffset = finalTexOffset;
			 	 	prevHeight = newHeight;
			 	 	prevRayZ = newZ;
			 	 	deltaTex = ( 1 - intersection ) * deltaTex;
			 	 	layerHeight = ( 1 - intersection ) * layerHeight;
			 	}
			 	sectionIndex++;
			}
			return uvs.xy + finalTexOffset;
		}


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float3 objectToClipDir200 = normalize( mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, float4(ase_vertexNormal, 0.0))) );
			float4 screenColor196 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( ase_grabScreenPosNorm + float4( ( objectToClipDir200 * _Distortion ) , 0.0 ) ).xy);
			float2 uv_Height = i.uv_texcoord * _Height_ST.xy + _Height_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float2 OffsetPOM145 = POM( _Height, uv_Height, ddx(uv_Height), ddy(uv_Height), ase_worldNormal, ase_worldViewDir, i.viewDir, 23, 23, _Scale, _RefPlane, _Height_ST.xy, float2(0,0), 0 );
			float2 panner235 = ( 1.0 * _Time.y * _Speed + OffsetPOM145);
			float2 temp_output_167_0 = ( panner235 * _ColorTIling );
			float4 temp_cast_2 = (1.0).xxxx;
			float mulTime242 = _Time.y * _NoiseEmissionSpeed;
			float3 appendResult241 = (float3(temp_output_167_0 , mulTime242));
			float simplePerlin3D239 = snoise( appendResult241*_NoiseEmissionScale );
			simplePerlin3D239 = simplePerlin3D239*0.5 + 0.5;
			float4 lerpResult244 = lerp( temp_cast_2 , _NoiseColor , simplePerlin3D239);
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float fresnelNdotV215 = dot( ase_normWorldNormal, ase_worldViewDir );
			float fresnelNode215 = ( 0.0 + _InnerFresnelScale * pow( max( 1.0 - fresnelNdotV215 , 0.0001 ), _InnerFresnelPower ) );
			float4 appendResult224 = (float4(1.0 , 1.0 , 1.0 , 1.0));
			float fresnelNdotV155 = dot( ase_normWorldNormal, ase_worldViewDir );
			float fresnelNode155 = ( 0.0 + _FresnelScale * pow( max( 1.0 - fresnelNdotV155 , 0.0001 ), _FresnelPower ) );
			float4 temp_output_161_0 = ( ( tex2D( _Noise, temp_output_167_0 ) * lerpResult244 ) + ( _ColorFront * tex2D( _MainTex, uv_MainTex ) ) + ( _InnerFresnelColor * ( 1.0 - saturate( fresnelNode215 ) ) ) + ( appendResult224 * abs( fresnelNode155 ) ) );
			float fresnelNdotV228 = dot( ase_normWorldNormal, ase_worldViewDir );
			float fresnelNode228 = ( 0.0 + _AlphaFresnelScale * pow( max( 1.0 - fresnelNdotV228 , 0.0001 ), _AlphaFresnelPower ) );
			float4 lerpResult203 = lerp( screenColor196 , temp_output_161_0 , ( (_ColorFront).a * ( 1.0 - saturate( fresnelNode228 ) ) ));
			float4 appendResult206 = (float4((lerpResult203).rgb , 1.0));
			o.Emission = appendResult206.xyz;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = IN.tSpace0.xyz * worldViewDir.x + IN.tSpace1.xyz * worldViewDir.y + IN.tSpace2.xyz * worldViewDir.z;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
-2039;1062;1920;831;1254.1;777.2986;2.03551;True;False
Node;AmplifyShaderEditor.RangedFloatNode;149;-1530.866,-653.0626;Inherit;False;Property;_Scale;Scale;2;0;Create;True;0;0;0;False;0;False;0;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;150;-1646.753,-521.1414;Inherit;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;151;-1639.131,-362.8159;Inherit;False;Property;_RefPlane;Ref Plane;3;0;Create;True;0;0;0;False;0;False;0;49;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;146;-1665.108,-983.4446;Inherit;False;0;148;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;148;-1767.208,-757.0447;Inherit;True;Property;_Height;Height;0;0;Create;True;0;0;0;False;0;False;None;4bfca74bad35bc84b8d91460795c7d7e;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.Vector2Node;236;-963.6069,-684.3999;Inherit;False;Property;_Speed;Speed;20;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ParallaxOcclusionMappingNode;145;-1220.974,-759.649;Inherit;False;0;23;False;-1;47;False;-1;2;0.02;0;False;1,1;False;0,0;8;0;FLOAT2;0,0;False;1;SAMPLER2D;;False;7;SAMPLERSTATE;;False;2;FLOAT;0.02;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT2;0,0;False;6;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;166;-694.1446,-648.2286;Inherit;False;Property;_ColorTIling;Color TIling;6;0;Create;True;0;0;0;False;0;False;0;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;235;-853.6069,-887.3999;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;243;-924.4924,-502.4949;Inherit;False;Property;_NoiseEmissionSpeed;Noise Emission Speed;22;0;Create;True;0;0;0;False;0;False;0;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;217;-984.1801,836.1089;Inherit;False;Property;_InnerFresnelPower;Inner Fresnel Power;13;0;Create;True;0;0;0;False;0;False;0;0.91;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;218;-1001.38,736.5092;Inherit;False;Property;_InnerFresnelScale;Inner Fresnel Scale;12;0;Create;True;0;0;0;False;0;False;0;2.61;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;167;-554.3492,-737.0659;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;242;-670.4924,-553.4949;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;241;-463.4924,-587.4949;Inherit;False;FLOAT3;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;240;-683.4924,-484.4949;Inherit;False;Property;_NoiseEmissionScale;Noise Emission Scale;21;0;Create;True;0;0;0;False;0;False;0;19.67;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;157;-1030.74,522.3206;Inherit;False;Property;_FresnelPower;Fresnel Power;11;0;Create;True;0;0;0;False;0;False;0;2.59;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;215;-720.7375,695.4123;Inherit;False;Standard;WorldNormal;ViewDir;True;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;199;-71.28793,1012.059;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;227;-60.8213,294.4848;Inherit;False;Property;_AlphaFresnelPower;Alpha Fresnel Power;9;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;156;-1047.94,422.7209;Inherit;False;Property;_FresnelScale;Fresnel Scale;10;0;Create;True;0;0;0;False;0;False;0;1.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;226;-78.02119,194.8851;Inherit;False;Property;_AlphaFresnelScale;Alpha  Fresnel Scale;8;0;Create;True;0;0;0;False;0;False;0;0.57;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;239;-345.4924,-536.4949;Inherit;False;Simplex3D;True;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;222;-461.4576,684.5108;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;245;-213.4924,-402.4949;Inherit;False;Constant;_Float2;Float 2;23;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;201;231.5885,1184.994;Inherit;False;Property;_Distortion;Distortion;15;0;Create;True;0;0;0;False;0;False;0;0.53;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TransformDirectionNode;200;146.9821,994.2635;Inherit;False;Object;Clip;True;Fast;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;223;-427.15,105.9591;Inherit;False;Constant;_Float1;Float 1;17;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;155;-767.2975,381.624;Inherit;False;Standard;WorldNormal;ViewDir;True;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;237;-602.1951,-353.1467;Inherit;False;Property;_NoiseColor;Noise Color;5;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;3.08031,3.08031,3.08031,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;228;202.6213,153.7882;Inherit;False;Standard;WorldNormal;ViewDir;True;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;152;-383.4998,-744.4979;Inherit;True;Property;_Noise;Noise;4;0;Create;True;0;0;0;False;0;False;-1;None;4bfca74bad35bc84b8d91460795c7d7e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;244;-81.49243,-411.4949;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GrabScreenPosition;197;258.512,782.8593;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;176;-708.2452,-165.0728;Inherit;True;Property;_MainTex;MainTex;18;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;202;389.5883,1006.994;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;153;-948.1337,-405.5876;Inherit;False;Property;_ColorFront;Color Front;7;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0.490566,0,0.2501181,0.7686275;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;220;-448.4576,426.5108;Inherit;False;Property;_InnerFresnelColor;Inner Fresnel Color;14;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.5986347,0,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;230;398.5092,73.18164;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;160;-523.0919,343.2247;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;219;-273.4576,601.5108;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;224;-293.15,55.95908;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;198;521.5121,840.8594;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;195;-319.2569,-210.6341;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;231;509.5092,16.18164;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;238;93.00134,-438.2827;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;159;-101.3355,57.7821;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;221;-52.45764,422.5108;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;181;-59.4104,-112.9545;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;196;695.6269,778.6714;Inherit;False;Global;_GrabScreen0;Grab Screen 0;11;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;161;252.0045,-190.7825;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;229;634.5092,-103.8184;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;203;938.9884,-163.8064;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;205;1113.988,-163.8064;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;207;1215.988,-14.8064;Inherit;False;Constant;_Float0;Float 0;12;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;213;1540.118,600.6562;Inherit;False;Property;_Smoothness;Smoothness;16;0;Create;True;0;0;0;False;0;False;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;206;1398.988,-136.8064;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;233;1579.509,2.181641;Inherit;False;Property;_Emission;Emission;19;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;214;1531.118,502.6562;Inherit;False;Property;_Metallic;Metallic;17;0;Create;True;0;0;0;False;0;False;0;0.174;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;182;613.1162,-304.1565;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;234;1791.509,-8.818359;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;211;2191.81,-209.3223;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Knife/Diamond;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;145;0;146;0
WireConnection;145;1;148;0
WireConnection;145;2;149;0
WireConnection;145;3;150;0
WireConnection;145;4;151;0
WireConnection;235;0;145;0
WireConnection;235;2;236;0
WireConnection;167;0;235;0
WireConnection;167;1;166;0
WireConnection;242;0;243;0
WireConnection;241;0;167;0
WireConnection;241;2;242;0
WireConnection;215;2;218;0
WireConnection;215;3;217;0
WireConnection;239;0;241;0
WireConnection;239;1;240;0
WireConnection;222;0;215;0
WireConnection;200;0;199;0
WireConnection;155;2;156;0
WireConnection;155;3;157;0
WireConnection;228;2;226;0
WireConnection;228;3;227;0
WireConnection;152;1;167;0
WireConnection;244;0;245;0
WireConnection;244;1;237;0
WireConnection;244;2;239;0
WireConnection;202;0;200;0
WireConnection;202;1;201;0
WireConnection;230;0;228;0
WireConnection;160;0;155;0
WireConnection;219;0;222;0
WireConnection;224;0;223;0
WireConnection;224;1;223;0
WireConnection;224;2;223;0
WireConnection;224;3;223;0
WireConnection;198;0;197;0
WireConnection;198;1;202;0
WireConnection;195;0;153;0
WireConnection;231;0;230;0
WireConnection;238;0;152;0
WireConnection;238;1;244;0
WireConnection;159;0;224;0
WireConnection;159;1;160;0
WireConnection;221;0;220;0
WireConnection;221;1;219;0
WireConnection;181;0;153;0
WireConnection;181;1;176;0
WireConnection;196;0;198;0
WireConnection;161;0;238;0
WireConnection;161;1;181;0
WireConnection;161;2;221;0
WireConnection;161;3;159;0
WireConnection;229;0;195;0
WireConnection;229;1;231;0
WireConnection;203;0;196;0
WireConnection;203;1;161;0
WireConnection;203;2;229;0
WireConnection;205;0;203;0
WireConnection;206;0;205;0
WireConnection;206;3;207;0
WireConnection;182;0;161;0
WireConnection;234;0;161;0
WireConnection;234;1;233;0
WireConnection;211;2;206;0
WireConnection;211;3;214;0
WireConnection;211;4;213;0
ASEEND*/
//CHKSM=8820C1D99E49CD2DD9AE80B002581BECF2DA01D1
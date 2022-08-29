Shader "Unlit/AnalogDistortion"
{
	Properties
	{
		//Main Texture
		_MainTex("Texture", 2D) = "" {}
		// Scan Line Jutter properties - the first gives the amount of jutter, while the second gives a threshold above which the jutter happens
		_ScanLineJitterDisp("Scan Line Jutter Amount", Range(0,1)) = 0
		_ScanLineJitterThresh("Scan Line Jutter Threshold", Range(0,1)) = 0
		//Vertical jump properties - the first gives the amout of jumping, while the second how fast the jump effect happens
		_VerticalJumpAmount("Amount of vertical jump", Float) = 0
		_VerticalJumpTime("Time of vertical jump", Float) = 0
		//Horizontal shake property - how much shake there will be
		_HorizontalShake("Amout of horizontal Shake", Float) = 0
		//Color drift properties - moving chromatic aberation, the first property specifies the amout of movement in horizontal direction,
		// the second propety specifies the speed based on timing
		_ColorDriftpAmount("Amount of color drift", Float) = 0
		_ColorDriftTime("Time of color drift", Float) = 0
	}
		Subshader
		{
			Pass
			{

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0

				
				// Initialize the used properties
				sampler2D _MainTex;
				float2 _MainTex_TexelSize;

				float _ScanLineJitterDisp;
				float _ScanLineJitterThresh;
				float _VerticalJumpAmount;
				float _VerticalJumpTime;
				float _HorizontalShake;
				float _ColorDriftpAmount;
				float _ColorDriftTime;

				//Cannonical random function used in cg and hlsl. The chosen values have been shown to give good pseudo random results
				float nrand(float x, float y)
				{
					return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 43758.5453);
				}

				//Setup the vertex shader input structure
				struct MeshData
				{
					float4 vertex : POSITION;
					float4 uv : TEXCOORD0;
				};
				//Setup the fragment shader input structure
				struct Interpolators
				{
					float4 vertex : SV_POSITION;
					float2 uv : TEXCOORD0;
					
				};

				//Vertex shader
				Interpolators vert(MeshData v)
				{
					Interpolators o;
					//transform the vertex coordinates from object to clip space for visualization
					o.vertex = UnityObjectToClipPos(v.vertex);
					//save the texture UV coordinates
					o.uv = v.uv;
					return o;
				}

				//Fragment shader
				float4 frag(Interpolators i) : COLOR
				{
					//separate the x and y coordinates of the UVs
					float u = i.uv.x;
					float v = i.uv.y;

					// Scan line jitter
					// Calculate for each fragment a random value using the time/20 since start of program. Multiply by 2 and subract 1 to get between (0,1)
					float jitter = nrand(v, _Time.x) * 2 - 1;
					// Multiply the jitter with a step mask where the random jitter values are bigger than the given threshold. Multiply the whole thing by the amount of jitter value
					jitter *= step(_ScanLineJitterThresh, abs(jitter)) * _ScanLineJitterDisp;

					// Vertical jump
					// We use lerp to linearly interpolate between the v UV coordinates and a value moved with time. Frac is used to clamp the moved values between 0 and 1.
					float jump = lerp(v, frac(v + _VerticalJumpTime * _Time.y), _VerticalJumpAmount);

					// Horizontal shake
					// Use the pseudo random function to calculate horizontal movement values between the current time and an arbitrary value 2. We remove 0.5 to get fit it horizontally
					float shake = (nrand(_Time.x, 2) - 0.5) * _HorizontalShake;

					// Color drift
					// use the sine function to create a moving sine wave with time and a specific amplitude. This drift will be used for the green color to create chromatic drift.
					// If there is any vertical jump this is factored in calculation of the sine wave
					float drift = sin(jump + _ColorDriftTime * _Time.y) * _ColorDriftpAmount;
					// Sample the texture once using the U values augmented by the jitter and shake noise, together with the V values augmented by the jump distortion
					float4 src1 = tex2D(_MainTex, frac(float2(u + jitter + shake, jump)));
					// Same as the above, but add the drift change to the U values
					float4 src2 = tex2D(_MainTex, frac(float2(u + jitter + shake + drift, jump)));
					// output the final colors using the R,B colors without the chroma drift and the G colors with the drift
					return float4(src1.r, src2.g, src1.b, 1);

				}
				ENDCG
			}
		}
}

# CGP Example Mini-project

![Visual](Images/ExampleMiniProject.gif)


The mini project presents a number of simple analog distortions that can be applied to either mesh textures or directly to a Render texture for camera feed distortion. The code is based on the work by Keijiro Takahashi [Github Link](https://github.com/Yiiip/KinoGlitch). Overall four distortions are developed - vertical per pixel jitter, vertical jump distortion, horizontal shake distortion and chromatic aberation distortion. 

The shader contains a number of properties that can be manually tweaked or manipulated through C# code:
- _ScanLineJitterDisp - scanline jitter amount
- _ScanLineJitterThresh - the threshold above which the jitter is going to be applied on the UV coordinate
- _VerticalJumpAmount - vertical jump amount
- _VerticalJumpTime - the speed with which the vertical jump is done, using the _Time.y value
- _HorizontalShake - horizontal shake amount
- _ColorDriftAmount - chromatic aberation amount
- _COlorDriftTime - the speed with which the chromatic averation is done, using the _Time.y value

As part of the distortions a pseudo randomization function is used 

```
float nrand(float x, float y)
{
  return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 43758.5453);
}
```

All the calculations are done in the fragment shader.

## Running the code

1. Clone the repository or download it
2. Open with Unity - requires versions > 2019
3. For texture distortion
  - Create a primitive, set the material to the primitive, add the frog texture to the material
  - Push play and change the shader parameters to see the different distortions
4. For Render texture distortion
  - Create a second camera, remove the Listener from it
  - Add the Render texture to the TargetTexture of the camera
  - Add the Render texture to the material of the shader
  - Add the material to a plane or quad

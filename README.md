# Unity Homework 6: Pixelization Shader
Author: Viktor Kostadinov, FN: 26312

# Implementation details
## Usage 
The scene from the first homework was used. It should be the default scene when the project is open. The following files are relevant to the Shader exercise:
* Effects/PixelizationEffect.shader
* Materials/Pixelate.mat
* Scripts/PixelizationEffect.cs

## Shader Implementation
The shader is created as an Image Effect Shader. It is then applied to a material, which is attached to a script, associated with the main camera of the scene. The effect works as follows:
1. Take the current pixel coordinates (2D float between 0 and 1)
2. Multiply the horizontal and vertical components by the selected resolution
3. Round the coordinates, resulting in a number between 0 and `_HRES`/`_VRES`
4. Divide the coordinates by the selected resolution, goind back to a number between 0 and 1
5. Set the pixel color to the color of the pixel located at the resulting coordinates in the original image

The practical result is that the image is divided into small squares and all pixels inside a square have the color of the pixel at the top-left coordinates of that square.

# Alternative Options
This shader might be more suited to a localized effect, like censoring and area of the screen (ala The Sims censoring), since the outlined logic produces rapid changes in the color of each individual scquare, resulting in a characteristic "simmer effect" as the subjects move. An alternative implementation might average the color of all pixels in the square (instead of looking at just the top-left pixel), outputting a much smoother, almost anti-aliased movement.
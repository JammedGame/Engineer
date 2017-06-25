#version 400
struct Light
{
	vec3 Color;
	vec3 Position;
	vec3 Attenuation;
	float Intensity;
};
uniform vec3 CameraPosition;
uniform mat4 ModelView;
uniform mat4 Projection;
uniform Light Lights[1/*NumLights*/];
uniform sampler2DArray Textures;
in vec3 F_Vertex;
in vec3 F_Normal;
in vec2 F_TextureUV;
out vec4 FinalColor;
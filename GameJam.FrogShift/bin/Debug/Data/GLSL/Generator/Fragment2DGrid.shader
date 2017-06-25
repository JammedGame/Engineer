#version 400
uniform int Index;
uniform vec4 Color;
uniform mat4 ModelView;
uniform mat4 Projection;
in vec3 F_Vertex;
out vec4 FinalColor;

void main()
{
	FinalColor = Color;
}
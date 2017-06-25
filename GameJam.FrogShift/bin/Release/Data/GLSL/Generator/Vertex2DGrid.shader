#version 400
uniform mat4 ModelView;
uniform mat4 Projection;
in vec3 V_Vertex;
out vec3 F_Vertex;

void main()
{
	F_Vertex = V_Vertex;
	gl_Position = Projection * ModelView * vec4(V_Vertex, 1);
}
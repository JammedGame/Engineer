#version 400
uniform mat4 ModelView;
uniform mat4 Projection;
in vec3 V_Vertex;
in vec3 V_Normal;
in vec2 V_TextureUV;
//out vec3 G_Vertex;
//out vec3 G_Normal;
//out vec2 G_TextureUV;

out VertexData
{
    vec3 normal;
} VertexOut;

void main()
{
	VertexOut.normal = vec3(Projection * ModelView * vec4(V_Normal, 1));
	gl_Position = Projection * ModelView * vec4(V_Vertex, 1);
}
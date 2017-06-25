#version 400
layout (points) in;
layout (line_strip, max_vertices = 2) out;

out VertexData {
    vec3 normal;
} VertexOut;


void main()
{   
	for(int i = 0; i < gl_in.length(); i++)
	  {
		gl_Position = gl_in[i].gl_Position + vec4(0.0, 0.0, 0.0, 0.0); 
		EmitVertex();

		gl_Position = gl_in[i].gl_Position + vec4(1, 0.0, 0.0, 0.0);
		EmitVertex();
		
		gl_Position = gl_in[i].gl_Position + vec4(1, 1.0, 0.0, 0.0);
		EmitVertex();
	  }
    EndPrimitive();
}  
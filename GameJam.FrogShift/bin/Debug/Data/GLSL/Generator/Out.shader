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
in vec3 F_Vertex;
in vec3 F_Normal;
in vec3 F_TextureUV;
out vec4 FinalColor;

void Diffuse(in Light Lights[1/*NumLights*/], in vec4 Color, out vec4 Diffuse)
{
	mat3 NormalMatrix = transpose(inverse(mat3(ModelView)));
	vec3 FinalLight = vec3(0,0,0);
	vec3 Normal = normalize(NormalMatrix * F_Normal);
	vec3 SurfacePosition = vec3(Projection * ModelView * vec4(F_Vertex, 1));
	for (int i = 0; i < 1/*NumLights*/; i++)
	{
		vec3 SurfaceToLight = normalize(SurfacePosition - mat3(ModelView) * Lights[i].Position);
		float DistanceToLight = length(mat3(ModelView) * Lights[i].Position - SurfacePosition);
		float CurrentAttenuation = Lights[i].Intensity / (Lights[i].Attenuation.x + Lights[i].Attenuation.y * DistanceToLight + Lights[i].Attenuation.z * DistanceToLight * DistanceToLight);
		float DiffuseCoefficient = max(0.0, dot(Normal, SurfaceToLight));
		vec3 CurrentDiffuse = DiffuseCoefficient * Color.rgb * Lights[i].Color;
		FinalLight = FinalLight + CurrentAttenuation * CurrentDiffuse;
	}
	vec3 Gamma = vec3(1.0 / 2.2);
	Diffuse = vec4(pow(FinalLight * Color.rgb, Gamma), Color.a);
}

void Ambient(in vec4 Color, out vec4 Ambient)
{
	Ambient = Color;
}

void Add(in vec4 Input1, in vec4 Input2, out vec4 Result)
{
	Result = max(Input1, Input2);
}

void main()
{
	vec4 Diffuse_Diffuse;
	Diffuse (Lights,vec4(0.796875,0.796875,0.796875,1),Diffuse_Diffuse);
	vec4 Ambient_Ambient;
	Ambient (vec4(0.19921875,0.19921875,0.19921875,1),Ambient_Ambient);
	vec4 Add_Result;
	Add (Ambient_Ambient,Diffuse_Diffuse,Add_Result);
	FinalColor = Add_Result;
}
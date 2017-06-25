void ImageMap(in int Index, out vec4 Color)
{
	if (Index == -1)
	{
		Color = vec4(0,0,0,1);
	}
	else
	{
		Color = texture(Textures, vec3(F_TextureUV,Index));
	}
}
#version 330 core

uniform sampler2D Texture;

in vec2 vUV;

  
out vec4 FragColor;

void main()
{
    FragColor = texture(Texture, vUV);
}
#version 330 core

uniform sampler2D Texture;
uniform vec4 Color;

//in vec4 vColor;
in vec2 vUV;

  
out vec4 FragColor;

void main()
{
    FragColor = texture(Texture, vUV);
    if(Color.w != 0){
        FragColor = vec4(Color.xyz,texture(Texture, vUV).w);
    }
}
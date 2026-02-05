#version 330 core

uniform sampler2D Texture;
uniform vec4 Color;

in vec2 vUV;

  
out vec4 FragColor;

void main()
{
    if(Color.w > 0){
        FragColor = Color;
    }
    else{
        vec4 color = texture(Texture, vUV);
        if(color.w == 0) discard;
        FragColor = color;
    }
}
#version 330 core

uniform vec2 WindowSize;
uniform vec2 TextureSize;
uniform float Depth;

uniform vec4 PositionSize;
uniform vec4 UVPositionSize;

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aUV;
  
out vec2 vUV;

void main()
{
    vUV = vec2(
        UVPositionSize.x + aUV.x*UVPositionSize.z, 
        UVPositionSize.y + aUV.y*UVPositionSize.w);
    gl_Position = vec4(
        PositionSize.x + aPosition.x*PositionSize.z, 
        PositionSize.y + aPosition.y*PositionSize.w,
        Depth,1);
}  
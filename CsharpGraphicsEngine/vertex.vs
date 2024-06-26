#version 330 core

uniform vec2 WindowSize;
uniform vec2 TextureSize;

uniform vec4 PositionSize;
uniform vec4 UVPositionSize;

layout (location = 0) in vec3 aPosition;
//layout (location = 1) in vec4 aColor;
//layout (location = 2) in vec2 aUV;
layout (location = 1) in vec2 aUV;
  
//out vec4 vColor;
out vec2 vUV;

void main()
{
    
    vUV = aUV;// vec2(aUV.x, aUV.y);
    //vUV = vec2(
        //UVPositionSize.x/TextureSize.x + 
        //aUV.x*UVPositionSize.z/TextureSize.x, 
        //UVPositionSize.y/TextureSize.y + 
        //aUV.y*UVPositionSize.w/TextureSize.y);
    //vUV = vec2(
    //    UVPositionSize.x/TextureSize.x + aUV.x*UVPositionSize.z/TextureSize.x, 
    //    UVPositionSize.y/TextureSize.y + aUV.y*UVPositionSize.w/TextureSize.y);
    //vColor = aColor;

    gl_Position = vec4(
        2*PositionSize.x/WindowSize.x - 1 + 2*aPosition.x*PositionSize.z/WindowSize.x, 
        2*PositionSize.y/WindowSize.y - 1 + 2*aPosition.y*PositionSize.w/WindowSize.y,
        aPosition.z,1);

}  
#version 330

//layout(location = 0) in vec3 vertexPosition_modelspace;
//layout(location = 1) in vec2 vertexUV;

in vec3 vPosition;
in vec2 vColor;

out vec2 UV;
uniform mat4 modelview;

void
main()
{
    gl_Position = modelview * vec4(vPosition, 1.0);
 
    //color = vec4( vColor, 1.0);
	UV = vColor;
}
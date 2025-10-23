#version 330

out vec4 outputColor;

// uniform vec4 uColor;
in vec3 vColor; // Menerima warna terinterpolasi dari Vertex Shader

void main()
{
    // outputColor = vec4(1.0, 1.0, 0.0, 1.0);
    // outputColor = uColor;
    outputColor = vec4(vColor, 1.0);
}
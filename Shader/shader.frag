#version 330

out vec4 outputColor;

// Uniform untuk menerima nilai warna (RGBA) dari C#
// uniform vec4 uColor;
in vec3 vColor; // Menerima warna terinterpolasi dari Vertex Shader

// UNIFORMS (Dari C#)
uniform int useUniformColor; // Sakelar: 0 = Gunakan Attribute, 1 = Gunakan Uniform
uniform vec4 uSolidColor;    // Warna solid (dari Uniform)

void main()
{
    // outputColor = vec4(1.0, 1.0, 0.0, 1.0);
    // outputColor = uColor;
    // outputColor = vec4(vColor, 1.0);
    
    if (useUniformColor == 1) {
        // KONDISI 1: Gunakan warna solid dari Uniform
        outputColor = uSolidColor;
    } else {
        // KONDISI 2: Gunakan warna per-vertex (gradien) dari Attribute
        outputColor = vec4(vColor, 1.0);
    }
}
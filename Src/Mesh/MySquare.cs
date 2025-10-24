using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace cs_gl.Src.Mesh;

public class MySquare : MyMesh
{
    private List<float> _vertices;
    private float rotationAngle=0;
    public MySquare(int  x, int y, int z, int xLength, int yLength, int zLength) : base([],false)
    {
        Enable = true;
        _vertices =
        [
            x, y, z,
            x + xLength, y, z+zLength,
            x + xLength, y+yLength, z+zLength,
            
            x + xLength, y+yLength, z+zLength,
            x, y+yLength, z,
            x, y, z,
        ];
    }

    public override MyMesh Build()
    {
        int sizeOfFloat = sizeof(float);
        int stride = 3 * sizeOfFloat; // 24 byte total per vertex

        var vertices = _vertices.ToArray();

        VboHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VboHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
            BufferUsageHint.StaticDraw);

        VaoHandle = GL.GenVertexArray();
        GL.BindVertexArray(VaoHandle);

        // --- KONFIGURASI ATTRIBUTE 0: POSISI ---
        GL.VertexAttribPointer(
            0, // Location 0 (aPosition di shader)
            3, // Size 3 (X, Y, Z)
            VertexAttribPointerType.Float,
            false,
            stride, // Stride 6 float: Jarak ke Posisi vertex berikutnya
            0 // Offset 0: Posisi dimulai dari awal vertex
        );
        GL.EnableVertexAttribArray(0);

        // --- KONFIGURASI ATTRIBUTE 1: WARNA ---
        // GL.VertexAttribPointer(
        //     1, // Location 1 (aColor di shader)
        //     3, // Size 3 (R, G, B)
        //     VertexAttribPointerType.Float,
        //     false,
        //     stride, // Stride 6 float: Jarak ke Warna vertex berikutnya
        //     3 * sizeOfFloat // Offset 3 float: Warna dimulai setelah 3 float Posisi
        // );
        // GL.EnableVertexAttribArray(1); // Aktifkan Location 1

        // Unbind
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // VertexCount = total float / (float per vertex)
        VertexCount = vertices.Length / 3;
        return this;
    }

    public override void Draw(int colorSwitchLoc, int solidColorLoc, int transformLoc)
    {
        GL.Uniform1(colorSwitchLoc, 1);
        GL.Uniform4(solidColorLoc, new Vector4(1f,1f,0f,1f));
        
        GL.BindVertexArray(VaoHandle);

        var transformMatrix = GetTransformMatrix();
        GL.UniformMatrix4(transformLoc, false, ref transformMatrix);
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, VertexCount);
    }

    public override void Forward()
    {
        rotationAngle += 1;
        if (rotationAngle >= 360)
        {
            rotationAngle = 0;
        }
    }

    public override Matrix4 GetTransformMatrix()
    {
        var transform = Matrix4.Identity;
        transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationAngle));
        return transform;
    }
}
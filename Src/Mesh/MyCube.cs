using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace cs_gl.Src.Mesh;

public class MyCube:MyMesh
{
    private float rotationAngle;
    public MyCube(float[] vertices):base([],false)
    {
        rotationAngle = 0;
        Enable = true;
        int sizeOfFloat = sizeof(float);
        int stride = 6 * sizeOfFloat; // 24 byte total per vertex

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
        GL.VertexAttribPointer(
            1, // Location 1 (aColor di shader)
            3, // Size 3 (R, G, B)
            VertexAttribPointerType.Float,
            false,
            stride, // Stride 6 float: Jarak ke Warna vertex berikutnya
            3 * sizeOfFloat // Offset 3 float: Warna dimulai setelah 3 float Posisi
        );
        GL.EnableVertexAttribArray(1); // Aktifkan Location 1

        // Unbind
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // VertexCount = total float / (float per vertex)
        VertexCount = vertices.Length / 6;
    }

    public override void Draw(int colorSwitchLoc, int solidColorLoc, int transformLoc)
    {
        if (Enable == false)
        {
            return;
        }
        GL.Uniform1(colorSwitchLoc, 0);
        GL.Uniform4(solidColorLoc, new Vector4(1f,1f,0f,1f));
        
        GL.BindVertexArray(VaoHandle);

        // var transform = Matrix4.Identity;
        // transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationAngle));
        
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

    public override Matrix4 GetDynamicTransformMatrix()
    {
        var transform = Matrix4.Identity;
        transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationAngle));
        return transform;
    }

    public override MyMesh Build()
    {
        throw new NotImplementedException();
    }
}
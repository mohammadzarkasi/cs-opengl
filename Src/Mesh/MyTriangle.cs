using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace cs_gl.Src.Mesh;

public class MyTriangle:MyMesh
{

    public static float[] TriangleToVertices(List<MyTriangle>  triangles)
    {
        List<float> result = [];
        foreach (var t in triangles)
        {
            var v = t.getVertices();
            foreach (var v1 in v)
            {
                result.Add(v1);
            }
        }
        return result.ToArray();
    }

    private List<float> _vertices;
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public MyTriangle(Vector3 a, Vector3 b, Vector3 c) : base([], false)
    {
        _vertices = [
            a.X, a.Y, a.Z,
            b.X, b.Y, b.Z, 
            c.X, c.Y, c.Z
        ];
        Console.WriteLine("triangle: " + Helper.PrintListFloat(_vertices,3));
    }

    public override void Draw(int colorSwitchLoc, int solidColorLoc, int transformLoc)
    {
        GL.Uniform1(colorSwitchLoc, 1);
        GL.Uniform4(solidColorLoc, solidColor);
        GL.BindVertexArray(VaoHandle);

        // var transformMatrix = GetTransformMatrix();
        // var transformMatrix = initialTransform * GetDynamicTransformMatrix();
        var transformMatrix =  GetDynamicTransformMatrix();
     
        GL.UniformMatrix4(transformLoc, false, ref transformMatrix);
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, VertexCount);
    }

    public float[] getVertices()
    {
        return _vertices.ToArray();
    }

    public override MyMesh Build()
    {
        Console.WriteLine("mytriangle build");
        int sizeOfFloat = sizeof(float);
        int stride = 3 * sizeOfFloat; // 24 byte total per vertex

        float[] vertices = _vertices.ToArray();

        VboHandle = GL.GenBuffer();
        Console.WriteLine("vbo handle: " + VboHandle);
        GL.BindBuffer(BufferTarget.ArrayBuffer, VboHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
            BufferUsageHint.StaticDraw);

        VaoHandle = GL.GenVertexArray();
        Console.WriteLine("vao handle: " + VaoHandle);
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
    
    public override Matrix4 GetDynamicTransformMatrix()
    {
        // var transform = Matrix4.Identity;
        var transform = initialTransform;
        transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(RotationAngle));
        transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(RotationAngle));
        // transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(RotationAngle));
        return transform;
    }
}
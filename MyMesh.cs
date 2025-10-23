using OpenTK.Graphics.OpenGL;

namespace cs_gl;

public class MyMesh
{
    public int VaoHandle { get; private set; }
    public int VboHandle { get; private set; }
    private int VertexCount { get; set; }

    public MyMesh(float[] vertices)
    {
        VboHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VboHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        VaoHandle = GL.GenVertexArray();
        GL.BindVertexArray(VaoHandle);
        
        GL.VertexAttribPointer(0,3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        VertexCount = vertices.Length;
    }

    public void Draw()
    {
        GL.BindVertexArray(VaoHandle);
        GL.DrawArrays(PrimitiveType.Triangles, 0, VertexCount);
    }
}
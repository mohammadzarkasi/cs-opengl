using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

// ReSharper disable once CheckNamespace
namespace cs_gl.Src
{
    public abstract class MyMesh
    {
        public int VaoHandle { get;  set; }
        public int VboHandle { get; set; }
        protected int VertexCount { get; set; }

        protected bool hasColor = false;
        protected bool Enable;
        protected float RotationAngle = 0;
        // ReSharper disable once InconsistentNaming
        protected Matrix4 initialTransform;

        public MyMesh(float[] vertices, bool enable)
        {
            Enable = enable;
            if (enable == true)
            {
                // Total 6 float per vertex (3 Posisi + 3 Warna)
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

                /*GL.VertexAttribPointer(0,3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                GL.BindVertexArray(0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                VertexCount = vertices.Length;*/
            }
        }

        public virtual MyMesh InitialTransform(Matrix4 transform)
        {
            initialTransform = transform;
            return this;
        }

        public abstract void Draw(int colorSwitchLoc, int solidColorLoc, int transformLoc);
        // public virtual void Draw(int colorSwitchLoc, int solidColorLoc, int tranformLoc)
        // {
        //     if (Enable == false)
        //     {
        //         return;
        //     }
        //     /*if (hasColor == false)
        //     {
        //         GL.Uniform4(colorLocation, 1, 0, 0, 1);
        //     }*/
        //     GL.Uniform1(colorSwitchLoc, 0);
        //     GL.Uniform4(solidColorLoc, new Vector4(1f,1f,0f,1f));
        //
        //     GL.BindVertexArray(VaoHandle);
        //     GL.DrawArrays(PrimitiveType.Triangles, 0, VertexCount);
        // }
        public virtual void Forward()
        {
            RotationAngle += 1;
            if (RotationAngle >= 360)
            {
                RotationAngle = 0;
            }
        }
        // public abstract void Forward();

        //public abstract Matrix4 GetTransformMatrix();
        public virtual Matrix4 GetTransformMatrix()
        {
            return Matrix4.Identity;
        }

        public abstract MyMesh Build();

        // public virtual MyMesh Build()
        // {
        //     return this;
        // }
    }
    
    
}
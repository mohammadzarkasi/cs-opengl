using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

// ReSharper disable once CheckNamespace
namespace cs_gl.Src
{
    public class Main
    {
        private readonly GameWindow _game;
        private readonly List<MyMesh> _meshes;
        private readonly Shader _shader;
    
        private Matrix4 _projectionMatrix;

        public Main()
        {
            _meshes = new();
            _shader = new Shader("Shader/shader.vert", "Shader/shader.frag");
        
            _game = new GameWindow(new GameWindowSettings()
                {
                    UpdateFrequency = 10
                }, 
                new NativeWindowSettings()
                {
                    ClientSize = new Vector2i(1280, 720),
                    Title = "Open GL Csharp",
                    Flags = ContextFlags.ForwardCompatible
                });

            _game.Load += Loaded;
            _game.Resize += Resize;
            _game.RenderFrame += RenderFrame;
            _game.Unload += Unload;
        }

        private void Unload()
        {
            Console.WriteLine("unload...");
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        
            foreach (var mesh in _meshes)
            {
                GL.DeleteBuffer(mesh.VboHandle);
                GL.DeleteVertexArray(mesh.VaoHandle);
            }
        
            GL.DeleteProgram(_shader.Handle);
        }

        private void Resize(ResizeEventArgs e)
        {
            Console.WriteLine("resize");
            Console.WriteLine("view port: " + _game.Size.X + "," + _game.Size.Y);
        
            GL.Viewport(0,0,_game.Size.X, _game.Size.Y);

            _projectionMatrix = Matrix4.CreateOrthographicOffCenter(-100, 100, -100, 100, -1, 1);

            // GL.MatrixMode(MatrixMode.Projection);
            // GL.LoadIdentity();


            // GL.Ortho(-100,100,-100,100,-1,1);

            // GL.MatrixMode(MatrixMode.Modelview);
            // GL.LoadIdentity();
        }

        private void RenderFrame(FrameEventArgs e)
        {
            // Console.WriteLine("render frame");
            // GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit);
        
            _shader.Use();
            int projLocation = GL.GetUniformLocation(_shader.Handle, "projection_matrix");
            GL.UniformMatrix4(projLocation, false, ref _projectionMatrix);

            // int colorLocation = GL.GetUniformLocation(_shader.Handle, "uColor");
        
            // Dapatkan lokasi Uniforms (sebaiknya dilakukan sekali saat inisialisasi)
            int solidColorLoc = GL.GetUniformLocation(_shader.Handle, "uSolidColor");
            int colorSwitchLoc = GL.GetUniformLocation(_shader.Handle, "useUniformColor");
        
            foreach (var mesh in _meshes)
            {
                mesh.Draw(colorSwitchLoc, solidColorLoc,0);
            }

            /*GL.Begin(PrimitiveType.Triangles);

            GL.Color3(1f, 1f,0f);
            GL.Vertex2(1,1);

            GL.Color3(0f, 1f,1f);
            GL.Vertex2(49,1);

            GL.Color3(1f, 0f,1f);
            GL.Vertex2(49,49);
            GL.Vertex2(5,49);

            GL.End();*/
        
            _game.SwapBuffers();
        }
    
        private void Loaded() 
        {
            Console.WriteLine("game window loaded");
            GL.ClearColor(0.5f, 0.5f, 0.5f, 1f);
            //Resize(new ResizeEventArgs(_game.Size));
            //_vertexBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, );
        
            // _meshes.Add(new MyMesh([
            //     1f,1f,0f,       1.0f, 0.0f, 0.0f,
            //     49f,1f,0f,      0.0f, 1.0f, 0.0f,
            //     49f,49f,0f,     0.0f, 0.0f, 1.0f
            // ],true));
        
            _shader.Init();
            _shader.Use();
        }

        public void Mulai()
        {
            _game.Run();
        }
    }
}
using cs_gl.Src.Mesh;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace cs_gl.Src;

public class Main2
{
    private readonly GameWindow _game;
    private readonly List<MyMesh> _meshes;
    private readonly Shader _shader;

    private Matrix4 _projectionMatrix;
    private Matrix4 _viewMatrix;
    private Vector3 _cameraPosition = new Vector3(0f, 0f, 50f);
    private Vector3 _lookAt = new Vector3(0f, 0f, 0f);
    private Vector3 _upVector = new Vector3(0f, 1f, 0f);

    public Main2()
    {
        _meshes = new();
        _shader = new Shader("Shader/shader2.vert", "Shader/shader.frag");

        _game = new GameWindow(new GameWindowSettings()
            {
                UpdateFrequency = 30
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
        _game.UpdateFrame += UpdateFrame;
    }

    private void UpdateFrame(FrameEventArgs e)
    {
        foreach (var mesh in _meshes)
        {
            mesh.Forward();
        }
        
    }

    private void Unload()
    {
        Console.WriteLine("unload...");
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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

        GL.Viewport(0, 0, _game.Size.X, _game.Size.Y);

        // _projectionMatrix = Matrix4.CreateOrthographicOffCenter(-100, 100, -100, 100, -1, 1);

        _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f),
            (float)_game.Size.X / _game.Size.Y, 0.1f, 100f);
        _viewMatrix = Matrix4.LookAt(_cameraPosition, _lookAt, _upVector);
    }

    private void RenderFrame(FrameEventArgs e)
    {
        // Console.WriteLine("render frame");
        // GL.LoadIdentity();
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        _shader.Use();
        int projLocation = GL.GetUniformLocation(_shader.Handle, "projection_matrix");
        GL.UniformMatrix4(projLocation, false, ref _projectionMatrix);

        int viewLocation = GL.GetUniformLocation(_shader.Handle, "view_matrix");
        GL.UniformMatrix4(viewLocation, false, ref _viewMatrix);

        int transformLoc = GL.GetUniformLocation(_shader.Handle, "transform_matrix");


        // Dapatkan lokasi Uniforms (sebaiknya dilakukan sekali saat inisialisasi)
        int solidColorLoc = GL.GetUniformLocation(_shader.Handle, "uSolidColor");
        int colorSwitchLoc = GL.GetUniformLocation(_shader.Handle, "useUniformColor");


        foreach (var mesh in _meshes)
        {
            // var tranformMatrix = mesh.GetTransformMatrix();
            // GL.UniformMatrix4(transformLoc, false, ref tranformMatrix);
            mesh.Draw(colorSwitchLoc, solidColorLoc, transformLoc);
        }

        _game.SwapBuffers();
    }

    private void Loaded()
    {
        Console.WriteLine("game window loaded");
        GL.ClearColor(0.5f, 0.5f, 0.5f, 1f);
        GL.Enable(EnableCap.DepthTest);

        // _meshes.Add(new MyCube([
        //     1f, 1f, 0f,       1.0f, 0.0f, 0.0f,
        //     15f, 1f, 0f,      0.0f, 1.0f, 0.0f,
        //     15f, 15f, 0f,     0.0f, 0.0f, 1.0f,
        //     
        //     1f, 1f, 0f,       1.0f, 0.0f, 0.0f,
        //     1f, 15f, 0f,      0.0f, 1.0f, 0.0f,
        //     15f, 15f, 0f,     0.0f, 0.0f, 1.0f,
        // ]));
        // _meshes.Add(new MySquare(0,0,0, 20,10,0).Build());
        _meshes.Add(new MyCube2(0, 0, 0, 20, 10, 20).Build());
        // _meshes.Add(new MyCube([
        //     1f, 1f, 0f,       1.0f, 0.0f, 0.0f,
        //     1f, 15f, 0f,      0.0f, 1.0f, 0.0f,
        //     15f, 15f, 0f,     0.0f, 0.0f, 1.0f,
        // ]));
        // _meshes.Add(new MyMesh([
        //     1f, 1f, 0f,       1.0f, 0.0f, 0.0f,
        //     19f, 1f, 0f,      0.0f, 1.0f, 0.0f,
        //     19f, 19f, 0f,     0.0f, 0.0f, 1.0f
        // ], true));
        // _meshes.Add(new MyMesh([
        //     1f, 1f, 0f,       1.0f, 0.0f, 0.0f,
        //     19f, 1f, 0f,      0.0f, 1.0f, 0.0f,
        //     19f, 19f, 0f,     0.0f, 0.0f, 1.0f
        // ], true));

        _shader.Init();
        _shader.Use();
        
        Console.WriteLine("matrix identity: " + Matrix4.Identity*Matrix4.Identity);
    }

    public void Mulai()
    {
        _game.Run();
    }
}
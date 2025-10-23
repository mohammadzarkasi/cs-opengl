using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace cs_gl;

public class Main
{
    private readonly GameWindow _game;

    public Main()
    {
        _game = new GameWindow(new GameWindowSettings()
        {
            UpdateFrequency = 10
        }, 
            new NativeWindowSettings()
        {
            ClientSize = new Vector2i(1280, 720),
            Title = "Open GL Csharp",
        });

        _game.Load += Loaded;
        _game.Resize += Resize;
        _game.RenderFrame += RenderFrame;
        
    }

    private void Resize(ResizeEventArgs e)
    {
        Console.WriteLine("resize");
        GL.Viewport(0,0,_game.Size.X, _game.Size.Y);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        GL.Ortho(-100,100,-100,100,-1,1);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();
    }

    private void RenderFrame(FrameEventArgs e)
    {
        // Console.WriteLine("render frame");
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        GL.Begin(PrimitiveType.Quads);
        
        GL.Color3(0f, 1f,0f);

        GL.Vertex2(1,1);
        GL.Vertex2(49,1);
        GL.Vertex2(49,49);
        GL.Vertex2(5,49);
        
        GL.End();
        
        _game.SwapBuffers();
    }
    
    private void Loaded() 
    {
        Console.WriteLine("game window loaded");
        GL.ClearColor(0.5f, 0.5f, 0.5f, 1f);
    }

    public void Mulai()
    {
        _game.Run();
    }
}
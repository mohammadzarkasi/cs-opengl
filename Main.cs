using System.Drawing;
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
        _game = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings()
        {
            ClientSize = new Vector2i(1280, 720),
            Title = "Open GL Csharp",
        });

        _game.Load += Loaded;
        _game.RenderFrame += RenderFrame;
    }

    private void RenderFrame(FrameEventArgs e)
    {
        //Console.WriteLine("render frame: " + e.ToString());
        GL.Clear(ClearBufferMask.ColorBufferBit);
        _game.SwapBuffers();
    }
    
    private void Loaded() 
    {
        Console.WriteLine("game window loaded");
        GL.ClearColor(Color.GreenYellow);
    }

    public void Mulai()
    {
        _game.Run();
    }
}
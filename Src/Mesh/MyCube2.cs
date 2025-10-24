using OpenTK.Mathematics;

namespace cs_gl.Src.Mesh;

public class MyCube2 : MyMesh
{
    // private List<MySquare> _squares = [];
    private List<MySquare2> _squares = [];

    public MyCube2(int x, int y, int z, int xLength, int yLength, int zLength) : base([], false)
    {
        //belakang
        // _squares.Add(new MySquare(x,y,z, xLength, yLength, 0));
        var belakang = new MySquare2(0, 0, xLength, yLength);
        belakang.SetInitialTransform(Matrix4.CreateTranslation(new Vector3(x, y, 0)));
        belakang.SetSolidColor(new Vector4(1f,0f,0f,1f));
        _squares.Add(belakang);
        // _squares.Add(new MySquare2(x, z, xLength, zLength));
        // kiri
        var kiri = new MySquare2(0, 0, zLength, yLength);
        kiri.SetInitialTransform(Matrix4.CreateRotationY(MathHelper.DegreesToRadians(270)));
        kiri.SetSolidColor(new Vector4(0f,1f,0f,1f));
        // kiri.InitialTransform(Matrix4.CreateTranslation(new Vector3(50,10,10)));
        _squares.Add(kiri);
        // _squares.Add(new MySquare(x,y,z, 0, yLength, zLength));
        // bawah
        var bawah = new MySquare2(0, 0, xLength, zLength);
        bawah.SetInitialTransform(Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90)));
        bawah.SetSolidColor(new Vector4(0f,0f,1f,1f));
        _squares.Add(bawah);
        // _squares.Add(new MySquare(x,y,z, xLength, 0, zLength));
        //depan
        var depan = new MySquare2(0, 0, xLength, yLength);
        depan.SetInitialTransform(Matrix4.CreateTranslation(new Vector3(x, y, zLength)));
        depan.SetSolidColor(new Vector4(1f,0.4f,0f,1f));
        _squares.Add(depan);
        // _squares.Add(new MySquare(x,y,z+zLength, xLength, yLength, 0));
        //kanan
        //_squares.Add(new MySquare(x+xLength,y,z, 0, yLength, zLength));
        //atas
        // _squares.Add(new MySquare(x,y+yLength,z, xLength, y, zLength));
    }

    public override void Draw(int colorSwitchLoc, int solidColorLoc, int transformLoc)
    {
        foreach (var s in _squares)
        {
            s.Draw(colorSwitchLoc, solidColorLoc, transformLoc);
        }
    }

    public override void Forward()
    {
        foreach (var s in _squares)
        {
            s.Forward();
        }
    }

    public override MyMesh Build()
    {
        Console.WriteLine("mycube2 build");
        foreach (var s in _squares)
        {
            s.Build();
        }

        return this;
    }
}
using OpenTK.Mathematics;

namespace cs_gl.Src.Mesh;

public class MyCube2 : MyMesh
{
    // private List<MySquare> _squares = [];
    private readonly List<MySquare2> _squares = [];

    public MyCube2(int x, int y, int z, int xLength, int yLength, int zLength) : base([], false)
    {
        var baseTranslate = Matrix4.CreateTranslation(x, y, z);

        //belakang
        // _squares.Add(new MySquare(x,y,z, xLength, yLength, 0));
        var belakang = new MySquare2(0, 0, xLength, yLength);
        belakang.AddInitialTransform(baseTranslate);
        belakang.SetSolidColor(new Vector4(1f, 0f, 0f, 1f));
        _squares.Add(belakang);
        // _squares.Add(new MySquare2(x, z, xLength, zLength));

        // kiri
        var kiri = new MySquare2(0, 0, zLength, yLength);
        kiri.AddInitialTransform(
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(90))
            * baseTranslate
        );
        kiri.SetSolidColor(new Vector4(0f, 1f, 0f, 1f));
        _squares.Add(kiri);
        // _squares.Add(new MySquare(x,y,z, 0, yLength, zLength));

        // bawah
        var bawah = new MySquare2(0, 0, xLength, zLength);
        bawah.AddInitialTransform(
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(270))
            * baseTranslate
        );
        bawah.SetSolidColor(new Vector4(0f, 0f, 1f, 1f));
        _squares.Add(bawah);
        // _squares.Add(new MySquare(x,y,z, xLength, 0, zLength));

        //depan
        var depan = new MySquare2(0, 0, xLength, yLength);
        depan.AddInitialTransform(
            Matrix4.CreateTranslation(0, 0, zLength)
            * baseTranslate
        );
        depan.SetSolidColor(new Vector4(1f, 0.4f, 0f, 1f));
        _squares.Add(depan);

        // _squares.Add(new MySquare(x,y,z+zLength, xLength, yLength, 0));
        //kanan
        var kanan = new MySquare2(0, 0, zLength, yLength);
        kanan.AddInitialTransform(
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(270))
            * Matrix4.CreateTranslation(xLength, 0, 0)
            * baseTranslate
        );
        kanan.SetSolidColor(new Vector4(0f, 1f, 1f, 1f));
        _squares.Add(kanan);
        //_squares.Add(new MySquare(x+xLength,y,z, 0, yLength, zLength));

        //atas
        var atas = new MySquare2(0, 0, xLength, zLength);
        atas.AddInitialTransform(
            baseTranslate
            * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90))
            * Matrix4.CreateTranslation(0, yLength, 0)
        );
        atas.SetSolidColor(new Vector4(0f, 0f, 1f, 1f));
        _squares.Add(atas);
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
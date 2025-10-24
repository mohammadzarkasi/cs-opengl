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
        _squares.Add(new MySquare2(x, y, xLength, yLength));
        // _squares.Add(new MySquare2(x, z, xLength, zLength));
        // kiri
        // _squares.Add(new MySquare(x,y,z, 0, yLength, zLength));
        // bawah
        // _squares.Add(new MySquare(x,y,z, xLength, 0, zLength));
        //depan
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
        foreach (var s in _squares)
        {
            s.Build();
        }

        return this;
    }
}
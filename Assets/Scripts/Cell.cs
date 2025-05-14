using UnityEngine;

public class Cell
{
    public CellType Type { get; set; }
    public Vector3Int Position { get; private set; }
    public int Number { get; set; }

    public bool Revealed { get; set; }
    public bool Flagged { get; set; }
    public bool Exploded { get; set; }


    public Cell(int x, int y)
    {
        Position = new Vector3Int(x, y, 0);
        Type = CellType.Empty;
    }
    public Cell()
    {
        Type = CellType.Invalid;
    }
}

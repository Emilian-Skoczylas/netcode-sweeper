using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Board : MonoBehaviour
{
    private Tilemap _tilemap;

    [Header("Tiles")]
    [SerializeField] private Tile _tileUnknown;
    [SerializeField] private Tile _tileEmpty;
    [SerializeField] private Tile _tileMine;
    [SerializeField] private Tile _tileExploded;
    [SerializeField] private Tile _tileFlag;
    [SerializeField] private Tile[] _tileNumbers;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public void UpdateTiles(Cell[,] state)
    {
        if (_tilemap == null)
        {
            Debug.LogError("[Build] Tilemap is null!");
            return;
        }

        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell cell = state[i, j];

                _tilemap.SetTile(cell.Position, GetTile(cell));
            }
        }
    }

    private Tile GetTile(Cell cell)
    {
        if (cell == null)
        {
            Debug.LogError("[GetTile] cell is null!");
            return null;
        }

        if (cell.Revealed)
        {
            return GetRevealedTile(cell);
        }
        else if (cell.Flagged)
        {
            return _tileFlag;
        }
        else
        {
            return _tileUnknown;
        }
    }

    private Tile GetRevealedTile(Cell cell)
    {
        Tile result;
        switch (cell.Type)
        {
            case CellType.Empty:
                result = _tileEmpty;
                break;
            case CellType.Mine:
                result = _tileMine;
                break;

            case CellType.Number:
                result = GetNumberTile(cell.Number);
                break;

            default:
                Debug.LogError($"[GetRevealedTile] Unknown cell type: {cell.Type}");
                return _tileUnknown;
        }

        return result;
    }

    private Tile GetNumberTile(int number)
    {
        int index = number - 1;

        if (index >= 0 && index < _tileNumbers.Length)
            return _tileNumbers[index];
        else
        {
            Debug.LogError($"[GetRevealedTile] Invalid number index: {number}");
            return _tileUnknown;
        }
    }
}

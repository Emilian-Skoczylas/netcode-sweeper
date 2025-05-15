using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic
{
    private Cell[,] _state;
    private Board _board;
    private int _width;
    private int _height;

    public event Action OnGameOver;
    public event Action<bool> OnFlagPlaced;

    public GameLogic(Cell[,] state, Board board, int width, int height)
    {
        _state = state;
        _board = board;
        _width = width;
        _height = height;
    }

    public void FlagTile()
    {
        Cell cell = GetCellFromWorldPoint();

        if (cell.Type == CellType.Invalid || cell.IsRevealed)
            return;

        cell.IsFlagged = !cell.IsFlagged;
        _state[cell.Position.x, cell.Position.y] = cell;
        _board.UpdateTiles(_state);

        OnFlagPlaced?.Invoke(cell.IsFlagged);
    }

    public void Reveal()
    {
        Cell cell = GetCellFromWorldPoint();

        if (cell.Type == CellType.Invalid || cell.IsRevealed || cell.IsFlagged)
            return;

        switch (cell.Type)
        {
            case CellType.Mine:
                Explode(cell);
                break;
            case CellType.Empty:
                Flood(cell);
                break;
            default:
                cell.IsRevealed = true;
                _state[cell.Position.x, cell.Position.y] = cell;
                break;
        }

        _board.UpdateTiles(_state);
    }

    private void Explode(Cell cell)
    {
        cell.IsRevealed = true;
        cell.IsExploded = true;
        _state[cell.Position.x, cell.Position.y] = cell;

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                cell = _state[i, j];

                if (cell.Type == CellType.Mine)
                {
                    cell.IsRevealed = true;
                    _state[i, j] = cell;
                }
            }
        }

        OnGameOver?.Invoke();
    }
    private void Flood(Cell start)
    {
        if (start == null)
            return;

        var stack = new Stack<Cell>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var cell = stack.Pop();

            if (cell == null || cell.IsRevealed || cell.Type == CellType.Mine || cell.Type == CellType.Invalid)
                continue;

            cell.IsRevealed = true;
            _state[cell.Position.x, cell.Position.y] = cell;

            if (cell.Type == CellType.Empty)
            {
                foreach (var offset in GameplayHelper.NeighborOffsets)
                {
                    stack.Push(GetCell(cell.Position.x + offset.x,
                                      cell.Position.y + offset.y));
                }
            }
        }
    }

    private Cell GetCellFromWorldPoint()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = _board.GetTilemap().WorldToCell(worldPos);
        Cell cell = GetCell(cellPos.x, cellPos.y);

        return cell;
    }

    private Cell GetCell(int x, int y)
    {
        if (GameplayHelper.IsCoordinateValid(_width, _height, x, y))
        {
            return _state[x, y];
        }
        else
        {
            return new Cell(); // INVALID, out of bounds
        }
    }
}

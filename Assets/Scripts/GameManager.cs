using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Board _board;
    
    [SerializeField] private int _boardWidth = 16;
    [SerializeField] private int _boardHeight = 16;
    [SerializeField] private int _minesCount = 8;
    private Cell[,] _state;

    private void Awake()
    {
        if (_board == null)
            Debug.LogError("[Awake] board is missing!");
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _state = new Cell[_boardWidth, _boardHeight];

        GenerateCells();
        GenerateMines();
        GenerateNumbers();

        Camera.main.transform.position = new Vector3(_boardWidth / 2f, _boardHeight / 2, -10f);
        if (_board != null)
            _board.UpdateTiles(_state);
    }

    private void GenerateCells()
    {
        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardHeight; j++)
            {
                Cell cell = new Cell(i, j);
                _state[i, j] = cell;
            }
        }
    }
    private void GenerateMines()
    {
        int totalTiles = _boardWidth * _boardHeight;

        if (_minesCount > totalTiles)
            _minesCount = totalTiles;

        int minesPlaced = 0;
        System.Random rand = new System.Random();

        while (minesPlaced < _minesCount)
        {         
            int x = rand.Next(_boardWidth);
            int y = rand.Next(_boardHeight);

            if (_state[x, y].Type != CellType.Mine)
            {
                _state[x, y].Type = CellType.Mine;
                minesPlaced++;
            }
        }
    }

    private void GenerateNumbers()
    {
        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardHeight; j++)
            {
                Cell cell = _state[i, j];

                if (cell.Type == CellType.Mine)
                    continue;

                cell.Number = GameplayHelper.CountAdjacentMines(_state, cell, _boardWidth, _boardHeight);

                if (cell.Number > 0)
                    cell.Type = CellType.Number;

                _state[i, j] = cell;
            }
        }
    }
}

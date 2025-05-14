using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board _board;
    
    [SerializeField] private int _boardWidth = 16;
    [SerializeField] private int _boardHeight = 16;
    [SerializeField] private int _minesCount = 8;
    private Cell[,] _state;

    private BoardGenerator _boardGenerator;
    private GameLogic _gameLogic;

    private bool _isGameOver;

    private void Awake()
    {
        if (_board == null)
            Debug.LogError("[Awake] board is missing!");
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (_isGameOver)
            return;

        HandleInput();
    }

    private void StartGame()
    {
        _isGameOver = false;

        _state = new Cell[_boardWidth, _boardHeight];
        _boardGenerator = new BoardGenerator(_state, _boardWidth, _boardHeight, _minesCount);
        _gameLogic = new GameLogic(_state, _board, _boardWidth, _boardHeight);
        _gameLogic.OnGameOver += OnGameOverEvent;

        _boardGenerator.GenerateCells();
        _boardGenerator.GenerateMines();
        _boardGenerator.GenerateNumbers();

        Camera.main.transform.position = new Vector3(_boardWidth / 2f, _boardHeight / 2, -10f);
        if (_board != null)
            _board.UpdateTiles(_state);
    }

    private void OnGameOverEvent()
    {
        _isGameOver = true;
        Debug.Log("GAME OVER");
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_gameLogic != null)
            {
                _gameLogic.FlagTile();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_gameLogic != null)
            {
                _gameLogic.Reveal();
                CheckWinCondition();
            }
        }
    }

    private void CheckWinCondition()
    {
        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardHeight; j++)
            {
                Cell cell = _state[i, j];

                if (cell.Type != CellType.Mine && !cell.Revealed)
                    return;
            }
        }

        // win
        _isGameOver = true;
        Debug.Log("GAME WIN");
    }
}

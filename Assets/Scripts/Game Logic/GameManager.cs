using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private Board _board;
    [SerializeField] private int _boardWidth = 16;
    [SerializeField] private int _boardHeight = 16;
    [SerializeField] private int _minesCount = 8;
    [SerializeField] private GameDifficultySO _gameDifficultySO;

    [Header("UI")]
    [SerializeField] private GameObject _canvasOverlay;
    [SerializeField] private GameObject _canvasMenu;
    [SerializeField] private TMP_Text _minesLeft;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _youWonObj;

    private Cell[,] _state;

    private BoardGenerator _boardGenerator;
    private GameLogic _gameLogic;

    private bool _gameStarted;
    private bool _isGameOver;
    private int _flagPlaced;
    private float _timer;
    private void Awake()
    {
        if (_board == null)
            Debug.LogError("[Awake] board is missing!");
    }

    public void InitGame(GameDifficultySO difficultySO)
    {
        _gameDifficultySO = difficultySO;
        _canvasOverlay.SetActive(true);
        StartCoroutine(DelayedStartGame());
    }

    private IEnumerator DelayedStartGame()
    {
        yield return null;
        StartGame();
    }

    private void StartGame()
    {
        _isGameOver = false;

        _boardWidth = _gameDifficultySO.boardWidth;
        _boardHeight = _gameDifficultySO.boardHeight;
        _minesCount = _gameDifficultySO.minesAmount;

        _state = new Cell[_boardWidth, _boardHeight];
        _boardGenerator = new BoardGenerator(_state, _boardWidth, _boardHeight, _minesCount);
        _gameLogic = new GameLogic(_state, _boardGenerator, _board, _boardWidth, _boardHeight);
        _gameLogic.OnGameOver += OnGameOverEvent;
        _gameLogic.OnFlagPlaced += OnFlagPlaced;

        _boardGenerator.GenerateCells();

        _youWonObj.SetActive(false);
        _board.gameObject.SetActive(true);

        Camera.main.transform.position = new Vector3(_boardWidth / 2f, _boardHeight / 2, -10f);
        if (_board != null)
        {
            _board.Reset();
            _board.UpdateTiles(_state);
        }

        _minesLeft.text = _minesCount.ToString();
        _timer = 0f;
        _flagPlaced = 0;
        _gameStarted = true;
    }

    private void Update()
    {
        if (_isGameOver)
            return;

        if (!_gameStarted)
            return;

        HandleInput();
        HandleTimer();
    }

    private void HandleTimer()
    {
        _timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(_timer / 60f);
        int seconds = Mathf.FloorToInt(_timer % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    private void OnFlagPlaced(bool isPlaced)
    {
        if (isPlaced)
            _flagPlaced++;
        else
            _flagPlaced--;

        int minesLeft = _minesCount - _flagPlaced;
        _minesLeft.text = minesLeft.ToString();
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
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            StartGame();
        }
    }

    private void CheckWinCondition()
    {
        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardHeight; j++)
            {
                Cell cell = _state[i, j];

                if (cell.Type != CellType.Mine && !cell.IsRevealed)
                    return;
            }
        }

        _isGameOver = true;
        _youWonObj.SetActive(true);
        Debug.Log("GAME WIN");
    }

    public void BackToMenu()
    {
        _isGameOver = true;
        _minesLeft.text = string.Empty;
        _timerText.text = string.Empty;

        _canvasOverlay.SetActive(false);
        _board.gameObject.SetActive(false);
        _canvasMenu.SetActive(true);
    }
    public void RestartGame()
    {
        StartGame();
    }
}

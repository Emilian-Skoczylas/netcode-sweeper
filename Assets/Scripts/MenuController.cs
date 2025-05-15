using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [Header("Difficulty")]
    [SerializeField] private GameDifficultySO[] _difficulties;

    [Header("Buttons")]
    [SerializeField] private Button[] _playButtons;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        if (_exitButton != null)
        {
            _exitButton.onClick.AddListener(() => Application.Quit());
        }

        if (_playButtons != null && _playButtons.Length > 0)
        {
            for (int i = 0; i < _playButtons.Length; i++)
            {
                int index = i;
                _playButtons[index].onClick.AddListener(() => OnClickedPlay(_difficulties[index]));
            }
        }
    }

    private void OnClickedPlay(GameDifficultySO difficultySO)
    {
        if (_gameManager != null)
        {
            _gameManager.InitGame(difficultySO);
            this.gameObject.SetActive(false);
        }
    }
}

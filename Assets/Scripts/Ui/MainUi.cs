using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUi : MonoBehaviour
{
    [SerializeField] private VoidEvent OnStartGame;
    [SerializeField] private TextMeshProUGUI _gamesAvailableText;

    [SerializeField] private Image _shiningGlow;
    [SerializeField] private Image _paleGlow;
    [SerializeField] private Button _startButton;
    
    private GameManager _gameManager => GameManager.Instance;

    private void Start()
    {
       UpdateUI();
    }

    public void StartGame()
    {
        OnStartGame.Raise();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _gamesAvailableText.text = _gameManager.GamesAvailable.ToString();

        if (_gameManager.GamesAvailable > 0)
        {
            _shiningGlow.gameObject.SetActive(true);
            _startButton.gameObject.SetActive(true);
            _paleGlow.gameObject.SetActive(false);
        }
        else
        {
            _shiningGlow.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(false);
            _paleGlow.gameObject.SetActive(true);
        }
    }
}

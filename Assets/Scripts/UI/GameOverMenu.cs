using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI winnerLabel;
    [SerializeField]
    private TextMeshProUGUI winnerText;
    [SerializeField]
    private Button menuButton;

    private const string WINNER_LABEL = "WINNER:";
    private const string DRAW_LABEL = "DRAW";

    private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuButtonClick);
        SubscribeToEvents();
    }

    private void OnMenuButtonClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.GameOver += OnGameOver;
    }

    private void OnGameOver(PlayerType endGamePlayerType)
    {
        switch (endGamePlayerType)
        {
            case PlayerType.X:
            case PlayerType.O:
                winnerLabel.text = WINNER_LABEL;
                winnerText.text = TicTacToeManager.Instance.CurrentPlayer.ToString();
                break;
            case PlayerType.None:
                winnerLabel.text = DRAW_LABEL;
                winnerText.text = string.Empty;
                break;
            default:
                break;
        }
    }
}

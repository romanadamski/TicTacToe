using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI winnerText;
    [SerializeField]
    private Button menuButton;

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

    private void OnGameOver(NodeType type)
    {
        switch (type)
        {
            case NodeType.X:
                winnerText.text = "Player 1 wins";
				break;
			case NodeType.O:
                winnerText.text = "Player 2 wins";
				break;
            case NodeType.None:
                winnerText.text = "Draw";
                break;
            default:
                break;
        }
    }
}

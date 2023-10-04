using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI winnerText;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private GameplayEventsSO gameplayEventsSO;

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
        gameplayEventsSO.OnGameOver += OnGameOver;
    }

    private void OnGameOver(IPlayer player)
    {
        if(player == null)
        {
            winnerText.text = "Draw";
        }
        else
        {
            switch (player.PlayerNumber)
            {
                case PlayerNumber.PlayerOne:
                    winnerText.text = "Player 1 wins";
                    break;
                case PlayerNumber.PlayerTwo:
                    winnerText.text = "Player 2 wins";
                    break;
            }
        }
    }
}

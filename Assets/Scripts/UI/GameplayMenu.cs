using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI playerOneNameText;
    [SerializeField]
    private Image playerOneActiveImage;
    [SerializeField]
    private TextMeshProUGUI playerTwoNameText;
    [SerializeField]
    private Image playerTwoActiveImage;
    [SerializeField]
    private Button menuButton;

    private Color _activePlayerColor = Color.white;
    private Color _inactivePlayerColor = new Color(0, 0, 0, 0.5f);

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
        EventsManager.Instance.PlayerChanged += PlayerChanged;
    }

    private void PlayerChanged(PlayerType playerType)
    {
        if (playerType == PlayerType.X)
        {
            playerOneActiveImage.color = _activePlayerColor;
            playerTwoActiveImage.color = _inactivePlayerColor;
        }
        else if (playerType == PlayerType.O)
        {
            playerOneActiveImage.color = _inactivePlayerColor;
            playerTwoActiveImage.color = _activePlayerColor;
        }
    }
}

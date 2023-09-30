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
    [SerializeField]
    private Button restartButon;
    [SerializeField]
    private GameSettingsSO settings;

    private Color _activePlayerColor = Color.white;
    private Color _inactivePlayerColor = new Color(0, 0, 0, 0.5f);

    private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuButtonClick);
		restartButon.onClick.AddListener(OnRestartButtonClick);
        SubscribeToEvents();
    }

	public override void Show()
	{
		base.Show();

		playerOneActiveImage.sprite = settings.PlayerOne;
		playerTwoActiveImage.sprite = settings.PlayerTwo;
	}

	private void OnMenuButtonClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void OnRestartButtonClick()
    {
        GameplayManager.Instance.RestartGameplay();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerChanged += PlayerChanged;
    }

    private void PlayerChanged(IPlayer player)
    {
        if (player.Type == NodeType.X)
        {
            playerOneActiveImage.color = _activePlayerColor;
            playerTwoActiveImage.color = _inactivePlayerColor;
        }
        else if (player.Type == NodeType.O)
        {
            playerOneActiveImage.color = _inactivePlayerColor;
            playerTwoActiveImage.color = _activePlayerColor;
        }
    }
}

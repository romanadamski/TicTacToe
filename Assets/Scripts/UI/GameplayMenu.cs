using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI playerOneText;
    [SerializeField]
    private Image playerOneActiveImage;
    [SerializeField]
    private TextMeshProUGUI playerTwoText;
    [SerializeField]
    private Image playerTwoActiveImage;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Button restartButon;
    [SerializeField]
    private Button undoButon;
    [SerializeField]
    private Button hintButton;
    [SerializeField]
    private TextMeshProUGUI timer;

    private Color _activePlayerColor = Color.white;
    private Color _inactivePlayerColor = new Color(0, 0, 0, 0.5f);

	[Inject]
	private TurnController _turnController;
    
	private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuButtonClick);
		restartButon.onClick.AddListener(OnRestartButtonClick);
		undoButon.onClick.AddListener(OnUndoButtonClick);
		hintButton.onClick.AddListener(OnHintButtonClick);
        SubscribeToEvents();
    }

	public override void Show()
    {
        base.Show();
        
        RefreshPlayerSprites();

        playerOneText.text = _turnController.PlayerOne.Name;
        playerTwoText.text = _turnController.PlayerTwo.Name;

        undoButon.gameObject.SetActive(_turnController.AnyComputerPlay);
        hintButton.gameObject.SetActive(_turnController.AnyComputerPlay);
    }

    private void RefreshPlayerSprites()
    {
        playerOneActiveImage.sprite = UIManager.Instance.GetPlayerSpriteByNodeType(_turnController.PlayerOne.NodeType);
        playerTwoActiveImage.sprite = UIManager.Instance.GetPlayerSpriteByNodeType(_turnController.PlayerTwo.NodeType);
    }

    private void OnMenuButtonClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void OnRestartButtonClick()
    {
        GameplayManager.Instance.RestartGameplay();
        RefreshPlayerSprites();
    }

    private void OnUndoButtonClick()
    {
        _turnController.UndoMove();
	}

    private void OnHintButtonClick()
    {
		var node = _turnController.GetNodeToHint();
		EventsManager.Instance.OnHint(node.index);

    }
    private void OnTimerChanged(float time)
    {
        timer.text = Mathf.Ceil(time).ToString("##");
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerChanged += PlayerChanged;
        EventsManager.Instance.TimerChanged += OnTimerChanged;
    }

    private void PlayerChanged(IPlayer player)
    {
        switch (player.PlayerNumber)
        {
            case PlayerNumber.PlayerOne:
                playerOneActiveImage.color = _activePlayerColor;
                playerTwoActiveImage.color = _inactivePlayerColor;
                break;
            case PlayerNumber.PlayerTwo:
                playerOneActiveImage.color = _inactivePlayerColor;
                playerTwoActiveImage.color = _activePlayerColor;
                break;
        }

        hintButton.interactable = player.AllowInput;
    }
}

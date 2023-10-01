using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
    private Button undoButon;
    [SerializeField]
    private Button hintButton;

    private Color _activePlayerColor = Color.white;
    private Color _inactivePlayerColor = new Color(0, 0, 0, 0.5f);

	[Inject]
	private TurnManager _turnManager;

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

		playerOneActiveImage.sprite = UIManager.Instance.PlayerOne;
		playerTwoActiveImage.sprite = UIManager.Instance.PlayerTwo;

		undoButon.gameObject.SetActive(_turnManager.AnyComputerPlay);
		hintButton.gameObject.SetActive(_turnManager.AnyComputerPlay);
	}

	private void OnMenuButtonClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void OnRestartButtonClick()
    {
        GameplayManager.Instance.RestartGameplay();
    }

    private void OnUndoButtonClick()
    {
        _turnManager.UndoMove();
	}

    private void OnHintButtonClick()
    {
		var node = _turnManager.GetNodeToHint();
		EventsManager.Instance.OnHint(node.index);
		
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

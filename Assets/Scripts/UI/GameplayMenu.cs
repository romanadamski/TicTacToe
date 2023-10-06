using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayMenu : BaseMenu
{
    [Header("HUD")]
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
    [SerializeField]
    private TextMeshProUGUI tilesToWin;

    [Header("Scriptable objects")]
    [SerializeField]
    private SettingsSO settingsSO;
    [SerializeField]
    private TurnEventsSO turnEventsSO;
    [SerializeField]
    private BoardEventsSO boardEventsSO;
    [SerializeField]
    private TurnStateSO turnStateSO;

    [Inject]
    private ITurnController _turnController;

    private Color _activePlayerColor = Color.white;
    private Color _inactivePlayerColor = new Color(0, 0, 0, 0.5f);

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

        playerOneText.text = turnStateSO.PlayerOne.Name;
        playerTwoText.text = turnStateSO.PlayerTwo.Name;

        undoButon.gameObject.SetActive(_turnController.AnyComputerPlay);
        hintButton.gameObject.SetActive(_turnController.AnyComputerPlay);

        tilesToWin.text = settingsSO.WinningNodes.ToString();
    }

    private void RefreshPlayerSprites()
    {
        playerOneActiveImage.sprite = UIManager.Instance.GetPlayerSpriteByNodeType(turnStateSO.PlayerOne.NodeType);
        playerTwoActiveImage.sprite = UIManager.Instance.GetPlayerSpriteByNodeType(turnStateSO.PlayerTwo.NodeType);
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
        boardEventsSO.UndoMove();
	}

    private void OnHintButtonClick()
    {
        boardEventsSO.Hint();
    }
    private void OnTimerChanged(float time)
    {
        timer.text = Mathf.Ceil(time).ToString("##");
    }

    private void SubscribeToEvents()
    {
        turnEventsSO.OnPlayerChanged += PlayerChanged;
        turnEventsSO.OnTimerChanged += OnTimerChanged;
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

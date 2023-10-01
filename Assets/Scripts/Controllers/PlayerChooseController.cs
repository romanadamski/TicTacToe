using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum PlayerType
{
	PlayerOne,
	PlayerTwo
}

[RequireComponent(typeof(Button))]
public abstract class PlayerChooseController : MonoBehaviour
{
	[SerializeField]
	private PlayerType playerType;
	[SerializeField]
	protected NodeType nodeType;

	[Inject]
	protected TurnManager _turnManager;
	private Button _button;

	protected abstract IPlayer Player { get; }

	private void Awake()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnButtonClick);
	}

	private void OnButtonClick()
	{
		switch (playerType)
		{
			case PlayerType.PlayerOne:
				_turnManager.PlayerOne = Player;
				break;
			case PlayerType.PlayerTwo:
				_turnManager.PlayerTwo = Player;
				break;
			default:
				break;
		}
	}
}

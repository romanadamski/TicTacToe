using System;
using UnityEngine;
using UnityEngine.UI;
public 
enum PlayerType
{
	PlayerOne,
	PlayerTwo
}

[RequireComponent(typeof(Button))]
public abstract class PlayerChooseController<T> : MonoBehaviour where T : IPlayer
{
	[SerializeField]
	protected PlayerType playerType;
	[SerializeField]
	protected NodeType nodeType;

	private TurnManager _turnManager => TurnManager.Instance;
	private Button _button;

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
				_turnManager.PlayerOne = (T)Activator.CreateInstance(typeof(T), nodeType, _turnManager);
				break;
			case PlayerType.PlayerTwo:
				_turnManager.PlayerTwo = (T)Activator.CreateInstance(typeof(T), nodeType, _turnManager);
				break;
			default:
				break;
		}
	}
}

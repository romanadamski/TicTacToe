using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TileController : MonoBehaviour
{
    private Image _image;
    private Sprite _defaultSprite;

    public Button Button { get; private set; }
    public Vector2 Index { get; private set; }
    public PlayerType PlayerType { get; private set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _defaultSprite = _image.sprite;
    }

    public void Init(Vector2 index, Action onButtonClick)
    {
        Index = index;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => onButtonClick?.Invoke());
        Button.interactable = true;
        _image.sprite = _defaultSprite;
        PlayerType = PlayerType.None;
    }

    public void SetTileState(PlayerType playerType)
    {
		switch (playerType)
		{
			case PlayerType.X:
				_image.sprite = GameSettingsManager.Instance.Settings.PlayerOne;
				break;
			case PlayerType.O:
				_image.sprite = GameSettingsManager.Instance.Settings.PlayerTwo;
				break;
		}

		PlayerType = playerType;
        Button.interactable = false;
    }

	public override string ToString()
	{
		return PlayerType + ", " + Index;
	}
}

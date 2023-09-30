using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TileController : MonoBehaviour
{
	[SerializeField]
	private GameSettingsScriptableObject settings;

	private Image _image;
    private Sprite _defaultSprite;

    public Button Button { get; private set; }
    public Vector2Int Index { get; private set; }
    public NodeType PlayerType { get; private set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _defaultSprite = _image.sprite;
    }

    public void Init(Vector2Int index, Action onButtonClick)
    {
        Index = index;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => onButtonClick?.Invoke());
        Button.interactable = true;
        _image.sprite = _defaultSprite;
        PlayerType = NodeType.None;
    }

    public void SetTileState(NodeType playerType)
    {
		switch (playerType)
		{
			case NodeType.X:
				_image.sprite = settings.PlayerOne;
				break;
			case NodeType.O:
				_image.sprite = settings.PlayerTwo;
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

using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TileController : MonoBehaviour
{
	private Image _image;
    private Sprite _defaultSprite;
    private UIManager _uiManager => UIManager.Instance;

    public Button Button { get; private set; }
    public Vector2Int Index { get; private set; }
    public NodeType NodeType { get; private set; }

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
        NodeType = NodeType.None;
    }

    public void SetTileState(NodeType nodeType)
    {
		switch (nodeType)
		{
			case NodeType.X:
				_image.sprite = _uiManager.PlayerOne;
				break;
			case NodeType.O:
				_image.sprite = _uiManager.PlayerTwo;
				break;
			case NodeType.None:
				_image.sprite = _defaultSprite;
				break;
		}

		NodeType = nodeType;
        Button.interactable = false;
    }

	public override string ToString()
	{
		return NodeType + ", " + Index;
	}
}

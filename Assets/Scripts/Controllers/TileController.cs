using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class TileController : MonoBehaviour
{
	[SerializeField]
	private Image hintImage;

	private Image _image;
    private Sprite _defaultSprite;
	private Coroutine _hintCoroutine;
	private Color transparentColor = new Color(0, 0, 0, 0);
	private Color targetHintColor = new Color(1, 1, 1, 1);

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

	/// <summary>
	/// Set tile node type and change sprite
	/// </summary>
	/// <param name="nodeType"></param>
    public void SetState(NodeType nodeType)
    {
		EndHighlightCoroutine();
		switch (nodeType)
		{
			case NodeType.X:
				_image.sprite = UIManager.Instance.PlayerOne;
				break;
			case NodeType.O:
				_image.sprite = UIManager.Instance.PlayerTwo;
				break;
			case NodeType.None:
				_image.sprite = _defaultSprite;
				break;
		}

		NodeType = nodeType;
        Button.interactable = false;
    }

	/// <summary>
	/// Quick fade-in and fade-out animation by given node type 
	/// </summary>
	/// <param name="nodeType"></param>
	public void Highlight(NodeType nodeType)
	{
		EndHighlightCoroutine();
		hintImage.sprite = UIManager.Instance.GetPlayerSpriteByNodeType(nodeType);
		_hintCoroutine = StartCoroutine(DoHighlight());
	}
	
	private IEnumerator DoHighlight()
	{
		var duration = 0.5f;
		var elapsed = 0.0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			hintImage.color = Color.Lerp(hintImage.color, targetHintColor, Time.fixedDeltaTime);

			yield return null;
		}
		elapsed = 0.0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			hintImage.color = Color.Lerp(hintImage.color, transparentColor, elapsed / duration);

			yield return null;
		}
	}

	/// <summary>
	/// Stop playing fade animaiton
	/// </summary>
	public void EndHighlightCoroutine()
	{
		hintImage.color = transparentColor;

		if(_hintCoroutine != null)
		{
			StopCoroutine(_hintCoroutine);
		}
		_hintCoroutine = null;

	}

	public override string ToString()
	{
		return NodeType + ", " + Index;
	}
}

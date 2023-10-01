using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TileController : MonoBehaviour
{
	[SerializeField]
	private Image hintImage;

	private Image _image;
    private Sprite _defaultSprite;
	private Coroutine _hintCoroutine;
	private Color transparentColor = new Color(0,0,0,0);

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
		EndHintCoroutine();
        Index = index;
		Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => onButtonClick?.Invoke());
        Button.interactable = true;
        _image.sprite = _defaultSprite;
        NodeType = NodeType.None;
    }

    public void SetState(NodeType nodeType)
    {
		EndHintCoroutine();
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

	public void Highlight()
	{
		EndHintCoroutine();
		StartCoroutine(DoHighlight());
	}
	
	private IEnumerator DoHighlight()
	{
		var duration = 0.5f;
		var elapsed = 0.0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			hintImage.color = Color.Lerp(hintImage.color, Color.white, elapsed / duration);

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

	private void EndHintCoroutine()
	{
		if(_hintCoroutine != null)
		{
			StopCoroutine(_hintCoroutine);
		}
		_hintCoroutine = null;

		hintImage.color = transparentColor;
	}

	public override string ToString()
	{
		return NodeType + ", " + Index;
	}
}

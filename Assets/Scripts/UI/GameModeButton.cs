using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameModeButton : MonoBehaviour
{
	[SerializeField]
	private Sprite selectedButtonSprite;

	private Button _button;
	private Sprite _normalButtonSprite;

	private void Awake()
	{
		_button = GetComponent<Button>();
		_normalButtonSprite = _button.image.sprite;
	}

	public void SelectButton()
	{
		_button.image.sprite = selectedButtonSprite;
	}

	public void DeselectButton()
	{
		_button.image.sprite = _normalButtonSprite;
	}
}

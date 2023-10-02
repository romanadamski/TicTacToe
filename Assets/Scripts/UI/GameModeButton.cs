using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class GameModeButton : MonoBehaviour
{
	[SerializeField]
	private Color selectedButtonColor;
	private Color _normalButtonColor;

	public Button Button { get; private set; }
	public bool Active { get; private set; }

	private void Awake()
	{
		Button = GetComponent<Button>();
		_normalButtonColor = Button.image.color;
	}

	public void SelectButton()
	{
		Button.image.color = selectedButtonColor;
		Active = true;
	}

	public void DeselectButton()
	{
		Button.image.color = _normalButtonColor;
		Active = false;
	}
}

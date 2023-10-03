using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class GameModeChoiceButton : MonoBehaviour
{
	[SerializeField]
	private ActionChoice actionChoice;
	public ActionChoice ActionChoice => actionChoice;

	[SerializeField]
	private Color selectedButtonColor;

	private Color _normalButtonColor;

	public Button Button;
	public bool Selected { get; private set; }

	private void Awake()
	{
		_normalButtonColor = Button.image.color;
	}

	public void Select()
	{
		Button.image.color = selectedButtonColor;
		Selected = true;
	}

	public void Deselect()
	{
		Button.image.color = _normalButtonColor;
		Selected = false;
	}
}

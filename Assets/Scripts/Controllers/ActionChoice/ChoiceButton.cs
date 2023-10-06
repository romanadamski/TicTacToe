using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Holds reference to <typeparamref name="ActionChoice"/> with Execute method
/// </summary>
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ActionChoice))]
public class ChoiceButton : MonoBehaviour
{
	[SerializeField]
	private Color selectedButtonColor;
	[SerializeField]
	private Button button;

	/// <summary>
	/// Reference to action that can be called if this button is selected. Note: selecting button do not call action.
	/// </summary>
	public ActionChoice ActionChoice { get; private set; }

	/// <summary>
	/// Is this button selected?
	/// </summary>
	public bool Selected { get; private set; }

	private Color _normalButtonColor;

	private void Awake()
	{
		ActionChoice = GetComponent<ActionChoice>();
		_normalButtonColor = button.image.color;
	}

	public void Select()
	{
		button.image.color = selectedButtonColor;
		Selected = true;
	}

	public void Deselect()
	{
		button.image.color = _normalButtonColor;
		Selected = false;
	}

	/// <summary>
	/// Add listener to button
	/// </summary>
	/// <param name="call">Action to call on button press</param>
	public void AddListener(UnityAction call)
    {
		button.onClick.AddListener(call);
    }
}

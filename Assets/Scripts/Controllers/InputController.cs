using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
	private Vector3 _onMouseDownPosition;
	public event Action<Vector3> OnMouseUpEvent;

	private void OnMouseDown()
	{
		_onMouseDownPosition = Input.mousePosition;
	}

	private void OnMouseUp()
	{
		if (Input.mousePosition != _onMouseDownPosition) return;

		OnMouseUpEvent?.Invoke(_onMouseDownPosition);
	}
}

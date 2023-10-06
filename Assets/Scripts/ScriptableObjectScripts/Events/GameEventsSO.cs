using System;
using UnityEngine;

/// <summary>
/// Holds reference to game related events
/// </summary>
[CreateAssetMenu(fileName = "GameEvents", menuName = "ScriptableObjects/GameEvents")]
public class GameEventsSO : ScriptableObject
{
	public event Action OnGoToMainMenu;
	public event Action OnStartLevel;
	public event Action OnGoToSettings;

	public void GoToMainMenu()
	{
		OnGoToMainMenu?.Invoke();
	}

	public void StartLevel()
	{
		OnStartLevel?.Invoke();
	}

	public void GoToSettings()
	{
		OnGoToSettings?.Invoke();
	}
}

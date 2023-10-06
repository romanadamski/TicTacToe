using System;
using UnityEngine;

/// <summary>
/// Holds reference to turn related events
/// </summary>
[CreateAssetMenu(fileName = "TurnEvents", menuName = "ScriptableObjects/TurnEvents")]
public class TurnEventsSO : ScriptableObject
{
	public event Action<IPlayer> OnPlayerChanged;
	public event Action<float> OnTimerChanged;

	public void TimerChanged(float time)
    {
		OnTimerChanged?.Invoke(time);
	}

	public void PlayerChanged(IPlayer player)
    {
		OnPlayerChanged?.Invoke(player);
	}
}

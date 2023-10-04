using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnEvents", menuName = "ScriptableObjects/TurnEvents")]
public class TurnEventsSO : ScriptableObject
{
	[SerializeField]
	private TurnStateSO turnStateSO;

	public event Action<IPlayer> OnPlayerChanged;
	public event Action<float> OnTimerChanged;
	public event Action<IPlayer> OnAddPlayer;

	public void TimerChanged(float time)
    {
		OnTimerChanged?.Invoke(time);
	}

	public void PlayerChanged(IPlayer player)
    {
		OnPlayerChanged?.Invoke(player);
	}

	public void AddPlayer(IPlayer player)
    {
		OnAddPlayer?.Invoke(player);
	}
}

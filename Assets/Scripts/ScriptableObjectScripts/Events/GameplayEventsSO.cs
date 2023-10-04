using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayEvents", menuName = "ScriptableObjects/GameplayEvents")]
public class GameplayEventsSO : ScriptableObject
{
	public event Action OnGameplayStarted;
	public event Action OnGameplayFinished;
	public event Action<IPlayer> OnGameOver;

	public void GameplayStarted()
    {
		OnGameplayStarted?.Invoke();
	}

	public void GameplayFinished()
    {
		OnGameplayFinished?.Invoke();
	}

	public void GameOver(IPlayer player)
    {
		OnGameOver?.Invoke(player);
	}
}

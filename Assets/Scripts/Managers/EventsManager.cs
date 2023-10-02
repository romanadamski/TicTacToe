using System;
using UnityEngine;

public class EventsManager : BaseManager<EventsManager>
{
    public event Action GameplayStarted;
    public void OnGameplayStarted()
    {
        GameplayStarted?.Invoke();
    }

    public event Action<IPlayer> PlayerChanged;
    public void OnPlayerChanged(IPlayer player)
    {
        PlayerChanged?.Invoke(player);
    }

    public event Action<IPlayer> GameOver;
    public void OnGameOver(IPlayer player)
    {
        GameOver?.Invoke(player);
    }

    public event Action<Vector2Int, NodeType> NodeMark;
    public void OnNodeMark(Vector2Int index, NodeType nodeType)
    {
		NodeMark?.Invoke(index, nodeType);
    }

    public event Action<Vector2Int> Hint;
    public void OnHint(Vector2Int index)
    {
		Hint?.Invoke(index);
    }

    public event Action<float> TimerChanged;
    public void OnTimerChanged(float time)
    {
        TimerChanged?.Invoke(time);
    }
}

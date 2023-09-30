using System;

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

    public event Action<NodeType> GameOver;
    public void OnGameOver(NodeType nodeType)
    {
        GameOver?.Invoke(nodeType);
    }
}

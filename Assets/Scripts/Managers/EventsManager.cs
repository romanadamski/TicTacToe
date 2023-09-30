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

    public event Action<IPlayer> GameOver;
    public void OnGameOver(IPlayer endGamePlayerType)
    {
        GameOver?.Invoke(endGamePlayerType);
    }
}

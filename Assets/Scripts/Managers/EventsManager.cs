using System;

public class EventsManager : BaseManager<EventsManager>
{
    public event Action GameplayStarted;
    public void OnGameplayStarted()
    {
        GameplayStarted?.Invoke();
    }

    public event Action<PlayerType> PlayerChanged;
    public void OnPlayerChanged(PlayerType playerType)
    {
        PlayerChanged?.Invoke(playerType);
    }

    public event Action<PlayerType> GameOver;
    public void OnGameOver(PlayerType endGamePlayerType)
    {
        GameOver?.Invoke(endGamePlayerType);
    }
}

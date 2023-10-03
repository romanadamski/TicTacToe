using UnityEngine;
using Zenject;

public abstract class SetPlayerBaseChoice : ActionChoice
{
    [SerializeField]
    private PlayerNumber playerNumber;

    [Inject]
    protected TurnController _turnController;

    protected abstract IPlayer Player { get; }

    public override void Excecute()
    {
        switch (playerNumber)
        {
            case PlayerNumber.PlayerOne:
                _turnController.PlayerOne = Player;
                break;
            case PlayerNumber.PlayerTwo:
                _turnController.PlayerTwo = Player;
                break;
            default:
                break;
        }
    }
}

using UnityEngine;
using Zenject;

public abstract class SetPlayerBaseChoice : ActionChoice
{
    [SerializeField]
    private PlayerNumber playerNumber;

    [SerializeField]
    private TurnStateSO turnStateSO;

    protected abstract IPlayer Player { get; }

    public override void Excecute()
    {
        switch (playerNumber)
        {
            case PlayerNumber.PlayerOne:
                turnStateSO.PlayerOne = Player;
                break;
            case PlayerNumber.PlayerTwo:
                turnStateSO.PlayerTwo = Player;
                break;
            default:
                break;
        }
    }
}

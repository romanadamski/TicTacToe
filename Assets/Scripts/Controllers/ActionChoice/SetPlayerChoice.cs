using UnityEngine;
using Zenject;

/// <summary>
/// Set PlayerOne or PlayerTwo with given IPlayer component in Execute method depends on <typeparamref name="PlayerNumber"/>
/// </summary>
public class SetPlayerChoice : ActionChoice
{
    [SerializeField]
    private PlayerNumber playerNumber;
    [SerializeField]
    private TurnStateSO turnStateSO;
    [SerializeField]
    private IPlayer PlayerPrefab;

    private IPlayer Player;

    [Inject]
    private DiContainer _diContainer;

    private void Awake()
    {
        Player = _diContainer.InstantiatePrefab(PlayerPrefab, transform).GetComponent<IPlayer>();
    }

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

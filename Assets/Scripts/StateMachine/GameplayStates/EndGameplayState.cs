public class EndGameplayState : State
{
    public EndGameplayState(StateMachine stateMachine, GameEventsSO gameEventsSO) : base(stateMachine)
    {
        this.gameEventsSO = gameEventsSO;
    }

    private GameEventsSO gameEventsSO;

    protected override void OnEnter()
    {
        GameplayManager.Instance.ClearGameplayStateMachine();
    }

    protected override void OnExit()
    {
        gameEventsSO.GoToMainMenu();
    }
}

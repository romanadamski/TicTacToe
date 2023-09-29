public class EndGameplayState : State
{
    public EndGameplayState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        GameplayManager.Instance.ClearGameplayStateMachine();
    }

    protected override void OnExit()
    {
        GameManager.Instance.GoToMainMenu();
    }
}

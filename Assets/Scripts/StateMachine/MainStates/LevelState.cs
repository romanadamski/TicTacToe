public class LevelState : State
{
    public LevelState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        GameplayManager.Instance.SetGameplayState();
    }
}

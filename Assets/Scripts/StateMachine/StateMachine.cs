using UnityEngine;

/// <summary>
/// Manages states flow, provide reference to current state within single state machine. Game can has multipie state machines.
/// </summary>
public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    /// <summary>
    /// Replace current state with given one
    /// </summary>
    /// <param name="state">State to set</param>
    public void SetState(State state)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = state;

        CurrentState.Enter();
    }

    /// <summary>
    /// Clear state machine: exit current state and set it to null
    /// </summary>
    public void Clear()
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = null;
    }
}

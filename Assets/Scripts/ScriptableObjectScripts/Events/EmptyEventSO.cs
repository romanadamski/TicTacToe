using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EmptyEvent", menuName = "ScriptableObjects/EmptyEvent")]
public class EmptyEventSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}

using UnityEngine;

/// <summary>
/// Base class for strategy based pattern, provides Execute method to override
/// </summary>
public abstract class ActionChoice : MonoBehaviour
{
    public abstract void Excecute();
}

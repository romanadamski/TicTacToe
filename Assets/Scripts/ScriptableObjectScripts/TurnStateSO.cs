using UnityEngine;

[CreateAssetMenu(fileName = "TurnState", menuName = "ScriptableObjects/TurnState")]
public class TurnStateSO : ScriptableObject
{
    public IPlayer PlayerOne;
    public IPlayer PlayerTwo;
}

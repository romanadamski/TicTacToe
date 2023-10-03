using UnityEngine;

[CreateAssetMenu(fileName = "SettingsSO", menuName = "ScriptableObjects/Settings")]
public class SettingsSO : ScriptableObject
{
    public float PlayerTurnTimeLimit;
    public uint HorizontalNodes;
    public uint VerticalNodes;
    public uint WinningNodes;
}

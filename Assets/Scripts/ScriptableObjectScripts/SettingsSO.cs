using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings")]
public class SettingsSO : ScriptableObject
{
    [SerializeField]
    public float PlayerTurnTimeLimit;
    [SerializeField]
    public uint HorizontalNodes;
    [SerializeField]
    public uint VerticalNodes;
    [SerializeField]
    public uint WinningNodes;
}

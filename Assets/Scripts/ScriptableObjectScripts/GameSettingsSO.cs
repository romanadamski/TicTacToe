using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    private float playerTurnTime = 5.0f;
    public float PlayerTurnTime => playerTurnTime;
	[SerializeField]
	private uint horizontalNodes;
    public uint HorizontalNodes => horizontalNodes;
	[SerializeField]
	private uint verticalNodes;
    public uint VerticalNodes => verticalNodes;
	[SerializeField]
	private uint winNodes;
    public uint WinNodes => winNodes;
}

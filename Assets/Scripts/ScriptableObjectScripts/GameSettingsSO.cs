using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    private float playerTurnTime = 5.0f;
    public float PlayerTurnTime => playerTurnTime;

	[SerializeField]
	private uint horizontalNodes = 3;
    public uint HorizontalNodes => horizontalNodes;

	[SerializeField]
	private uint verticalNodes = 3;
    public uint VerticalNodes => verticalNodes;

	[SerializeField]
	private uint winNodes = 3;
    public uint WinNodes => winNodes;
}

using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    private float playerTurnTime = 5.0f;
    public float PlayerTurnTime => playerTurnTime;

	[Range(3, 10)]
	[SerializeField]
	private uint horizontalNodes = 3;
    public uint HorizontalNodes => horizontalNodes;

	[Range(3, 10)]
	[SerializeField]
	private uint verticalNodes = 3;
    public uint VerticalNodes => verticalNodes;

	[Range(3, 10)]
	[SerializeField]
	private uint winNodes = 3;
    public uint WinNodes => winNodes;
}

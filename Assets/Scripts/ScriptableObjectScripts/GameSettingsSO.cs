using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    private float minPlayerTurnTimeLimit = 1.0f;
    public float MinPlayerTurnTimeLimit => minPlayerTurnTimeLimit;

    [SerializeField]
    private float maxPlayerTurnTimeLimit = 99.0f;
    public float MaxPlayerTurnTimeLimit => maxPlayerTurnTimeLimit;

	[SerializeField]
	private uint minHorizontalNodes = 3;
    public uint MinHorizontalNodes => minHorizontalNodes;

	[SerializeField]
	private uint maxHorizontalNodes = 10;
	public uint MaxHorizontalNodes => maxHorizontalNodes;

	[SerializeField]
	private uint minVerticalNodes = 3;
	public uint MinVerticalNodes => minVerticalNodes;

	[SerializeField]
	private uint maxVerticalNodes = 10;
    public uint MaxVerticalNodes => maxVerticalNodes;
}

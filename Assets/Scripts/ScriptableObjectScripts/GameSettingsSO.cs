using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    private uint verticalTilesCount;
    public uint VerticalTilesCount => verticalTilesCount;
    [SerializeField]
    private uint horizontalTilesCount;
    public uint HorizontalTilesCount => horizontalTilesCount;
    [SerializeField]
    private uint winningTilesCount;
    public uint WinningTilesCount => winningTilesCount;
    [SerializeField]
	public Sprite PlayerOne;
    [SerializeField]
	public Sprite PlayerTwo;
    [SerializeField]
	public Sprite background;
    [SerializeField]
    private float playerTurnTime = 5.0f;
    public float PlayerTurnTime => playerTurnTime;
}

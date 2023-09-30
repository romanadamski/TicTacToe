using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsScriptableObject : ScriptableObject
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
    private Sprite playerOne;
    public Sprite PlayerOne => playerOne;
    [SerializeField]
    private Sprite playerTwo;
    public Sprite PlayerTwo => playerTwo;
    [SerializeField]
    private float playerTurnTime = 5.0f;
    public float PlayerTurnTime => playerTurnTime;
}

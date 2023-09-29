using UnityEngine;

public class GameSettingsManager : BaseManager<GameSettingsManager>
{
    [SerializeField]
    private GameSettingsScriptableObject settings;
    public GameSettingsScriptableObject Settings => settings;
}

using System;
using UnityEngine;

public class SaveManager : BaseManager<SaveManager>
{
    private const string SAVE_KEY = "SAVE";
    private SaveData _saveData;

    /// <summary>
    /// Subscribe to this event to save game on application quit
    /// </summary>
    public event Action<SaveData> OnSaveGame;

    /// <summary>
    /// Subscribe to this event to load save data on start of the game
    /// </summary>
    public event Action<SaveData> OnLoadGame;

    private void Start()
    {
        LoadGame();
    }

    private void SaveGame()
    {
        OnSaveGame?.Invoke(_saveData);

        var saveString = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(SAVE_KEY, saveString);
    }

    private void LoadGame()
    {
        var saveString = PlayerPrefs.GetString(SAVE_KEY);
        _saveData = JsonUtility.FromJson<SaveData>(saveString);

        if (_saveData == null)
        {
            NewGame();
        }

        OnLoadGame?.Invoke(_saveData);
    }

    private void NewGame()
    {
        _saveData = new SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

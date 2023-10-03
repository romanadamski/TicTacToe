using System;
using UnityEngine;

public class SaveManager : BaseManager<SaveManager>
{
    private const string SAVE_KEY = "SAVE";
    private SaveData _saveData;

    public event Action<SaveData> OnSaveGame;
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
        if(_saveData == null)
        {
            NewGame();
        }
        var saveString = PlayerPrefs.GetString(SAVE_KEY);
        _saveData = JsonUtility.FromJson<SaveData>(saveString);

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

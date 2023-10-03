using UnityEngine;

public class SaveManager
{
    private const string SAVE_KEY = "SAVE";
    public SaveData SaveData { get; private set; }

    public void Save()
    {
        var saveString = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(SAVE_KEY, saveString);
    }

    public void LoadSave()
    {
        var saveString = PlayerPrefs.GetString(SAVE_KEY);
        if (string.IsNullOrWhiteSpace(saveString))
        {
            SaveData = new SaveData();
        }
        else
        {
            SaveData = JsonUtility.FromJson<SaveData>(saveString);
        }
    }
}

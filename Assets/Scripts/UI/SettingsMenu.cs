using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Slider verticalNodesSlider;
    [SerializeField]
    private Slider horizontalNodesSlider;
    [SerializeField]
    private Slider winningNodesSlider;
    [SerializeField]
    private TextMeshProUGUI verticalNodesValue;
    [SerializeField]
    private TextMeshProUGUI horizontalNodesValue;
    [SerializeField]
    private TextMeshProUGUI winningNodesValue;
    [SerializeField]
    private TMP_InputField playerTurnTimeLimitInput;
    [SerializeField]
    private GameLimitValuesSO gameLimitValuesSO;
    [SerializeField]
    private SettingsSO settingsSO;
    
    private void Awake()
    {
        SaveManager.Instance.OnLoadGame += OnLoadGame;
        SaveManager.Instance.OnSaveGame += OnSaveGame;

        menuButton.onClick.AddListener(OnMenuButtonClick);

        SetSizeSlidersLimits();
        verticalNodesSlider.onValueChanged.AddListener(OnVerticalNodesSliderValueChanged);
        horizontalNodesSlider.onValueChanged.AddListener(OnHorizontalNodesSliderValueChanged);
        winningNodesSlider.onValueChanged.AddListener(OnWinningNodesSliderValueChanged);
        playerTurnTimeLimitInput.onDeselect.AddListener(OnPlayerTimeLimitInputDeselect);
        playerTurnTimeLimitInput.onValueChanged.AddListener(OnPlayerTimeLimitInputValueChanged);
    }

    public void OnLoadGame(SaveData saveData)
    {
        LoadData(saveData);
        RefreshUI();
    }

    private void LoadData(SaveData saveData)
    {
        settingsSO.VerticalNodes = (uint)saveData.verticalNodes;
        settingsSO.HorizontalNodes = (uint)saveData.horizontalNodes;
        settingsSO.WinningNodes = (uint)saveData.winningNodes;
        settingsSO.PlayerTurnTimeLimit = saveData.playerTurnTimeLimit;
    }

    public void OnSaveGame(SaveData saveData)
    {
        saveData.verticalNodes = verticalNodesSlider.value;
        saveData.horizontalNodes = horizontalNodesSlider.value;
        saveData.winningNodes = winningNodesSlider.value;
        saveData.playerTurnTimeLimit = float.Parse(playerTurnTimeLimitInput.text);
    }

    private void RefreshUI()
    {
        verticalNodesSlider.value = settingsSO.VerticalNodes;
        horizontalNodesSlider.value = settingsSO.HorizontalNodes;
        winningNodesSlider.value = settingsSO.WinningNodes;

        verticalNodesValue.text = verticalNodesSlider.value.ToString();
        horizontalNodesValue.text = horizontalNodesSlider.value.ToString();
        winningNodesValue.text = winningNodesSlider.value.ToString();
        playerTurnTimeLimitInput.text = settingsSO.PlayerTurnTimeLimit.ToString();
    }

    private void OnVerticalNodesSliderValueChanged(float value)
    {
        verticalNodesValue.text = value.ToString();
        SetWinningNodesSliderLimit();

        settingsSO.VerticalNodes = (uint)value;
    }

    private void OnHorizontalNodesSliderValueChanged(float value)
    {
        horizontalNodesValue.text = value.ToString();
        SetWinningNodesSliderLimit();

        settingsSO.HorizontalNodes = (uint)value;
    }

    private void OnWinningNodesSliderValueChanged(float value)
    {
        winningNodesValue.text = value.ToString();

        settingsSO.WinningNodes = (uint)value;
    }

    private void OnPlayerTimeLimitInputDeselect(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            playerTurnTimeLimitInput.text = gameLimitValuesSO.MinPlayerTurnTimeLimit.ToString();
        }
    }
    
    private void OnPlayerTimeLimitInputValueChanged(string text)
    {
        if (!float.TryParse(text, out var limit)) return;

        playerTurnTimeLimitInput.text = Mathf.Clamp(limit,
            gameLimitValuesSO.MinPlayerTurnTimeLimit,
            gameLimitValuesSO.MaxPlayerTurnTimeLimit).ToString();

        settingsSO.PlayerTurnTimeLimit = limit;
    }

    private void SetWinningNodesSliderLimit()
    {
        winningNodesSlider.maxValue = Mathf.Max(verticalNodesSlider.value, horizontalNodesSlider.value);
    }

    private void SetSizeSlidersLimits()
    {
        verticalNodesSlider.minValue = gameLimitValuesSO.MinVerticalNodes;
        horizontalNodesSlider.minValue = gameLimitValuesSO.MinHorizontalNodes;
        verticalNodesSlider.maxValue = gameLimitValuesSO.MaxVerticalNodes;
        horizontalNodesSlider.maxValue = gameLimitValuesSO.MaxHorizontalNodes;
        winningNodesSlider.minValue = Mathf.Min(verticalNodesSlider.minValue, horizontalNodesSlider.minValue);

        SetWinningNodesSliderLimit();
    }

    private void OnMenuButtonClick()
    {
        GameManager.Instance.SetMainMenuState();
    }
}

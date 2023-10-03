using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
    private Button saveButton;

    [Inject]
    private SaveManager _saveManager;

    private void Start()
    {
        menuButton.onClick.AddListener(OnMenuButtonClick);
        saveButton.onClick.AddListener(OnSaveButtonClick);

        SetSizeSlidersLimits();
        verticalNodesSlider.onValueChanged.AddListener(OnVerticalNodesSliderValueChanged);
        horizontalNodesSlider.onValueChanged.AddListener(OnHorizontalNodesSliderValueChanged);
        winningNodesSlider.onValueChanged.AddListener(OnWinningNodesSliderValueChanged);
        playerTurnTimeLimitInput.onDeselect.AddListener(OnPlayerTimeLimitInputDeselect);
        playerTurnTimeLimitInput.onValueChanged.AddListener(OnPlayerTimeLimitInputValueChanged);
    }

    public override void Show()
    {
        base.Show();

        LoadSave();
    }

    private void OnSaveButtonClick()
    {
        _saveManager.SaveData.verticalNodes = verticalNodesSlider.value;
        _saveManager.SaveData.horizontalNodes = winningNodesSlider.value;
        _saveManager.SaveData.winningNodes = winningNodesSlider.value;
        _saveManager.SaveData.playerTurnTimeLimit = float.Parse(playerTurnTimeLimitInput.text);
    }

    private void LoadSave()
    {
        verticalNodesSlider.value = _saveManager.SaveData.verticalNodes;
        horizontalNodesSlider.value = _saveManager.SaveData.horizontalNodes;
        winningNodesSlider.value = _saveManager.SaveData.winningNodes;
        playerTurnTimeLimitInput.text = _saveManager.SaveData.playerTurnTimeLimit.ToString();

        RefreshValues();
    }

    private void RefreshValues()
    {
        verticalNodesValue.text = verticalNodesSlider.value.ToString();
        horizontalNodesValue.text = horizontalNodesSlider.value.ToString();
        winningNodesValue.text = winningNodesSlider.value.ToString();
    }

    private void OnVerticalNodesSliderValueChanged(float value)
    {
        verticalNodesValue.text = value.ToString();
        SetWinningNodesSliderLimit();
    }

    private void OnHorizontalNodesSliderValueChanged(float value)
    {
        horizontalNodesValue.text = value.ToString();
        SetWinningNodesSliderLimit();
    }

    private void OnWinningNodesSliderValueChanged(float value)
    {
        winningNodesValue.text = value.ToString();
    }

    private void OnPlayerTimeLimitInputDeselect(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            playerTurnTimeLimitInput.text = GameManager.Instance.Settings.MinPlayerTurnTimeLimit.ToString();
        }
    }
    
    private void OnPlayerTimeLimitInputValueChanged(string text)
    {
        if (!float.TryParse(text, out var limit)) return;

        playerTurnTimeLimitInput.text = Mathf.Clamp(limit,
            GameManager.Instance.Settings.MinPlayerTurnTimeLimit,
            GameManager.Instance.Settings.MaxPlayerTurnTimeLimit).ToString();
    }

    private void SetWinningNodesSliderLimit()
    {
        winningNodesSlider.maxValue = Mathf.Max(verticalNodesSlider.value, horizontalNodesSlider.value);
    }

    private void SetSizeSlidersLimits()
    {
        verticalNodesSlider.minValue = GameManager.Instance.Settings.MinVerticalNodes;
        horizontalNodesSlider.minValue = GameManager.Instance.Settings.MinHorizontalNodes;
        verticalNodesSlider.maxValue = GameManager.Instance.Settings.MaxVerticalNodes;
        horizontalNodesSlider.maxValue = GameManager.Instance.Settings.MaxHorizontalNodes;
        winningNodesSlider.minValue = Mathf.Min(verticalNodesSlider.minValue, horizontalNodesSlider.minValue);

        SetWinningNodesSliderLimit();
    }

    private void OnMenuButtonClick()
    {
        GameManager.Instance.SetMainMenuState();
    }
}

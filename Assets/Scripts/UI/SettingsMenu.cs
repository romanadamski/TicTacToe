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

    [Inject]
    private SaveManager _saveManager;

    private void Start()
    {
        menuButton.onClick.AddListener(OnMenuButtonClick);

        SetSizeSlidersLimits();
        verticalNodesSlider.onValueChanged.AddListener(OnVerticalNodesSliderValueChanged);
        horizontalNodesSlider.onValueChanged.AddListener(OnHorizontalNodesSliderValueChanged);
        winningNodesSlider.onValueChanged.AddListener(OnWinningNodesSliderValueChanged);

        LoadSave();
    }

    private void LoadSave()
    {
        verticalNodesSlider.value = _saveManager.SaveData.verticalNodes;
        horizontalNodesSlider.value = _saveManager.SaveData.horizontalNodes;
        winningNodesSlider.value = _saveManager.SaveData.winningNodes;

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
        _saveManager.SaveData.verticalNodes = value;
        verticalNodesValue.text = value.ToString();
        SetWinningNodesSliderLimit();
    }

    private void OnHorizontalNodesSliderValueChanged(float value)
    {
        _saveManager.SaveData.horizontalNodes = value;
        horizontalNodesValue.text = value.ToString();
        SetWinningNodesSliderLimit();
    }

    private void OnWinningNodesSliderValueChanged(float value)
    {
        winningNodesValue.text = value.ToString();
        _saveManager.SaveData.winningNodes = value;
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

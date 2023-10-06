using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button reskinButton;
    [SerializeField]
    private TMP_InputField reskinInputField;
	[SerializeField]
	private AssetBundlePathSO assetBundlePathSO;
	[SerializeField]
	private ChoiceButtonGroup playerOneGameModeButtons;
	[SerializeField]
	private ChoiceButtonGroup playerTwoGameModeButtons;

	private void Start()
	{
		playerOneGameModeButtons.AddListener(SetStartButtonInteractable);
		playerTwoGameModeButtons.AddListener(SetStartButtonInteractable);

		SetStartButtonInteractable();

		startGameButton.onClick.AddListener(OnStartGameButtonClick);
		settingsButton.onClick.AddListener(OnSettingsButtonClick);
		reskinButton.onClick.AddListener(() => Reskin(reskinInputField.text));
	}

	private void OnStartGameButtonClick()
	{
		playerOneGameModeButtons.ExecuteSelected();
		playerTwoGameModeButtons.ExecuteSelected();
		GameManager.Instance.SetLevelState();
	}

	private void OnSettingsButtonClick()
	{
		GameManager.Instance.SetSettingsState();
	}

	public override void Show()
	{
		base.Show();

		playerOneGameModeButtons.DeselectAll();
		playerTwoGameModeButtons.DeselectAll();
		reskinInputField.text = string.Empty;
		SetStartButtonInteractable();
	}

	private void SetStartButtonInteractable()
	{
		startGameButton.interactable = playerOneGameModeButtons.AnyButtonSelected
			&& playerTwoGameModeButtons.AnyButtonSelected;
	}

    private void Reskin(string bundleName)
    {
		var assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));
		if (!assetBundle) return;

		var paths = assetBundlePathSO.AssetBundlePaths.FirstOrDefault(x => x.BundleName.Equals(bundleName));

		if (paths == null) return;

		if (!string.IsNullOrWhiteSpace(paths.XSpritePath))
		{
			var xSprite = assetBundle.LoadAsset<Sprite>(paths.XSpritePath);
			UIManager.Instance.PlayerOne = xSprite;
		}
		if (!string.IsNullOrWhiteSpace(paths.OSpritePath))
		{
			var oSprite = assetBundle.LoadAsset<Sprite>(paths.OSpritePath);
			UIManager.Instance.PlayerTwo = oSprite;
		}
		if (!string.IsNullOrWhiteSpace(paths.BGSpritePath))
		{
			var bgSprite = assetBundle.LoadAsset<Sprite>(paths.BGSpritePath);
			UIManager.Instance.SetBackgroundSprite(bgSprite);
		}

		assetBundle.Unload(false);
	}
}

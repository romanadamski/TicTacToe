using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button reskinButton;
    [SerializeField]
    private TMP_InputField reskinInputField;
    [SerializeField]
    private List<GameModeButton> playerOneButtons;
    [SerializeField]
    private List<GameModeButton> playerTwoButtons;
    [SerializeField]
	private AssetBundlePathSO assetBundlePathSO;

	private void Start()
	{
        startGameButton.onClick.AddListener(OnStartGameButtonClick);
		reskinButton.onClick.AddListener(() => Reskin(reskinInputField.text));
		playerOneButtons.ForEach(button => button.Button.onClick.AddListener(() => OnPlayerOneGameModeButtonClick(button, playerOneButtons)));
		playerTwoButtons.ForEach(button => button.Button.onClick.AddListener(() => OnPlayerTwoGameModeButtonClick(button, playerTwoButtons)));
		SetStartButtonInteractable();
	}

	public override void Show()
	{
		base.Show();

		playerOneButtons.ForEach(button => button.DeselectButton());
		playerTwoButtons.ForEach(button => button.DeselectButton());
		reskinInputField.text = string.Empty;
		SetStartButtonInteractable();
	}

	private void OnPlayerOneGameModeButtonClick(GameModeButton sender, List<GameModeButton> playerOneButtons)
	{
		playerOneButtons.ForEach(button => button.DeselectButton());
		sender.SelectButton();
		SetStartButtonInteractable();
	}

	private void OnPlayerTwoGameModeButtonClick(GameModeButton sender, List<GameModeButton> playerTwoButtons)
	{
		playerTwoButtons.ForEach(button => button.DeselectButton());
		sender.SelectButton();
		SetStartButtonInteractable();
	}

	private void SetStartButtonInteractable()
	{
		startGameButton.interactable = playerOneButtons.Any(button => button.Active)
			&& playerTwoButtons.Any(button => button.Active);
	}

	private void OnStartGameButtonClick()
    {
        GameManager.Instance.SetLevelState();
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

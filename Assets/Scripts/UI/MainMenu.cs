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
    private GameModeButton PvP;
    [SerializeField]
    private GameModeButton PvC;
    [SerializeField]
	private GameModeButton CvC;
	[SerializeField]
	private AssetBundlePathSO assetBundlePathSO;

	private GameModeButton[] _gameModeButtons;

	private void Awake()
    {
		_gameModeButtons = GetComponentsInChildren<GameModeButton>();
	}

	private void Start()
	{
		foreach(var button in _gameModeButtons)
        {
			button.Button.onClick.AddListener(() => OnGameModeButtonClick(button));
		}
		SetStartButtonInteractable();

		startGameButton.onClick.AddListener(OnStartGameButtonClick);
		reskinButton.onClick.AddListener(() => Reskin(reskinInputField.text));
		PvP.Button.onClick.AddListener(() => OnGameModeButtonClick());
		PvC.Button.onClick.AddListener(() => OnGameModeButtonClick());
		CvC.Button.onClick.AddListener(() => OnGameModeButtonClick());
	}

	private void OnStartGameButtonClick()
	{
		GameManager.Instance.SetLevelState();
	}

	private void OnGameModeButtonClick()
    {
	}

	public override void Show()
	{
		base.Show();

		DeselectButtons();
		reskinInputField.text = string.Empty;
		SetStartButtonInteractable();
	}

	private void DeselectButtons()
    {
		foreach (var button in _gameModeButtons)
		{
			button.DeselectButton();
		}
	}

	private void OnGameModeButtonClick(GameModeButton button)
	{
		DeselectButtons();
		button.SelectButton();
		SetStartButtonInteractable();
	}

	private void SetStartButtonInteractable()
	{
		startGameButton.interactable = _gameModeButtons.Any(button => button.Active);
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

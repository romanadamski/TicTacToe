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
    private Button reskinButton;
    [SerializeField]
    private TMP_InputField reskinInputField;
    [SerializeField]
	private AssetBundlePathSO assetBundlePathSO;
    [SerializeField]
	private GameSettingsSO settings;

    private void Awake()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonClick);
		reskinButton.onClick.AddListener(() => Reskin(reskinInputField.text));

		Reskin("main");
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
			settings.PlayerOne = xSprite;
		}
		if (!string.IsNullOrWhiteSpace(paths.OSpritePath))
		{
			var oSprite = assetBundle.LoadAsset<Sprite>(paths.OSpritePath);
			settings.PlayerTwo = oSprite;
		}
		if (!string.IsNullOrWhiteSpace(paths.BGSpritePath))
		{
			var bgSprite = assetBundle.LoadAsset<Sprite>(paths.BGSpritePath);
			settings.background = bgSprite;
		}

		assetBundle.Unload(false);
	}
}

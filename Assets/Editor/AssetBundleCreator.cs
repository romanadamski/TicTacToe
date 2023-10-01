using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class AssetBundleCreator : EditorWindow
{
    private static Sprite _exSprite;
    private static Sprite _oSprite;
    private static Sprite _backgroundSprite;
    private static string _assetBundleName;
    private static AssetBundlePathSO _assetBundlePathSO;
    private static Vector2 _scrollPos;
    private const string ASSET_BUNDLE_PATH = "Assets/assetBundlePath.asset";
	private const float IMAGE_SIZE = 150.0f;

	[MenuItem("Assets/Asset bundle creator _F3", false, 50)]
	private static void ShowWindow()
	{
		Refresh();
		LoadAssetBundlePathSO();

		var window = HasOpenInstances<AssetBundleCreator>()
			? GetWindow<AssetBundleCreator>("Asset bundle creator")
			: CreateWindow<AssetBundleCreator>("Asset bundle creator");
		window.minSize = new Vector2(210, 200);
	}

	private static void LoadAssetBundlePathSO()
	{
		if (!File.Exists(ASSET_BUNDLE_PATH))
		{
			_assetBundlePathSO = CreateInstance<AssetBundlePathSO>();
			var uniquePath = AssetDatabase.GenerateUniqueAssetPath(ASSET_BUNDLE_PATH);
			AssetDatabase.CreateAsset(_assetBundlePathSO, uniquePath);
			AssetDatabase.SaveAssets();
		}
		else
		{
			_assetBundlePathSO = AssetDatabase.LoadAssetAtPath<AssetBundlePathSO>(ASSET_BUNDLE_PATH);
		}
	}

	private static void Refresh()
	{
		_exSprite = null;
		_oSprite = null;
		_backgroundSprite = null;
		_assetBundleName = string.Empty;
	}

	private static bool ValidBuild(out string message)
	{
		message = string.Empty;
		var result = true;

		if (string.IsNullOrWhiteSpace(_assetBundleName))
		{
			message = "Asset bundle file name is empty!";
			result = false;
		}
		if (_assetBundleName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
		{
			message = "Asset bundle file name contains invalid characters!";
			result = false;
		}
		var filePath = Path.Combine(Application.streamingAssetsPath, _assetBundleName);
		if (File.Exists(filePath))
		{
			message = $"File in path {filePath} already exist!";
			result = false;
		}

		return result;
	}

	private static void BuildAssetBundles()
	{
		if(!ValidBuild(out var message))
		{
			Debug.LogError(message);
			return;
		}

		try
		{
			if(!Directory.Exists(Application.streamingAssetsPath))
			{
				Directory.CreateDirectory(Application.streamingAssetsPath);
			}

			AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

			buildMap[0].assetBundleName = _assetBundleName;

			var assetsNames = new string[3];
			var exSpritePath = AssetDatabase.GetAssetPath(_exSprite);
			var oSpritePath = AssetDatabase.GetAssetPath(_oSprite);
			var bgSpritePath = AssetDatabase.GetAssetPath(_backgroundSprite);
			assetsNames[0] = exSpritePath;
			assetsNames[1] = oSpritePath;
			assetsNames[2] = bgSpritePath;
			buildMap[0].assetNames = assetsNames;

			var result = BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, buildMap,
				BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
			
			if (result)
			{
				var assetBundlePath = new AssetBundlePath(_assetBundleName, exSpritePath, oSpritePath, bgSpritePath);
				if (!_assetBundlePathSO)
				{
					LoadAssetBundlePathSO();
				}
				_assetBundlePathSO.AssetBundlePaths.Add(assetBundlePath);
				EditorUtility.SetDirty(_assetBundlePathSO);
				AssetDatabase.SaveAssets();
				Debug.Log($"Created asset bundle: {_assetBundleName.Bold()} succesfully!");

				Refresh();
			}
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
		}
	}

	private void OnGUI()
	{
		_assetBundleName = EditorGUILayout.TextField("Asset bundle name:", _assetBundleName);
		if (GUILayout.Button("Build", GUILayout.Width(200), GUILayout.Height(20)))
		{
			BuildAssetBundles();
		}

		EditorGUILayout.BeginVertical();
		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

		EditorGUILayout.LabelField("X Sprite:");
		EditorGUILayout.LabelField(_exSprite?.name);
		_exSprite = (Sprite)EditorGUILayout.ObjectField(_exSprite, typeof(Sprite), false, GUILayout.Width(IMAGE_SIZE), GUILayout.Height(IMAGE_SIZE));

		EditorGUILayout.LabelField("O Sprite:");
		EditorGUILayout.LabelField(_oSprite?.name);
		_oSprite = (Sprite)EditorGUILayout.ObjectField(_oSprite, typeof(Sprite), false, GUILayout.Width(IMAGE_SIZE), GUILayout.Height(IMAGE_SIZE));

		EditorGUILayout.LabelField("BG Sprite:");
		EditorGUILayout.LabelField(_backgroundSprite?.name);
		_backgroundSprite = (Sprite)EditorGUILayout.ObjectField(_backgroundSprite, typeof(Sprite), false, GUILayout.Width(IMAGE_SIZE), GUILayout.Height(IMAGE_SIZE));

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}
}

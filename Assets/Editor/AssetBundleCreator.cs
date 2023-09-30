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
    private const string _assetBundlePath = "Assets/assetBundlePath.asset";

	[MenuItem("Assets/Asset bundle creator _F3", false, 50)]
	private static void ShowWindow()
	{
		Refresh();
		LoadAssetBundlePathSO();

		var window = HasOpenInstances<AssetBundleCreator>()
			? GetWindow<AssetBundleCreator>("Asset bundle creator")
			: CreateWindow<AssetBundleCreator>("Asset bundle creator");
		window.Show();
	}

	private static void LoadAssetBundlePathSO()
	{
		if (!File.Exists(_assetBundlePath))
		{
			_assetBundlePathSO = CreateInstance<AssetBundlePathSO>();
			var uniquePath = AssetDatabase.GenerateUniqueAssetPath(_assetBundlePath);
			AssetDatabase.CreateAsset(_assetBundlePathSO, uniquePath);
			AssetDatabase.SaveAssets();
		}
		else
		{
			_assetBundlePathSO = AssetDatabase.LoadAssetAtPath<AssetBundlePathSO>(_assetBundlePath);
		}
	}

	private static void Refresh()
	{
		_exSprite = null;
		_oSprite = null;
		_backgroundSprite = null;
		_assetBundleName = string.Empty;
	}

	private static void BuildAssetBundles()
	{
		if(string.IsNullOrWhiteSpace(_assetBundleName))
		{
			Debug.LogError("Asset bundle file name is empty!");
			return;
		}
		if(_assetBundleName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
		{
			Debug.LogError("Asset bundle file name contains invalid characters!");
			return;
		}
		var filePath = Path.Combine(Application.streamingAssetsPath, _assetBundleName);
		if (File.Exists(filePath))
		{
			Debug.LogError($"File in path {filePath} already exist!");
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
				Debug.Log($"Created asset bundle in: {filePath} succesfully!");

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

		var cellRect = new Rect(10, EditorGUIUtility.singleLineHeight * 3, 200, EditorGUIUtility.singleLineHeight);
		GUI.Label(cellRect, "X Sprite:");
		cellRect.x += 60;

		GUI.Label(cellRect, _exSprite?.name);
		cellRect.x = 10;
		cellRect.y += EditorGUIUtility.singleLineHeight;
		cellRect.height = 200;

		_exSprite = (Sprite)EditorGUI.ObjectField(cellRect, _exSprite, typeof(Sprite), false);
		cellRect.y += EditorGUIUtility.singleLineHeight;

		cellRect.y += cellRect.height;
		cellRect.height = EditorGUIUtility.singleLineHeight;

		GUI.Label(cellRect, "O Sprite:");
		cellRect.x += 60;

		GUI.Label(cellRect, _oSprite?.name);
		cellRect.x = 10;
		cellRect.y += EditorGUIUtility.singleLineHeight;
		cellRect.height = 200;

		_oSprite = (Sprite)EditorGUI.ObjectField(cellRect, _oSprite, typeof(Sprite), false);
		cellRect.y += EditorGUIUtility.singleLineHeight;

		cellRect.y += cellRect.height;
		cellRect.height = EditorGUIUtility.singleLineHeight;

		GUI.Label(cellRect, "BG Sprite:");
		cellRect.x += 60;

		GUI.Label(cellRect, _backgroundSprite?.name);
		cellRect.x = 10;
		cellRect.y += EditorGUIUtility.singleLineHeight;
		cellRect.height = 200;

		_backgroundSprite = (Sprite)EditorGUI.ObjectField(cellRect, _backgroundSprite, typeof(Sprite), false);
	}
}

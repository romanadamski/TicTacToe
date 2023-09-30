using System;

[Serializable]
public class AssetBundlePath
{
	public string BundleName;
	public string XSpritePath;
	public string OSpritePath;
	public string BGSpritePath;

	public AssetBundlePath(string bundleName, string xSpritePath, string oSpritePath, string bGSpritePath)
	{
		BundleName = bundleName;
		XSpritePath = xSpritePath;
		OSpritePath = oSpritePath;
		BGSpritePath = bGSpritePath;
	}
}

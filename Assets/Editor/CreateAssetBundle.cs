using UnityEditor;
using UnityEngine;

public class CreateAssetBundle
{
	[MenuItem("Assets/Project/Create Bundle")]
	static void CreateBundle()
	{
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
	}
}


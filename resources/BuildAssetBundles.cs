using UnityEditor;
using System.IO;
using UnityEngine;

public class BuildAssetBundles
{
    [MenuItem("Assets/Build My AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string platform = EditorUserBuildSettings.activeBuildTarget.ToString();
        string assetBundleDirectory = "Assets/AssetBundles";

        if (!Directory.Exists(assetBundleDirectory))
            Directory.CreateDirectory(assetBundleDirectory);

        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        EditorUserBuildSettings.activeBuildTarget);

        Debug.Log($"AssetBundles built successfully at: {assetBundleDirectory}");
    }
}

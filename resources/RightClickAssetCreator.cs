using UnityEngine;
using UnityEditor;
using System.IO;

public class RightClickSpriteAssetCreator
{
    [MenuItem("Assets/Generate Sprite Asset From This", false, 1)]
    public static void CreateSpriteAsset()
    {
        Object selectedObject = Selection.activeObject;
        if (selectedObject == null) return;

        string selectedPath = AssetDatabase.GetAssetPath(selectedObject);

        // 1. Load the actual Sprite object from the PNG file
        Sprite sourceSprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedPath);

        if (sourceSprite == null)
        {
            Debug.LogError("The selected file is not configured as a Sprite. Change its Texture Type to 'Sprite (2D and UI)' first.");
            return;
        }

        // 2. Setup the output path
        string directory = Path.GetDirectoryName(selectedPath);
        string fileName = Path.GetFileNameWithoutExtension(selectedPath);
        string newAssetPath = Path.Combine(directory, fileName + "_Sprite.asset");

        // 3. Instantiate a standalone copy of the Sprite object
        Sprite newSprite = Object.Instantiate(sourceSprite);

        // 4. Save it as a raw standalone .asset file
        AssetDatabase.CreateAsset(newSprite, newAssetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(newSprite);
        Debug.Log($"Successfully created standalone Sprite asset at: {newAssetPath}");
    }

    [MenuItem("Assets/Generate Sprite Asset From This", true)]
    public static bool ValidateCreateSpriteAsset()
    {
        return Selection.activeObject != null && !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(Selection.activeObject));
    }
}

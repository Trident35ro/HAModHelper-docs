# Loading Custom Assets

This guide explains how to build Unity AssetBundles and embed them directly into your mod's `.dll` plugin file to load at runtime.

---

## Step 1: Exporting AssetBundles from Unity

To package your custom 3D models, prefabs, or textures into an AssetBundle:

1. Create a folder named `Editor` inside your Unity project's `Assets` directory (`Assets/Editor`).
2. Download the build script below and save it as `BuildAssetBundles.cs` inside the `Assets/Editor` folder.

> 📥 **[Download BuildAssetBundles.cs](../../resources/BuildAssetBundles.cs)**

3. Select all the assets/prefabs you want to inject into the game.
4. In the Unity Inspector window, assign the **same AssetBundle tag** to every selected asset at the bottom of the panel.
5. In Unity's top navigation bar, click **Assets > Build My AssetBundles**.
6. The compiled bundle will be generated in `Assets/AssetBundles/`.

> [!TIP]
> If you're bundling a sprite that comes from a source PNG/texture, save
> [RightClickAssetCreator.cs](../../resources/RightClickAssetCreator.cs) into `Assets/Editor` too.
> It adds a **Generate Sprite Asset From This** entry to the right-click menu for any texture already
> set to Texture Type "Sprite (2D and UI)", producing a standalone `.asset` copy of just that Sprite.
> Tag *that* asset for your bundle instead of the raw texture — it keeps the bundle from also pulling
> in the source texture's import settings.

---

## Step 2: Embed the Asset in Your Mod Project

Copy your compiled AssetBundle file into your C# project folder, then configure it as an embedded resource using **Option A** (GUI) or **Option B** (direct `.csproj` editing):

### Option A: IDE Graphical Interface (Visual Studio / Rider / VS Code)
1. Add the AssetBundle file to your project.
2. Right-click the file in your project explorer and open its **Properties** / **File Properties**.
3. Set the **Build Action** to **EmbeddedResource**.

### Option B: Project File (`.csproj`)
Alternatively, open your `.csproj` file in any text editor and add the file directly inside an `<ItemGroup>` tag:

```xml
<ItemGroup>
    <EmbeddedResource Include="Assets\mybundle"/>
</ItemGroup>
```

---

## Step 3: Load the Embedded AssetBundle in Code

Extract the bundle stream at runtime using `Assembly.GetManifestResourceStream()` and pass it into Unity's `AssetBundle.LoadFromStream()`:

```csharp
private AssetBundle? myBundle;

public override void Load()
{
    var assembly = Assembly.GetExecutingAssembly();
    
    // Manifest path format: DefaultNamespace.Folders.FileName
    using (Stream? stream = assembly.GetManifestResourceStream("MyCoolPlugin.Assets.mybundle"))
    {
        if (stream == null) return;

        myBundle = AssetBundle.LoadFromStream(stream);
        
        // Load and instantiate a prefab from the bundle
        GameObject prefab = myBundle.LoadAsset<GameObject>("MyPrefabName");
        if (prefab != null)
        {
            Object.Instantiate(prefab);
        }
    }
}
```
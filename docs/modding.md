# Getting Started with Modding

## Introduction

HAML and Hybrid Animals are built using C# on top of Unity. Before making your first mod, all you need is a basic understanding of C#.

* **C# Basics:** Check out [Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/).
* **Unity Reference:** See the [Unity 2021.3 Script Reference](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/index.html).

> **Note:** Reverse engineering tools (like dnSpy or Ghidra) are completely optional and only needed for advanced modding tasks.

---

## Modding Fundamentals

Modding with HAML comes down to two main ideas:

1. **BasePlugin:** The main entry point of your mod. When the game launches, HAML loads your mod and executes the code inside your `Load()` method.
2. **References (.dll files):** Assemblies containing game and BepInEx methods. Adding these references lets your IDE know what game features exist so you can use them in your code.

---

## Modding Tutorials & Guides

Check out these guides to learn how to add custom content to the game:

* [Loading Custom Assets](tutorials/loading-assets.md) — Import custom textures, models, and Unity AssetBundles.
* [Creating Custom Items](tutorials/creating-items.md) — Define item stats, icons, and register new items.
* [Injecting Crafting Recipes](tutorials/crafting-recipes.md) — Add custom recipes to crafting stations.
* [Adding New Entities & Creatures](tutorials/adding-creatures.md) — Spawn custom creatures and world prefabs.
* [Using the Event Bus](tutorials/event-bus.md) — Listen for and fire custom mod events.
* [Creating Perks & Abilities](tutorials/creating-perks.md) — Register custom active/passive perks.

---

## Method 1: Using the Template (Recommended)

The easiest way to get started is by using the project template.

1. Download and install the [**.NET 10 SDK**](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).
2. Download or clone the [Hybrid Animals Mod Template](https://github.com/XtraCube/HybridAnimalsModTemplate).
3. Open the folder in your IDE (Visual Studio, VS Code, or JetBrains Rider).
4. Rename the project files, folder, and code namespaces to match your mod's name.
5. **Add HAModHelper Reference:** Copy `HAModHelper.GamePlugin.dll` from your phone's `plugin` folder or download it from the Discord server (see [Introduction](introduction.md)) and add it as a reference so you can access the HAMH API.
6. Build your mod!

---

## Method 2: Making a Mod From Scratch

If you want to set up a project manually without using the template:

### 1. Grab Game References from Your Phone

Run FusionCore on your phone at least once to generate the required assemblies. Copy the `.dll` files you need from your phone to your PC.

Check these folders on your device:
```text
Internal Storage/FusionCore/com.abstractsoft.hybridanimals/BepInEx/
├── dummy/
├── interop/
├── core/
└── unity-libs/
```

### 2. Create the Project File

Create a file named `MyMod.csproj` in your project folder:

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net10.0</TargetFramework>
        <LangVersion>latest</LangVersion>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

    <ItemGroup>
        <!-- Reference BepInEx and HAModHelper DLLs copied from your phone -->
        <Reference Include="BepInEx.Unity.IL2CPP">
            <HintPath>path\to\BepInEx.Unity.IL2CPP.dll</HintPath>
        </Reference>
        <Reference Include="HAModHelper.GamePlugin">
            <HintPath>path\to\HAModHelper.GamePlugin.dll</HintPath>
        </Reference>
    </ItemGroup>

</Project>
```

### 3. Write Your Plugin Code

Create a file named `Plugin.cs` in the same directory:

```csharp
using BepInEx;
using BepInEx.Unity.IL2CPP;

namespace MyMod;

[BepInPlugin("com.yourname.mymod", "My Mod", "1.0.0")]
[BepInDependency("dev.allofus.hamodhelper")]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        // Simple log output to confirm your mod loaded
        Log.LogInfo("Hello world!");
    }
}
```

---

## Installing & Testing Your Mod

1. Build your project in your IDE or run `dotnet build` in your terminal.
2. Locate the compiled `.dll` file in `bin/Debug/net10.0/`.
3. Copy your `.dll` file to your phone:
   ```text
   Internal Storage/FusionCore/com.abstractsoft.hybridanimals/BepInEx/plugins/
   ```
4. Launch Hybrid Animals through FusionCore to test your mod!
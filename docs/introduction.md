Introduction
===

Simple tutorial
---

This guide will walk you though on how to set up HAML and start playing with mods or multiplayer.
As of speaking, HAML doesn't work on any Android emulator.

> [!IMPORTANT]
> Make sure to backup your original HA saves as those will be deleted with the uninstallation of HA.

> [!NOTE]
> To run HAML or FusionCore you need at least Android 11 for it to work. Also note that iOS isn't supported and never will be due to its nature and most Android 11 devices might also don't work even if it fits the version requirement.

To start up make sure you joined the [Hybrid Animals Modding](https://discord.gg/TYEhNVmXhw) Discord server. Now follow these steps:

1. Download the latest version of FusionCore from the [#haml-updates](https://discord.com/channels/1351005866996011060/1486848511554097172) channel and install it
2. Back up your HA saves if you haven't already (follow [this](../introduction.html#backup-your-saves) tutorial if you don't know how to do it)
3. Download and replace your latest version HA with HA v200613 (you can find it on websites like APKPure, UpToDown, etc.); make sure to go to the Hybrid Animals page on Google Play Store, click the 3 dots in the top-right corner and uncheck the auto-update option
4. Open FusionCore and select Hybrid Animals from the list
5. If you see a black screen then congrats, HAML works on your phone and you need to wait until the game finally gets to the main menu (don't worry, your game isn't broken, it just needs to do some internal stuff in the background); if the game crashes [get your logs](../introduction.html#get-your-log-file) or [use ADB](../introduction.html#using-adb) to get additional logs and make a bug report on the Discord server
6. Download the latest version of HAMH from [#hamh-updates](https://discord.com/channels/1351005866996011060/1486848544521584793) and copy it to ```FusionCore/com.abstractsoft.hybridanimals/BepInEx/plugins```
7. You're done, have fun!

This video shows how to do the steps above:

Coming soon

Expert tutorial
---

This tutorial is harder than the simple one and it might not be for you if you don't know what are you doing. The advantage of using this method is that you can have the original Hybrid Animals and you can also have HAML set up at the same time. Without any ado, let's jump into the steps of making this posible:
1. Follow the [Simple tutorial](../introduction.html#simple-tutorial) until the 3rd step, where you don't need to install the older version yet
2. Change the package name of the older version with another of a FusionCore's supported games (see [here](https://github.com/All-Of-Us-Mods/FusionCore/blob/main/fusionApp/src/main/AndroidManifest.xml#L14) a list of them) then use one of the following ways to do it:
* APKTool/APKTool M method: use APKTool (on PC) or APKTool M (on Android) to decompile the installation file; alternatively use APKTool M's "Quick Edit" feature to edit the package name directly without editing any file
* Archive method: rename the ```.apk``` file to ```.zip``` and open it like a normal archive, extract everything or just what you need
3. For both methods above do the following: get the ```AndroidManifest.xml``` file search for the ```package="com.abstractsoft.hybridanimals"``` line and replace ```com.abstractsoft.hybridanimals``` with any package from the list at the 2nd step after that replace the original file inside the installer or move it back and compile/archive it back
4. Install the app and continue with all the remaining steps, skipping the 3rd step; note that you need to use your new package instead of ```com.abstractsoft.hybridanimals``` in the other steps and the rest of the documentations; in FusionCore it shows the package name so use it to boot into the correct version
5. If you did everything correctly you should be good to go.

Get your log file
---

If you are having issues or if you want to see what's happening in the background you can check your automatically real-time updated log file.
It is located at ```FusionCore/com.abstractsoft.hybridanimals/BepInEx/LogOutput.log``` where you can send, copy or look at it yourself.

Using ADB
---

Coming soon

Backup your saves
---

Coming soon
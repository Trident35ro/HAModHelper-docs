Introduction
===

> [!NOTE]
> The text that it is between [] and have a * at the end contain steps or sub steps that should be done only the first time setting up.

Installing HAML
---

This guide will walk you though on how to set up HAML and start playing with mods or multiplayer.
As of speaking, HAML doesn't work on any Android emulator. Also you need exactly Hybrid Animals version 200613 installed.

> [!IMPORTANT]
> Make sure to backup your original HA saves as those will be deleted with the uninstallation of HA.

> [!NOTE]
> To run HAML or FusionCore you need at least Android 11 for it to work. Also note that iOS isn't supported and never will be due to its nature and most Android 11 devices might also don't work even if it fits the version requirement.

To start up make sure you joined the [Hybrid Animals Modding](https://discord.gg/TYEhNVmXhw) Discord server. Now follow these steps:

1. Download the latest version of FusionCore from the [#haml-updates](https://discord.com/channels/1351005866996011060/1486848511554097172) channel and install it
2. Back up your HA saves if you haven't already (follow [this](introduction.html#backuprestore-your-saves) tutorial if you don't know how to do it)
3. Download and replace your latest version HA with HA v200613 (you can find it on stores like APKPure, UpToDown, Aurora Store etc.); make sure to go to the Hybrid Animals page on Google Play Store, click the 3 dots in the top-right corner and uncheck the auto-update option
4. Open FusionCore and select Hybrid Animals from the list
5. If you see a black screen then congrats, HAML works on your phone and you need to wait until the game finally gets to the main menu (don't worry, your game isn't broken, it just needs to do some internal stuff in the background); if the game crashes [get your logs](introduction.html#get-your-log-file) or [use ADB](introduction.html#using-adb) to get additional logs and make a bug report on the Discord server
6. Download the latest version of HAMH from [#hamh-updates](https://discord.com/channels/1351005866996011060/1486848544521584793) and copy it to ```FusionCore/com.abstractsoft.hybridanimals/BepInEx/plugins```
7. You're done, have fun!

This video shows how to do the steps above (thank you Segual for the footage):

<p align="center">
    <video width="340" controls>
        <source src="/resources/installation.mp4" type="video/mp4">
        Your browser does not support HTML video.
    </video>
</p>

Get your log file
---

If you are having issues or if you want to see what's happening in the background you can check your automatically real-time updated log file.
It is located at ```FusionCore/com.abstractsoft.hybridanimals/BepInEx/LogOutput.log``` where you can send, copy or look at it yourself.

Using ADB
---

In some cases, when the game crashes the regular log file might not exist, be updated with the last session logs or be cut off. In all of these cases the errors that made the crash happen might not appear. In this case we can use Google's own tool, called [Android Debugging Bridge (ADB)](https://developer.android.com/tools/adb). This can be done on both a computer and the same phone.
Firstly this is how you do it with your computer:
1. [To start off, enable Developer Options/Settings on your phone; it depends on the phone brands how to do it, but you mostly need to tap on the build number in the phone details or about phone page]*
2. [After you enabled Developer Options/Settings scroll in it until you find USB Debugging and enable it]*
3. [Download the Android SDK Platform Tools package from [here](https://developer.android.com/tools/releases/platform-tools)]* and run your terminal/Command Prompt inside the folder you got after you unarchived the archived files
4. Connect your phone to your computer using an USB charger that supports transfering files; [you may need to approve using USB debugging on your computer on your phone if it is your first time doing this]*
5. Open FusionCore/HAML then run this command in your terminal/Command Prompt window on your computer: ```adb logcat --pid=$(adb shell pidof -s dev.allofus.fusioncore) > HAMLlog.txt```; note that FusionCore must be running before executing the command
6. Make your game crash and get the HAMLlog.txt file from the ADB folder
7. That's all!

To do it only with your phone you should follow these steps instead:

1. [Download from Google Play Store an app called Bugjaeger]*
2. [Open it and click on the charger with plus like button from the top-right area then click on the pair button]*
3. [Again, enable "Developer Options/Settings" on your phone; depends on phone manufacturer]*
4. Look in "Developer Options/Settings" for "Wireless Debugging" instead and enable it (you need to have an wireless connection for it to be enabled)
5. Click on the setting itself and hold then tap on "copy" button on the "IP adress and port" zone
6. [Go back in Bugjaeger and paste the numbers splited by dots before the ":" in "IP adress" and what's after the double dots in "Port"]*
7. [Back in "Wireless Debugging" page click on "Pair device with pairing code"; it will give you a code that you need to enter in the notification that Bugjaeger gave you and send it]*
8. [Back in the app exit the pairing screen]*, click again on the charger-like button and in the connect screen enter again your copied IP adress and port and click on connect; if you did everything correctly then you should see a notification from Android saying that you got connected to Wireless Debugging
9. On the main screen click on the bottom right button ("<>") and paste this command ```logcat --pid=$(pidof -s dev.allofus.fusioncore)```; note that as the computer method you need to have FusionCore running before running the command; also a small tip, before running any commands press on the X button on the bottom to clear the text so there aren't any other useless data copied in the next step
10. Press on the button next to the X to copy everything in the console and paste it in a file or send it somewhere
11. That's it!

> [!NOTE]
> "Wireless Debugging" might randomly disable itself. In that case enable it again, copy the IP adress and port again and paste it where you need it.

Backup/Restore your saves
---

Making a copy of your saves also can be done only on your phone or with a computer. This involves in bypassing the Android phone limitations from accessing the ```Android/data``` folder where is your saves stored. Without futher ado let's start with the computer method.

1. Get yourself a USB charger with file transfer capability and connect your phone to your computer
2. Allow on your phone to access your files on the computer
3. Go inside the phone storage on your computer and after you got to the root of your storage go to ```Android/data/com.abtractsoft.hybridanimals/files``` and copy everything inside or what you want to back up to a safe place(but I suggest backing up the entire folder)
4. That's it!

Using only the mobile phone it is a bit more difficult, but it is absolutely doable:

1. Download Shizuku and open it
2. [To not make this tutorial too long Shizuku offers a tutorial on how to enable it; you can find it by pressing "Step-by-step guide" and "Pairing" buttons inside the app]*
3. On the main screen press on the "Start" button and do whatever it needs you to do
4. Use an app like "File Manager" (made by "File Manager +")
or "Zarchiver" to use Shizuku to access the ```Android/data``` folder; [note that you need to allow the app to use Shizuku (by toggling it on "Autorize application" menu) in the app or in the app you are using directly (by trying to get into that folder and triggering the app to ask for permission); note that not every file manager app can use Shizuku]*
5. Go to ```Android/data/com.abtractsoft.hybridanimals/files``` and backup your saves or everything
6. That's it!

To restore your saves for both your methods, do the exact same steps, but instead of copying from the phone, paste what have you backed up already. To avoid corruption, delete everything inside first then put your backup. If in any of the methods it says that any files from the ```il2cpp``` or ```Unity``` folders couldn't be moved, deleted, copied or replaced, ignore those errors. It won't affect your backup.
# Introduction

## Installing HAML

This guide walks you through setting up HAML so you can start playing with mods or multiplayer.

> [!IMPORTANT]
> **Back up your original Hybrid Animals saves before proceeding.** Uninstalling the base game will delete your local save files.

> [!WARNING]
> * **Supported Devices:** Requires an **Android 11+ ARM64** device.
> * **Unsupported Platforms:** Android emulators and iOS devices are **not supported**. Some Android 11 devices may still fail due to vendor-specific kernel/ROM limitations.
> * **Game Version:** You must have **Hybrid Animals v200613** installed.

To begin, join the [Hybrid Animals Modding Discord Server](https://discord.gg/TYEhNVmXhw) and complete the following steps:

1. Download the latest version of FusionCore from the [#haml-updates](https://discord.com/channels/1351005866996011060/1486848511554097172) channel and install it.
2. Back up your HA saves if you haven't already (refer to the [Backup & Restore Saves](#backup--restore-your-saves) guide below).
3. Replace your current game version with **Hybrid Animals v200613** (available on third-party stores such as APKPure, Uptodown, or Aurora Store). 
   * *Tip:* Open the Hybrid Animals page on the Google Play Store, tap the three dots in the top-right corner, and uncheck **Enable auto-update**.
4. Open FusionCore and select **Hybrid Animals** from the application list.
5. If the app displays a black screen, HAML is running. Wait for the game to complete its first-time background initialization and load the main menu (this process can take up to 10 minutes).
   * If the game crashes, retrieve your [Log Output](#get-your-log-file) or use [ADB Logging](#using-adb) to submit a bug report on Discord.
6. Download the latest HAMH release from [#hamh-updates](https://discord.com/channels/1351005866996011060/1486848544521584793) and move the file to:
   ```text
   FusionCore/com.abstractsoft.hybridanimals/BepInEx/plugins/
   ```
7. Launch the game and enjoy!

---

### Installation Video Guide

<p align="center">
    <video width="340" controls>
        <source src="../resources/installation.mp4" type="video/mp4">
        Your browser does not support HTML video playback.
    </video>
</p>

---

## Get Your Log File

If you experience crashes or want to observe internal game events in real-time, inspect the output log file located at:

```text
FusionCore/com.abstractsoft.hybridanimals/BepInEx/LogOutput.log
```

---

## Using ADB

If the game crashes before the standard log file can write output, use the **Android Debug Bridge (ADB)** to capture raw device logs.

### Option A: PC Method

#### Initial Setup (Do Once)
1. Enable **Developer Options** on your Android device (typically found by tapping **Build Number** 7 times in **Settings > About Phone**).
2. In **Developer Options**, enable **USB Debugging**.
3. Download and extract the [Android SDK Platform Tools](https://developer.android.com/tools/releases/platform-tools).
4. Connect your phone to your PC via USB and approve the USB debugging prompt on your device.

#### Capturing Logs
1. Open a terminal inside the extracted `platform-tools` directory.
2. Launch FusionCore/HAML on your phone.
3. Run the following command in your PC terminal:
   ```bash
   adb logcat --pid=$(adb shell pidof -s dev.allofus.fusioncore) > HAMLlog.txt
   ```
4. Reproduce the crash. The output will save to `HAMLlog.txt` inside your platform-tools folder.

---

### Option B: Mobile-Only Method

#### Initial Setup (Do Once)
1. Install **Bugjaeger** from the Google Play Store.
2. Enable **Developer Options** in your Android settings.
3. Open Bugjaeger, tap the plug icon in the top-right corner, and tap **Pair**.
4. In Android **Developer Options**, enable **Wireless Debugging** (requires active Wi-Fi).
5. Tap **Wireless Debugging** > **Pair device with pairing code**. Enter the pairing code into Bugjaeger's notification prompt.
6. Exit the pairing screen in Bugjaeger.

#### Capturing Logs
1. Open **Wireless Debugging** in Android settings, copy the **IP Address & Port**.
2. In Bugjaeger, tap the plug icon, paste the IP Address & Port, and tap **Connect**.
3. Open FusionCore/HAML on your phone.
4. In Bugjaeger, tap the command tab (`<>`) at the bottom right and run:
   ```bash
   logcat --pid=$(pidof -s dev.allofus.fusioncore)
   ```
5. Copy the terminal output and attach it to your bug report.

> [!TIP]
> **Wireless Debugging** may automatically disable itself when Wi-Fi disconnects. Re-enable it in Android settings if connections fail.

---

## Backup & Restore Your Saves

Saves are stored inside the protected `Android/data` directory. Use one of the methods below to bypass directory restrictions.

### Option A: PC Method

1. Connect your phone to your PC via USB and select **File Transfer / MTP** mode on your device.
2. Navigate to the following directory on your PC:
   ```text
   Internal Storage/Android/data/com.abstractsoft.hybridanimals/files
   ```
3. Copy the folder contents to a safe local location.

---

### Option B: Mobile-Only Method (Shizuku)

#### Initial Setup (Do Once)
1. Download and install **Shizuku**.
2. Open Shizuku and follow the in-app pairing guide to start the Shizuku service.
3. Install a supported file manager (e.g., **ZArchiver** or **File Manager +**).
4. Authorize your file manager inside Shizuku's **Authorized Applications** menu.

#### Backing Up Saves
1. Open your authorized file manager.
2. Navigate to `Android/data/com.abstractsoft.hybridanimals/files`.
3. Copy your save files to a safe directory on your internal storage.

---

### Restoring Saves
To restore data, copy your backed-up files back into `Android/data/com.abstractsoft.hybridanimals/files`. 

> [!IMPORTANT]
> Clear existing files in the target directory prior to restoring to prevent file corruption. Ignore permission errors concerning the `il2cpp` or `Unity` system subdirectories.
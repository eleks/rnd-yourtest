# YourTest.MixedReality

## Environment configuration

* Install all needed environment: [developer.microsoft.com](https://developer.microsoft.com/en-us/windows/holographic/install_the_tools)

## Export to the Visual Studio solution

* Open project with Unity3D.
* Open File > Build Settings window.
* Click Add Open Scenes to add the scene.
* Change Platform to Universal Windows Platform and click Switch Platform.
* In Windows Store settings ensure, SDK is Universal 10.
* For Target device, leave to Any Device for occluded displays or switch to HoloLens.
* UWP Build Type should be D3D.
* UWP SDK could be left at Latest installed.
* Check Unity C# Projects under Debugging.
* Click Build.
* In the file explorer, click New Folder and name the folder "App".
* With the App folder selected, click the Select Folder button.
* When Unity is done building, a Windows File Explorer window will appear.
* Open the App folder in file explorer.
* Open the generated Visual Studio solution.

## Compile the Visual Studio solution

* Using the top toolbar in Visual Studio, change the target from Debug to Release and from ARM to X86.

## Deploy to mixed reality device over Wi-Fi

* Click on the arrow next to the Local Machine button, and change the deployment target to Remote Machine.
* Enter the IP address of your mixed reality device and change Authentication Mode to Universal (Unencrypted Protocol) for HoloLens and Windows for other devices.
* Click Debug > Start without debugging.

For HoloLens, If this is the first time deploying to your device, you will need to pair [using Visual Studio](https://docs.microsoft.com/en-us/windows/mixed-reality/using-visual-studio).

For more information see [link](https://docs.microsoft.com/en-us/windows/mixed-reality/holograms-100).

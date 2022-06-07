# Géololens

## Vidéo de présentation :

https://user-images.githubusercontent.com/37304100/172496641-d898c90d-adc0-4cc8-b128-ff425aa3887c.mp4
 
## Installation instructions 

Unity Version `2020.3.21f1 LTS`
<br>
Modules to add when installing Unity : 
- Universal Windows Platform Build Support
- Windows Build Support (IL2CPP) 

### Visual Studio
You can download it <a href="https://visualstudio.microsoft.com/fr/downloads/" target="_blank">here</a>
<br>

While installing Visual Studio, make sure to check those options :

- Development desktop C++
- Development UWP (Universal Windows Platform)
- Development Game with Unity

### MRTK Feature Tools 

After this you can download MRTK Features Tools <a href="https://www.microsoft.com/en-us/download/details.aspx?id=102778" target="_blank">here</a>

## Project Setup

### Adding MRTK Assets

Before opening the project in Unity you will have to add some `Assets` with MRTK Tool :
- Run `MRTKFeatureTool.exe`
- Click on `Start`, select the path of the project and click `Discover Features`
- In the list add `Mixed Reality OpenXR Plugin` under `Platform Support`
- And under `Mixed Reality Toolkit` add `Extensions`, `Foundations`, `Standard Assets` and `Examples` (last is optionnal)


### Build

To build the project you have to go in :
`File > Build Settings ` make sure to select `Universal Windows Platform`
Settings should be : 
- Target Device : `HoloLens`
- Architecture  : `ARM64`
- Build Type : `D3D Project`
- Target SDK Version : `Latest installed`
- Build and Run on : `USB Device`
- Build configuration : `Release`

Then you can click on `Build` and wait, it may take few minutes.

### Deploy

When build is complete, the file explorer should be open<br>
Then you can open the build file `name_of_your_project` with Visual Studio Community.<br>
On Visual Studio make sure the target is `ARM64` and `Release`, and you can deploy the solution with USB or Remote Connection

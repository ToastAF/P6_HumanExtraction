# P6_HumanExtraction

This is where the Unity part of the project resides. It includes the client and the application (UI) for 3D human generation.

## What does the project entail?

In this project, we describe the development of a user-friendly interface for generating 3D models of people based on images. The project is a collaboration with Milestone Systems, who asked us to explore methods for generating synthetic data for training machine learning models aimed at automatic anomaly detection.

Our approach was to use machine learning to extract information about a person’s clothing, skin, and hair type, as well as their respective colors. This information was then used to customize a UMA 3D model in Unity so that it resembled the person in the reference image. The result was presented in a user-friendly interface that allowed users to manually adjust the model as needed.

Through a user test and a robustness test, we concluded that the application is user-friendly but works best with images where the people are well-lit, placed in the foreground or midground, facing the camera, and of high image quality. Future iterations of the program should focus on expanding the range of clothing types, adding the ability to place the person back into the image, and optimizing the machine learning models to better handle lower-quality images.

## Showing the pipeline of the project

A high‑level diagram is available in the thesis appendix: **[Pipeline\_Final.pdf](https://github.com/user-attachments/files/20444275/MED6_gr02.-.Pipeline_Final.pdf)**.

## Scripts

The necessary scripts for the program can be found in the folder named `ScriptFinal`. This includes the Client, ColorPicker, ColorChanger, ClothesChanger, etc.

## Scene

The final application scene is located in the `Scenes` folder and is named `RonjaKopiering`.

## 📁 Project Structure - (Branch - FinalUdenRobustnessTest)

```plaintext
P6_Unity/
├── .idea/
├── .vscode/
├── Assets/
│   ├── ScriptsFinal/               # Contains all key scripts
│   │   ├── ColourPicker/
│   │   ├── ServerClient/
│   │   ├── AnimationChanger.cs
│   │   ├── ColorAndClothesChanger.cs
│   │   ├── ColorChangerPerhaps.cs
│   │   ├── DestroyChildObjects.cs
│   │   ├── ImagePicker.cs
│   │   ├── LoadingScreen.cs
│   │   ├── ServerClient.cs
│   │   ├── UI_ChangeClothes.cs
│   ├── Scenes/                     # Scene folder
│   │   ├── ScenesNoUse/
│   │   └── RonjaKopiering.unity    # Final scene file
│   ├── CustomHiders/
│  ....

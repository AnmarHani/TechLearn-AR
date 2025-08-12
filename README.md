# TechLearn-AR

Learn computer parts interactively with Augmented Reality.

This repository contains a Unity-based AR learning app (product name: TechLearnAR) that uses Vuforia Engine to recognize targets and overlay 3D content that teaches hardware components.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Demo](#demo)
- [Tech Stack](#tech-stack)
- [Project Requirements](#project-requirements)
- [Getting Started](#getting-started)
  - [Clone and Open](#clone-and-open)
  - [Install/Verify Vuforia](#installverify-vuforia)
  - [Add License Key](#add-license-key)
- [Run](#build-and-run)
  - [Android](#android)
- [Usage](#usage)
- [Acknowledgments](#acknowledgments)

## Overview
TechLearn-AR helps learners identify and understand computer hardware using AR. Point your device at supported targets to view 3D models, names, and descriptions of parts such as the CPU, GPU, RAM, and storage.


## Features
- Image target recognition and tracking using Vuforia Engine
- Interactive 3D visualizations of computer components
- On-screen guidance and UI built with Unity UI
- Support for Android devices
- Mobile-optimized shaders and assets (C#, ShaderLab, HLSL)

## Demo


https://github.com/user-attachments/assets/32bf0505-1398-45ba-ba55-7a7d845d5041



## Tech Stack
- Game engine: Unity 6000.1.13f1
- AR SDK: Vuforia Engine 11.3.4
- Unity packages (highlights):
  - com.unity.xr.arcore 6.1.1
  - com.unity.postprocessing 3.5.0
  - com.unity.ugui 2.0.0
  - com.unity.inputsystem 1.14.0
- Languages: C#, ShaderLab, HLSL


## Project Requirements
- Hardware: AR-capable Android device
- OS:
  - Android minimum SDK currently set to 34 (Android 14)
- Tools:
  - Unity Editor 6000.1.13f1 (open the project with this version for best compatibility)
  - Android: Android Studio/SDK Platform 34+, USB debugging
- Vuforia Developer account (for a License Key)

## Getting Started

### Clone and Open
```bash
git clone https://github.com/mqo5/TechLearn-AR.git
cd TechLearn-AR
```
- Open the project with Unity Hub using Unity 6000.1.13f1.

### Install/Verify Vuforia
Vuforia is referenced via a local tarball in Packages/manifest.json:
```
"com.ptc.vuforia.engine": "file:com.ptc.vuforia.engine-11.3.4.tgz"
```
- If Unity cannot resolve this dependency, download the matching Vuforia package (11.3.4) from the Vuforia Developer Portal and place the tarball at the project root or update the path accordingly.
- After opening the project, Unity should import Vuforia automatically.

### Add License Key
You must add your Vuforia License Key:
- Create or retrieve a License Key from the Vuforia Developer Portal.
- In Unity: Edit > Project Settings > Vuforia Engine
  - Paste your App License Key.
- Alternatively, update the Vuforia configuration asset (typically under Assets/Resources/VuforiaConfiguration.asset) if present.

## Run

### Android
There is already a built apk for android devices in the releases.

## Usage
- Launch the app and grant camera permissions.
- Point the camera at gaming laptop (prefer MSI) to recognize components.
- Use on-screen controls to navigate through content.

Tips:
- Good lighting and steady framing improve target detection.

## Acknowledgments
- Vuforia Engine
- Unity community packages and samples

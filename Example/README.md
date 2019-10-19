# Introduction

This folder contains an example of how you can protect your SDK and how your customers (developers using the SDK). Below is a description of the folders:

## Overview
* **SDKExample** - This is a sample SDK (targeting .NET Standard 2.0) that we want to protect. Inside `SDKExample`, you can find the solution file that will also load `SoftwareUsingSDK` project.
* **SoftwareUsingSDK** - This project (targeting .NET Core 2.2) will call methods from `SDKExample`. It's important to sign this assembly, which we cover later.
* **AssemblySigner** - This project contains the Assembly Signer command line util with a demo `build.json` file.

## Getting started
To test the 

1. Go to `SDKExample` folder and open `SDKExample.sln` in [Visual Studio](https://visualstudio.microsoft.com/vs/community/).
2. Go to `AssemblySigner` folder and run the command below: 
```
dotnet AssemblySigner.dll ..\SoftwareUsingSDK\bin\Debug\netcoreapp2.2\build.json
```
3. Compile and run the `SoftwareUsingSDK` project.

## Questions
If you have any questions, please reach out to us at support@cryptolens.io!
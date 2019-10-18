# For developers

## Idea
To be able to use the SDK in your projects, you need to obtain a signed certificate for each assembly that will call the SDK.

## Getting started
### Assemblies that need to be signed
The first assembly that will call the SDK needs to be signed. For example, if you add a wrapper around the SDK and then call the wrapper library from another application, you need to sign the application only. The wrapper only needs to be signed if it will also call the SDK on its own. 
![](/Images/sdk-what-to-sign.png)

### Signing the assembly
To create a signed certificate of an assembly, you can either use a command-line interface or the GUI application. THese are provided by the developer of the SDK.

#### Using the GUI
The simplest way to sign an assembly is by using the GUI. You just need to provide the license key and the path to the assembly.

![](/Images/sdk-sign-gui.png)

### Using the command line
The command line is great if you would like to automate signing of assemblies. To do that, you can create a `build.json` file in the same folder as the command line interface (i.e. `AssemblySigner.dll`). If `build.json` is located in a different folder, you can specify the path as an argument when calling `AssemblySinger.dll`.

The structure of the ``build.json` is shown below:

```
{
    "Assemblies": ["C:\\Users\\User\\Documents\\GitHub\\sdk-licensing\\Old\\SoftwareUsingSDK\\bin\\Debug\\netcoreapp2.2\\SoftwareUsingSDK.dll"],
    "Key" : "KOAQP-OWMXF-UVGBX-DUYGW"
}
```
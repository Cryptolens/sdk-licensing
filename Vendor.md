# For vendors

## Idea

> Before you start, please review the [SDK licensing tutorial](https://help.cryptolens.io/licensing-models/sdk-licensing).

The idea behind the SDK protection technique is that developers need to get a signed
certificate for each assembly that will use your SDK. To facilitate that, we have developed Assembly Signer (command-line and GUI), an application that does it automatically. 

Starting from [Cryptolens.Licensing](https://github.com/cryptolens/cryptolens-dotnet) v4.0.21, there is also a new method that you can call to verify this certificate.

The *certificate* is just the response from a `Key.Activate` call, so you can extract the original license key and check things like features, when it expires, etc.

![](/Images/sdk-ex.png)

## Getting started

Two steps are necessary to protect your SDK. First, we need to configure the Assembly Signer so that you customers (i.e. the developers) can use it to sign their applications (that use the SDK). Secondly, we need to verify the signature inside in the SDK to make sure only developers that have paid for it can access its methods.

### Configuring Assembly Signer
The Assembly Signer is available as pre-built binaries [here](https://github.com/Cryptolens/sdk-licensing/releases). You can also compile it from source. To set up the Assembly Signer, you need to create a `config.json` file in the same folder as the executable (either the command-line version or the GUI). It will look similar to the one below:

```
{
    "ProductId": 1234,
    "ActivateToken": "Activate token. Consider feature masking.",
    "DataObjectToken" : "AddDataObject and SetStringValue permissions. Set KeyLock = -1."
}
```

When creating the [access tokens](https://app.cryptolens.io/User/AccessToken#/), it is recommended to:

* For `ActivateToken`, we recommend to consider using [feature masking](https://help.cryptolens.io/licensing-models/sdk-licensing#privacy) for sensitive fields.
* For `DataObjectToken`, we strongly recommend to set `KeyLock=-1`. Otherwise, an adversary can see all your data objects, some of which can contain sensitive information.

> **Note**: please create a separate access token for `ActivateToken` and `DataObjectToken`.

### Verifying the entry assembly
To verify that the assembly has been signed properly, you can call the [VerifySDKLicenseCertificate()](https://help.cryptolens.io/api/dotnet/api/SKM.V3.Methods.Helpers.html#SKM_V3_Methods_Helpers_VerifySDKLicenseCertificate_System_String_) method with your RSA Public Key, as shown below.

```cs
string RSA = "RSA Pub key";
if(Helpers.VerifySDKLicenseCertificate(RSA) != null)
{
    // all ok.
}
else 
{
    // signature incorrect.
}
```

The [VerifySDKLicenseCertificate()](https://help.cryptolens.io/api/dotnet/api/SKM.V3.Methods.Helpers.html#SKM_V3_Methods_Helpers_VerifySDKLicenseCertificate_System_String_) method will return a [LicenseKey](https://help.cryptolens.io/api/dotnet/api/SKM.V3.LicenseKey.html) object if the verification succeeded or null otherwise. The license key object is especially useful if you offer different features inside your SDK. In some cases, you might want to check the license status with the server, in which case you can use the license key object to extract the license key string.

If developers would get problems with the verification, i.e. that `VerifySDKLicenseCertificate` does not find a valid certificate, you can check with them which assembly they sign. `VerifySDKLicenseCertificate` will validate the certificate of the entry assembly, i.e. the first assembly that triggered the call to a method in your SDK. In this way, developers cannot create another SDK around yours to bypass verification.
# For vendors

## Idea

> Before you start, please review the [SDK licensing tutorial](https://help.cryptolens.io/licensing-models/sdk-licensing).

The idea behind the SDK protection technique is that developers need to get a signed
certificate for each assembly that will use your SDK. To facilitate that, we have developed Assembly Signer (command-line and GUI), an application that does it automatically. 

Starting from [Cryptolens.Licensing](https://github.com/cryptolens/cryptolens-dotnet) v4.0.21, there is also a new method that you can call to verify this certificate.

The *certificate* is just the response from a `Key.Activate` call, so you can extract the original license key and check things like features, when it expires, etc.

## Getting started

### Configuring Assembly Signer
To set up the Assembly Signer, you need to create a `config.json` file in the same folder as the executable (either the command-line version or the GUI). It will look similar to the one below:

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

### Verifying authenticity 
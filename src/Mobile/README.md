# YourTest.Mobile

Xamarin.Forms application. Used for testing user by set of questions retrieved from bak-end.

## Pre-Requirements

- For Security and quick configuration used `Mobile.BuildTools` _[github](https://github.com/dansiegel/Mobile.BuildTools)_. Perform `configuration` before __`first build`__
- Configured Azure Active Directory B2C
- Deployed and configured REST api on Azure with AAD B2C
- Use `rorub2c://com.eleks.YourTest` as custom Redirect URI

## Build

### Local Configuration

```bash

# Navigate to Core Project dir
cd src/Mobile/YourTest/YourTest/

# Create secrets.json file
echo "{
    \"B2cClientId\" : \"[Azure B2C native Application ID]\",
    \"B2cRedirectUri\" : \"rorub2c://com.eleks.YourTest\",
    \"B2cAuthority\" : \"https://login.microsoftonline.com/tfp/{B2C tenant Name or GUID}/{Policy ID}/v2.0/\",
    \"B2cScopes\" : \"[Scopes for REST API]\",
    \"RestEndpoint\": \"[Url of hosted back-end]\"
}" > secrets.json

```

`secrets.json` ignored by source control. On each new project folder you need to create it.

`rorub2c://com.eleks.YourTest` - register for Azure AD B2C native client.

### Build Server Configuration

Add next Environment Variables:

|Env Name|Value|
|---|---|
|Secret_B2cClientId|[Azure Application ID]|
|Secret_B2cRedirectUri|rorub2c://com.eleks.YourTest|
|Secret_B2cAuthority|https://login.microsoftonline.com/tfp/{B2C tenant Name or GUID}/{Policy ID}/v2.0/|
|Secret_B2cScopes|[Scopes for REST API]|
|Secret_RestEndpoint|[Url of hosted back-end]|

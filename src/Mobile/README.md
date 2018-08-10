# YourTest.Mobile

TODO small description ...

## Pre-Requirements

For Security and quick configuration used `Mobile.BuildTools` _[github](https://github.com/dansiegel/Mobile.BuildTools)_. Perform `configuration` before __`first build`__.

### Local Configuration

```bash

# Navigate to Core Project dir
cd src/Mobile/YourTest/YourTest/

# Create secrets.json file
echo "{
    \"AzureClientId\" : \"[Azure Application ID]\",
    \"AzureRedirectUri\" : \"[Azure Application Redirect Uri]\"
}" > secrets.json

```

`secrets.json` ignored by source control. On each new project folder you need to create it.

### Build Server Configuration

Add next Environment Variables:
|Env Name|Value|
|---|---|
|Secret_AzureClientId|[Azure Application ID]|
|Secret_AzureRedirectUri|[Azure Application Redirect Uri]|

# YourTest.REST

This is REST api for for YourTest project. It build using Azure Functions.

## Pre-requirements

1. [Visual Studio 2017](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#prerequisites)
 or [Visual Studio for Mac](https://docs.microsoft.com/en-us/visualstudio/mac/azure-functions#requirements)
2. [Azure Functions Core Tools v1.x](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local#install-the-azure-functions-core-tools)

## Build

Run on local machine

```bash

cd src/Backend/YourTest.Rest

func host start --build=true

```

## Publish to Azure

```bash

# login to azure portal if not logged
func azure login

cd src/Backend/YourTest.Rest

func azure functionapp publish <ExistingAzureFunctionAppName>

```
[Azure Docs](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
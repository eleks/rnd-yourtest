# YourTest.REST

This is REST api for for YourTest project. It build using Azure Functions.

## Pre-requirements

1. [Visual Studio 2017](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#prerequisites)
 or [Visual Studio for Mac](https://docs.microsoft.com/en-us/visualstudio/mac/azure-functions#requirements)
2. [Azure Functions Core Tools v1.x and v2.x](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local#install-the-azure-functions-core-tools)

## Build

Run on local machine in terminal for consumption purposes.

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

## Test data managing

Test information stored on backend in `DEPLOYMENT_DIR/Store/tests.json`.

For editing this file connect server over **ftp**. This data can be collected for function app  `Platform Settings > Deployment Center > FTP`.

File scheme:

```json
[
    {
        // Ensure unique id
        "id": 3,
        // Test name
        "name": "Jet engine test",
        // Percent to pass the test CorrectAnswersCount/TestQuestionCount
        "persentPassageThreshold": 0.60,
        // Questions set
        "questions": [
            {
                // Question text
                "description": "Jet engines are a type of reaction engine, meaning they rely heavily on which of Newton’s laws of motion?",
                // Possible answers no count limits
                "possibleAnswers": [
                    "Newton’s first law",
                    "Newton’s second law",
                    "Newton’s third law"

                ],
                // Correct answer
                // Must be identical in possible answers
                "answer": "Newton’s third law",
                // Question type
                //  0 - Text
                //  1 - Mixed Reality
                "type": 0,
                // Unique id in test scope
                "id": 1
            },
            
            /* ... */
        ]
    },

    /* ... */
]
```
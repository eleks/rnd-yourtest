# YourTest

![yourtest](img/ic_logo_big_round.png)

Nowadays exist many testing systems, such as Moodle, and problem is that it is not possible to test a specialist in a practice, so with the help of the augmented reality we can test a person in real-time with a hands-free approach and simulate situations from real life. And authorization via smartphone.

## Tech Stack

- Unity (for HoloLens)
- Azure Functions (Back-end)
- Azure Active Directory B2C (Auth provider)
- Xamarin.Forms (Mobile Client)

## Pre-requirements

- [Configure Azure Environment](#Azure-Active-Directory-B2C-Auth-configuration)

## Build

Solution consist from 3 components:

1. `src/Backend` Azure Functions project. Used as esy development and deploy process. Read details: _[src/Backend/README.md](src/Backend/README.md)_
2. `src/MixedReality` Unity project for HoloLens. Read details: _[src/MixedReality/README.md](src/MixedReality/README.md)_
3. `src/Mobile` Xamarin.Forms project. Read details: _[src/Mobile/README.md](src/Mobile/README.md)_

---

## Azure Active Directory B2C Environment configuration

### [0. Install tools for pre-requirement section of back-end](src/Backend/README.md#Pre-requirements)

### 1. Create Resource Group

```bash

ad group create --name "YOUR_GROUP_NAME" --location "North Europe"

```

### 2. Azure AD B2C

1. [Create Azure AD B2C Tenant](https://docs.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-tenant#create-an-azure-ad-b2c-tenant)
2. [Link created AD to current subscription and select resource group create in previous step](https://docs.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-tenant#link-your-tenant-to-your-subscription)

### 3. Register b2c Web app

1. [Register Web app](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-app-registration#register-a-web-api)
2. In redirect url set `https://YOR_PLANED_SITE_NAME.azurewebsites.net/.auth/login/aad/callback`
3. In APP ID URI add input `yourtest`
4. In Public Scopes add `read` scope [docs](https://azure.microsoft.com/en-us/blog/azure-ad-b2c-access-tokens-now-in-public-preview/)
5. Store `ClientId` for further usage
6. Create Sign-up policy in B2C [step 3. in article](https://blogs.msdn.microsoft.com/hmahrt/2017/03/07/azure-active-directory-b2c-and-azure-functions/) and Store `Issuer` url

### 4. Register b2c Native App

1. [Register native app](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-app-registration#register-a-mobile-or-native-app)
2. In Custom Redirect URL set: `rorub2c://com.eleks.YourTest` from [Mobile Pre-requirements](src/Mobile/README.md#Pre-requirements)
3. In Api access add Web App from previous section with all scopes
4. Store `ClientId` for configuring mobile client

### 5. Run automated script

```bash

cd src/Backend/environment

sh az_environment_setup.sh "YOUR_GROUP_NAME" \
    "SITE_NAME" \
    "WebClientID" \
    "B2C_Issuer_URL_From_3_Section"

```

### Manual Azure Functions App set up

1. [Create Azure Function App](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-function-app-portal)
2. [Configure Function App Auth with AD B2C](https://blogs.msdn.microsoft.com/hmahrt/2017/03/07/azure-active-directory-b2c-and-azure-functions/)
3. [Deploy YourTest.REST](src/Backend/README.md#Publish-to-Azure)
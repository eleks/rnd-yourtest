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

### Azure AD B2C

1. [Create Azure AD B2C Tenant](https://docs.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-tenant#create-an-azure-ad-b2c-tenant)
2. [Add subscription to created Tenant](https://docs.microsoft.com/en-us/azure/active-directory/fundamentals/active-directory-how-subscriptions-associated-directory#to-associate-an-existing-subscription-to-your-azure-ad-directory)

### Azure Functions App

1. [Create Azure Function App](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-function-app-portal)
2. [Configure Function App Auth with AD B2C](https://blogs.msdn.microsoft.com/hmahrt/2017/03/07/azure-active-directory-b2c-and-azure-functions/)
3. [Deploy YourTest.REST](src/Backend/README.md#Publish-to-Azure)

### Azure Mobile Client

1. [Register Mobile App](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-app-registration#register-a-mobile-or-native-app)
2. Set as Custom Redirect URI: `rorub2c://com.eleks.YourTest` from [Mobile Pre-requirements](src/Mobile/README.md#Pre-requirements)
3. [Configure Mobile clint for build](src/Mobile/README.md#Build)
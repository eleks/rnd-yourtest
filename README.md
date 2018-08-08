# YourTest

![yourtest](img/yourtest.png)

Nowadays exist many testing systems, such as Moodle, and problem is that it is not possible to test a specialist in a practice, so with the help of the augmented reality we can test a person in real-time with a hands-free approach and simulate situations from real life. And authorization via smartphone.

## Tech Stack

- Unity (for HoloLens)
- Azure Functions (Back-end)
- Xamarin.Forms (Mobile Client)

## Build

Solution consist from 3 components:

1. `src/Backend` Azure Functions project. Used as esy development and deploy process. Read details: _[src/Backend/README.md](src/Backend/README.md)_
2. `src/MixedReality` Unity project for HoloLens. Read details: _[src/MixedReality/README.md](src/MixedReality/README.md)_
3. `src/Mobile` Xamarin.Forms project. Read details: _[src/Mobile/README.md](src/Mobile/README.md)_
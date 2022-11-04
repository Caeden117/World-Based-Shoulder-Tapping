# World-Based Shoulder Tapping

A VRChat package/prefab that allows players to "tap" each others shoulders and get each other's attention.

This package was made for the [Helping Hands](https://discord.gg/helpinghands) community.

## Features

- Tapping user's left or right shoulder.
- HUD notification showing which shoulder was tapped, and who tapped it.
- Toggle your ability to tap shoulders
- Toggle others' ability to tap your shoulders

## Installation

### Dependencies

*These MUST be installed before importing this package.*

- [UdonSharp](https://github.com/vrchat-community/UdonSharp/)
- [CyanPlayerObjectPool](https://github.com/CyanLaser/CyanPlayerObjectPool)

### Setup

0) Install the project dependencies.
1) Ensure you have a `PlayerObjectPool` prefab in your scene, found in `Assets/Cyan/PlayerObjectPool`.
2) Download and install this package from [Releases](https://github.com/Caeden117/World-Based-Shoulder-Tapping/releases).
3) Drag and drop the main prefab found in `Assets/World-Based Shoulder Tapping/Prefabs` into your scene.

Assuming these steps were done correctly, shoulder tapping should now be working in your world.

See the example scene in `Assets/World-Based Shoulder Tapping/Scenes` for more details on proper setup.

## UI

The package comes with a standalone world-space UI that you can move to fit in your world.

You will have to create extra UI on your own, following this standalone/example UI.
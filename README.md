# World-Based Shoulder Tapping

A VRChat package/prefab that allows players to "tap" each others shoulders and get each other's attention.

## Why?

This package was made for the [Helping Hands](https://discord.gg/helpinghands) community.

In (American) Deaf culture, tapping the shoulders is seen as a friendly way to get your attention. But hardware and software has not progressed enough to make this possible in VRChat.

Some people, notibly [hppedeaf](https://hppedeaf.booth.pm/items/3851679), have made shoulder tapping systems for VRChat's Avatar SDK. This is great, however this system must be added to each avatar individually.

My solution uses the World SDK. That means shoulder tapping will be available for every user in your world, provided you install this package.

## Features

- Tapping user's left or right shoulder.
- HUD notification showing which shoulder was tapped, and who tapped it.
- Toggle your ability to tap shoulders
- Toggle others' ability to tap your shoulders

## Installation

### Dependencies

*These MUST be installed into your World before installing this package.*

- [UdonSharp](https://github.com/vrchat-community/UdonSharp/)
- [CyanPlayerObjectPool](https://github.com/CyanLaser/CyanPlayerObjectPool)

### Setup

0) Install the project dependencies.
1) Ensure you have a `PlayerObjectPool` prefab in your scene. This is added by CyanPlayerObjectPool.
2) Download and install this package from [Releases](https://github.com/Caeden117/World-Based-Shoulder-Tapping/releases).
3) Drag and drop the main prefab found in `Assets/World-Based Shoulder Tapping/Prefabs` into your scene.

Assuming these steps were done correctly, shoulder tapping should now be working in your world.

See the example scene in `Assets/World-Based Shoulder Tapping/Scenes` for more details on proper setup.
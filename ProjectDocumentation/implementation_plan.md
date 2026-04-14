# Menu UI and Audio Control Implementation Plan

This plan outlines the implementation of the UI menu functionality, including scene navigation, social media links, and global audio control as requested.

## User Review Required

> [!IMPORTANT]
> - I am assuming the main game scene is located at `Assets/Scenes/Levels/map.unity`. If the scene name is different, the `PlayGame` function will need to be updated.
> - The volume control will use `AudioListener.volume` to affect all game sounds globally.

## Proposed Changes

### UI & Audio Components

#### [NEW] [MenuController.cs](file:///d:/gamerac/Monster/Assets/Scripts/UI/MenuController.cs)
A new script to handle all menu interactions.

- **Scene Navigation**: `PlayGame()` will load the game level ("map").
- **Settings Panel**: `OpenSettings()` and `CloseSettings()` will toggle panel visibility with smooth activation.
- **Social Media**: `OpenDiscord()` and `OpenFacebook()` will use the provided URLs.
- **Audio Control**: `SetVolume(float value)` will update `AudioListener.volume`. 
- **Persistence**: Save and Load the volume setting using `PlayerPrefs` so the player's preference is remembered.
- **Menu Button**: `OpenLevelSelect()` placeholder for the list icon button.

## Open Questions

- Should the volume setting be saved (using `PlayerPrefs`) so it persists when the game is restarted?
- Does the "Menu" button (the one with the list icon) have a specific function other than showing general info or levels? For now, I'll focus on the Play, Settings, and Social functionality.

## Verification Plan

### Automated Tests
- I will perform a code review to ensure the logic for `Application.OpenURL` and `AudioListener.volume` is correct.
- I will verify the scene loading logic against the existing project structure.

### Manual Verification
- After attaching the script, the user should:
  1. Click the Play button to ensure it loads the Map scene.
  2. Test the Discord and Facebook buttons to see if they open the browser.
  3. Adjust the slider and check if the game audio volume changes.

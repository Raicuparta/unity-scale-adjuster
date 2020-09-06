# Unity Scale Adjuster

A universal mod for Unity games that lets you change the scale of the world / camera. Useful for VR games where the scale feels off, and the game doesn't offer options to adjust it.

## Installing the mod

1. [Install MelonLoader by following the instructions on their page](https://melonwiki.xyz/#/?id=requirements);
2. [Download the .dll of the latest release of this mod](https://github.com/Raicuparta/unity-scale-adjuster/releases/latest);
3. Copy `ScaleAdjuster.dll` to `[Your Game's Directory]/Mods`;
4. Example `C:\Program Files (x86)\Steam\steamapps\common\Overload\Mods\ScaleAdjuster.dll`
5. A MelonLoader logs window should open together with your game. If the name of this mod is there (`UnityScaleAdjuster`) with no extra errors, then the mod has been installed successfully.

## Changing the world scale in-game

1. Start the game until you're in-game, seeing the world that you want to scale;
2. Use keyboard keys **F4** and **F3** to scale the world up and down respectively.

## Saving your preferred scale

1. Notice that when you press **F3** and **F4** to change the world scale, a message shows in MelonLoader logs window saying `[UnityScaleAdjuster] Change camera scale to XXX`, where `XXX` is a number like `0.9`, `1.542`, etc.
2. Change the world scale to something you're satisfied with;
3. Copy the last scale number that was logged;
4. Add that number to the game launch parameter `-cameraScale`. Two ways to do this:
   * If you have the game on Steam:
      * Right click the game on your library;
      * Select "Properties";
      * In the "General" tab, select "Set Launch Options...";
      * In the text field add `-cameraScale XXX`, where `XXX` is the scale value from before.
   * If you don't have the game on Steam:
      * Create a shortcut to the game's exe (or use an existing shortcut);
      * Right click the shortcut;
      * Select "Properties";
      * Add `-cameraScale XXX` after the path in the "Target" field, where `XXX` is the scale value from before;
      * Example target value: `"C:\Program Files (x86)\Steam\steamapps\common\Overload\Overload.exe" -cameraScale 1.2`
5. The mod will try to set your preferred scale automatically whenever a new level / scene loads;
6. If this doesn't work or if the game resets the scale, press **F5** to manually set the scale to value in `-cameraScale` again.

## Keys

Already mentioned in the instructions above, but to summarize:

* **F3** scales the world down;
* **F4** scales the world up;
* **F5** sets the scale to the user defined scale in `-cameraScale` launch parameter;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public SerializableDictionary<BuildingType, ToolbarButton> buildingButtons;
    public BuildingPlacer buildingPlacer;

    private void Start()
    {
        EnableCurrentlyUnlockedBuildings();
    }

    public void EnableCurrentlyUnlockedBuildings()
    {
        HashSet<BuildingType> unlocked = FactoryManager.GetCurrentlyUnlockedBuildings();
        // Set each button to be unlocked (clickable and visible) only if it should be
        foreach (ToolbarButton button in buildingButtons.dict.Values)
            button.SetUnlockState(unlocked.Contains(button.buildingType));
    }

    public void BuildingButtonPressed(BuildingType buildingType)
    {
        buildingPlacer.StartPlacement(buildingType);
    }
}

/*

Toolbar.cs
- Shows building icons for players to click to place buildings
- More buildings appear as they are unlocked
- Will have another small script that handles clicks and sends that info to this script
- Pseudocode:
  - Variables: buildingButtons, buildingPlacer
  - Start => EnableCurrentlyUnlockedBuildings()
  - fn EnableCurrentlyUnlockedBuildings() => turn on the buttons that are unlocked
  - fn BuildingButtonPressed(buildingType) => call buildingPlacer.StartPlacement(buildingType)

*/

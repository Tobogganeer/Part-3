using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public List<ToolbarButton> buildingButtons;
    public BuildingPlacer buildingPlacer;

    readonly KeyCode[] NumKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7 };

    private void Start()
    {
        EnableCurrentlyUnlockedBuildings();
    }

    public void EnableCurrentlyUnlockedBuildings()
    {
        HashSet<BuildingType> unlocked = FactoryManager.GetCurrentlyUnlockedBuildings();
        // Set each button to be unlocked (clickable and visible) only if it should be
        foreach (ToolbarButton button in buildingButtons)
            button.SetUnlockState(unlocked.Contains(button.buildingType));
    }

    public void BuildingButtonPressed(BuildingType buildingType)
    {
        buildingPlacer.StartPlacement(buildingType);
    }

    private void Update()
    {
        // Allow player to press number keys to select buildings as well
        for (int i = 0; i < NumKeys.Length; i++)
        {
            if (Input.GetKeyDown(NumKeys[i]))
                buildingButtons[i].OnClicked();
        }
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

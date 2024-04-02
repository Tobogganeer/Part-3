using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void StartPlacement(BuildingType buildingType)
    {

    }
}

/*

BuildingPlacer.cs
- Code used to handle the placement of buildings
- Shows the "ghost" of the building to be placed
- Verifies that there is room for the building
- Pseudocode:
  - Variables: currentGhost
  - fn StartPlacement(buildingType) => set currentGhost to building prefab from FactoryManager dict
  - Update => if ghost != null: move ghost to mouse pos, check for left click (TryPlaceGhost()), check for right click (destroy ghost/cancel)
  - fn TryPlaceGhost() => Check World.CanPlaceBuilding, then call World.PlaceBuilding if it is possible

*/

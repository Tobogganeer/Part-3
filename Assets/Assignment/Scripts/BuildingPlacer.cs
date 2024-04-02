using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;

    FactoryBuilding currentGhost;
    Camera mainCam;

    Vector2 CursorPosition => mainCam.ScreenToWorldPoint(Input.mousePosition);

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void StartPlacement(BuildingType buildingType)
    {
        // If we already have a building we are placing, destroy it instead
        // (The player clicked on the toolbar with a building in their hand, they want to put it back)
        if (currentGhost != null)
            CancelPlacement();
        else
        {
            // Spawn and initialize the building
            currentGhost = Instantiate(currentGhost, CursorPosition, Quaternion.identity).GetComponent<FactoryBuilding>();
            currentGhost.Init(buildingType);
        }
    }

    public void CancelPlacement()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost.gameObject);
            currentGhost = null;
        }
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

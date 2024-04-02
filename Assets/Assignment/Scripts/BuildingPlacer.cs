using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;

    FactoryBuilding currentGhost;
    Camera mainCam;
    int framesSinceStartedPlacement;

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
            currentGhost = Instantiate(buildingPrefab, CursorPosition, Quaternion.identity).GetComponent<FactoryBuilding>();
            currentGhost.Init(buildingType);
            framesSinceStartedPlacement = 0;
        }
    }

    private void Update()
    {
        framesSinceStartedPlacement++;

        // Only update the ghost if it's been more than a few frames
        if (currentGhost == null || framesSinceStartedPlacement < 5)
            return;

        // Place the building on left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (World.PlaceBuilding(currentGhost))
            {
                currentGhost = null; // We did it, huzzah
                return;
            }
        }
        // ... and cancel placement on right click
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CancelPlacement();
            return;
        }

        // Rotate the building with R (anti-clockwise if holding shift)
        if (Input.GetKeyDown(KeyCode.R))
            currentGhost.SetRotation(Input.GetKey(KeyCode.LeftShift) ?
                currentGhost.Rotation.RotateLeft() : currentGhost.Rotation.RotateRight());

        // Make it follow the mouse
        currentGhost.SetPosition(World.WorldToGridPosition(CursorPosition));
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

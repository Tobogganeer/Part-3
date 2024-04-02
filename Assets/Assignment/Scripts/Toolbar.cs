using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void BuildingButtonPressed(BuildingType buildingType)
    {

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

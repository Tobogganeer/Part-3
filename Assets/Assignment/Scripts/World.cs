using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

/*

World.cs
- Stores the state of the world (placed buildings, where resources are, etc)
- Creates the WorldTile objects
- No other components - empty GameObject
- Pseudocode:
  - Variables: worldTilePrefab, worldTileSize, worldSize (vec2int), resourceLocations, worldTiles, buildings, tileToBuilding
  - Start => create world tiles, store in worldTiles dict
  - fn CanPlaceBuilding(position, building) => returns true if the necessary tiles are clear
  - fn PlaceBuilding(position, building) => checks if it can be built, adds building to list, adds building tiles to tileToBuilding dict, sets building position and sets it as active
  - fn RemoveBuilding(building) => removes building and tiles from list/dict, destroys object

*/

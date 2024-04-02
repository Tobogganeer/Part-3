using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject worldTilePrefab;
    public float worldTileSize; // Units
    public Vector2Int worldSize; // Tiles
    public SerializableDictionary<Vector2Int, ProductID> resourceLocations;

    // All tiles in the world
    Dictionary<Vector2Int, WorldTile> worldTiles = new Dictionary<Vector2Int, WorldTile>();
    // All buildings in the world
    Dictionary<Vector2Int, FactoryBuilding> buildings = new Dictionary<Vector2Int, FactoryBuilding>();
    // For multi-tile buildings - store what buildings occupy each world tile
    Dictionary<WorldTile, FactoryBuilding> tileToBuilding = new Dictionary<WorldTile, FactoryBuilding>();
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

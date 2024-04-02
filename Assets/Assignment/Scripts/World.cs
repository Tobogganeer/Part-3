using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private static World instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject worldTilePrefab;
    public float worldTileSize; // Units
    public Vector2Int worldSize; // Tiles
    public Vector2 worldPositionOffset; // Units
    public SerializableDictionary<Vector2Int, ProductID> resourceLocations;

    // All tiles in the world
    Dictionary<Vector2Int, WorldTile> worldTiles = new Dictionary<Vector2Int, WorldTile>();
    // All buildings in the world
    Dictionary<Vector2Int, FactoryBuilding> buildings = new Dictionary<Vector2Int, FactoryBuilding>();
    // For multi-tile buildings - store what buildings occupy each world tile
    Dictionary<WorldTile, FactoryBuilding> tileToBuilding = new Dictionary<WorldTile, FactoryBuilding>();

    private void Start()
    {
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                // Did we specify there being a resource to mine here?
                bool actualResourceHere = resourceLocations.dict.ContainsKey(position);
                // Fetch the resource (if it exists)
                ProductID product = actualResourceHere ? resourceLocations.dict[position] : ProductID.None;
                
                // Spawn the tile as a child of us
                SpawnTile(position, product, transform);
            }
        }
    }

    void SpawnTile(Vector2Int gridPosition, ProductID product, Transform holder)
    {
        // Convert the grid coordinate to actual world space
        Vector2 tileWorldPosition = (Vector2)gridPosition * worldTileSize + GetWorldOffset();
        // Spawn and scale the tile
        GameObject spawnedTile = Instantiate(worldTilePrefab, tileWorldPosition, Quaternion.identity, holder);
        spawnedTile.transform.localScale = Vector3.one * worldTileSize;
        // Tell the tile what and where it is
        WorldTile tile = spawnedTile.GetComponent<WorldTile>();
        tile.Init(product, gridPosition);
        worldTiles.Add(gridPosition, tile);
    }

    Vector2 GetWorldOffset()
    {
        // Center the world and then add the world offset
        Vector2 halfWorldSize = (Vector2)worldSize / 2f * worldTileSize;
        Vector2 halfTileSize = Vector2.one * worldTileSize / 2f;
        return worldPositionOffset - halfWorldSize + halfTileSize;
    }

    public static bool CanPlaceBuilding(Vector2Int gridPosition, FactoryBuilding building)
    {
        // TODO: Check building's tiles
        throw new System.NotImplementedException();
    }

    public static void PlaceBuilding(Vector2Int gridPosition, FactoryBuilding building)
    {
        // TODO: Place building
        throw new System.NotImplementedException();
    }

    public static void RemoveBuilding(FactoryBuilding building)
    {
        // TODO: Remove building and tiles
        throw new System.NotImplementedException();
    }


    private void OnDrawGizmos()
    {
        // Code essentially stolen from Start

        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                // Did we specify there being a resource to mine here?
                bool actualResourceHere = resourceLocations.dict.ContainsKey(position);
                // Fetch the resource (if it exists)
                ProductID product = actualResourceHere ? resourceLocations.dict[position] : ProductID.None;

                Gizmos.color = GetProductColour(product);
                Vector2 tileWorldPosition = (Vector2)position * worldTileSize + GetWorldOffset();
                Gizmos.DrawWireCube(tileWorldPosition, Vector2.one * (worldTileSize * 0.95f));
            }
        }
    }

    Color GetProductColour(ProductID product) => product switch
    {
        ProductID.Water => Color.blue,
        ProductID.Wheat => Color.yellow,
        ProductID.Tomato => Color.red,
        ProductID.Can => Color.grey,
        _ => Color.green // Default colour
    };
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

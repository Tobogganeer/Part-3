using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    List<FactoryBuilding> buildings = new List<FactoryBuilding>();
    // For multi-tile buildings - store what buildings occupy each world tile
    Dictionary<Vector2Int, FactoryBuilding> tileToBuilding = new Dictionary<Vector2Int, FactoryBuilding>();

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
        Vector2 tileWorldPosition = _GridToWorldPosition(gridPosition);
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

    /// <summary>
    /// Returns true if the <paramref name="building"/> is in a valid area to be placed.
    /// </summary>
    /// <param name="building"></param>
    /// <returns></returns>
    public static bool CanPlaceBuilding(FactoryBuilding building)
    {
        List<WorldTile> worldTiles = new List<WorldTile>();

        foreach (Tile tile in building.Tiles)
        {
            // Check if each tile on the building is out of bounds
            if (!instance.worldTiles.TryGetValue(tile.GridPosition, out WorldTile wt))
                return false;
            worldTiles.Add(wt);
        }

        // Make sure all tiles don't have buildings and that this position is agreeable to the building
        return building.CanBePlacedOn(worldTiles) && worldTiles.All((tile) => !tile.HasBuilding());
    }

    /// <summary>
    /// Attempts to place the <paramref name="building"/> in the world.
    /// </summary>
    /// <param name="building"></param>
    /// <returns>Whether or not the building could be placed</returns>
    public static bool PlaceBuilding(FactoryBuilding building)
    {
        if (!CanPlaceBuilding(building))
            return false;

        building.Place();

        // Add the building's info to our vaults
        instance.buildings.Add(building);
        foreach (Tile tile in building.Tiles)
            instance.tileToBuilding[tile.GridPosition] = building;

        return true;
    }

    /// <summary>
    /// Removes the <paramref name="building"/> from the world and destroys it.
    /// </summary>
    /// <param name="building"></param>
    public static void RemoveBuilding(FactoryBuilding building)
    {
        // Obliterate that structure
        Destroy(building.gameObject);

        // Expunge the building's data
        instance.buildings.Remove(building);
        foreach (Tile tile in building.Tiles)
            instance.tileToBuilding.Remove(tile.GridPosition);
    }

    /// <summary>
    /// Does the <paramref name="position"/> have a building tile on it?
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool HasBuilding(Vector2Int gridPosition) => instance.tileToBuilding.ContainsKey(gridPosition);

    /// <summary>
    /// Returns the resource type, if any, at <paramref name="position"/>.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static ProductID GetResourceType(Vector2Int gridPosition) => GetWorldTile(gridPosition).Product;

    public static WorldTile GetWorldTile(Vector2Int gridPosition) => instance.worldTiles[gridPosition];


    /// <summary>
    /// Returns the center of the grid cell at <paramref name="gridPosition"/>.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public static Vector2 GridToWorldPosition(Vector2Int gridPosition)
    {
        return instance._GridToWorldPosition(gridPosition);
    }

    /// <summary>
    /// Returns the grid position of the <paramref name="worldPosition"/>.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public static Vector2Int WorldToGridPosition(Vector2 worldPosition)
    {
        return instance._WorldToGridPosition(worldPosition);
    }

    /// <summary>
    /// Returns true if the <paramref name="gridPosition"/> is in placeable area.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public static bool IsPositionInWorld(Vector2Int gridPosition)
    {
        return instance._IsPositionInWorld(gridPosition);
    }

    // Local function so gizmos won't complain about the singleton
    Vector2 _GridToWorldPosition(Vector2Int gridPosition)
    {
        return (Vector2)gridPosition * worldTileSize + GetWorldOffset();
    }

    Vector2Int _WorldToGridPosition(Vector2 worldPosition)
    {
        return Vector2Int.RoundToInt((worldPosition / worldTileSize) - GetWorldOffset());
    }

    bool _IsPositionInWorld(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < worldSize.x &&
            gridPosition.y >= 0 && gridPosition.x < worldSize.y;
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
                Vector2 tileWorldPosition = _GridToWorldPosition(position);
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

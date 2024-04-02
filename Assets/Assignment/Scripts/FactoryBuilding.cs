using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : MonoBehaviour
{
    [SerializeField] BuildingDescriptor descriptor;
    [SerializeField] SpriteRenderer spriteRenderer;

    public Direction Rotation { get; private set; }
    public Vector2Int GridPosition { get; private set; }
    public Tile[] Tiles { get; private set; }
    public bool Created { get ; private set; }
    public List<Product> Products { get; private set; } = new List<Product>();

    // From the descriptor
    public Vector2Int Size => descriptor.size;
    public BuildingType Type => descriptor.type;
    public Sprite Sprite => descriptor.sprite;


    /// <summary>
    /// Call this when the building is first created.
    /// </summary>
    /// <param name="type"></param>
    public void Init(BuildingType type)
    {
        descriptor = FactoryManager.Instance.buildings.dict[type];
    }

    protected virtual void Start()
    {
        // Set our GameObject's name properly
        name = descriptor.name;

        // Create tiles
        Tiles = new Tile[descriptor.tiles.Length];
        for (int i = 0; i < Tiles.Length; i++)
            Tiles[i] = new Tile(this, descriptor.tiles[i]);

        spriteRenderer.sprite = Sprite;
        CenterSpriteRenderer();
    }

    protected void CenterSpriteRenderer()
    {
        Vector2 min = Tiles[0].GridPosition;
        Vector2 max = min;

        // Find the corners
        for (int i = 0; i < Tiles.Length; i++)
        {
            Vector2 worldPos = Tiles[i].GridPosition;
            min = Vector2.Min(min, worldPos);
            max = Vector2.Max(max, worldPos);
        }

        // Put it in between the corners
        // TODO: (maybe) account for grid sizes other than 1?
        spriteRenderer.transform.localPosition = min + (max - min) / 2f;
    }

    public virtual void Place()
    {
        //SetPosition(GridPosition); // Set our transform and data position
        Created = true;
    }

    protected virtual void Tick() { }

    Vector2 testPos;

    void Update()
    {
        if (Created)
            Tick();

        // TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Created)
                World.RemoveBuilding(this);
            else
                World.PlaceBuilding(this);
        }

        if (Input.GetKeyDown(KeyCode.R))
            SetRotation(Rotation.RotateRight());

        testPos.x += Input.GetAxis("Horizontal") * 0.1f;
        testPos.y += Input.GetAxis("Vertical") * 0.1f;
        SetPosition(World.WorldToGridPosition(testPos));
    }

    public virtual bool CanBePlacedOn(List<WorldTile> worldTiles) => true;

    public virtual void OnInput(Product product, TileInput input) => Products.Add(product);

    public virtual bool WillAccept(Product product, TileInput input) => true;

    
    public void SetPosition(Vector2Int gridPosition)
    {
        // No editing after we have been created
        if (Created) return;

        GridPosition = gridPosition;
        transform.position = World.GridToWorldPosition(gridPosition);
    }

    public void SetRotation(Direction newUp)
    {
        // No editing after we have been created (if we are too big, allow 1x1s to rotate)
        if (Created && (Size.x > 1 || Size.y > 1)) return;

        // Set our rotation both in data and graphically
        Rotation = newUp;
        transform.rotation = Quaternion.Euler(0, 0, newUp.ToDegrees());
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(testPos, 0.3f);

        Gizmos.color = Color.yellow;
        if (Tiles != null)
            foreach (Tile tile in Tiles)
                Gizmos.DrawSphere(World.GridToWorldPosition(tile.GridPosition), 0.3f);

        Gizmos.color = Color.red;
        if (Application.isPlaying)
            Gizmos.DrawSphere(World.GridToWorldPosition(GridPosition), 0.3f);
    }
}

public enum BuildingType
{
    None,
    Outbox,
    Miner,
    Conveyor,
    Assembler,
    UndergroundInput,
    UndergroundOutput,
    Splitter
}

/*

FactoryBuilding.cs (see subclasses at bottom)
- Meat and bones of the game - represents a building (assemblers, conveyors, anything the player can place)
- Used to mine, transport, process, and outbox items
- Has a SpriteRenderer and BoxCollider2D (sprite set in inspector)
- Stores the tiles that make up the building
- Stores a list of products that are inside the building
- Pseudocode:
  - Variables: descriptor, tiles, created, products
  - Properties: inputs, outputs, buildingType
  - virtual Start => create tiles from descriptor.tiles
  - virtual fn Place(position) => set position, set created = true
  - virtual fn Tick()
  - Update => if created call Tick() (only called once the building is actually placed)
  - virtual fn OnInput(product) => adds the product to the list
  - virtual bool WillAccept(product) => returns true by default
  - virtual fn OnMouseDown() => may open menus for certain subclasses (i.e. recipe selector for assemblers)
  - virtual fn CanBePlacedOn(worldTiles) => returns true if all tiles have no buildings on them

*/

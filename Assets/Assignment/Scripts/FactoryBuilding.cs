using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : MonoBehaviour
{
    public Vector2Int gridPosition;

    public virtual bool WillAccept(Product product, TileInput input) => true;
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

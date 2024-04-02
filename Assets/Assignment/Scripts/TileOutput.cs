using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutput : TileIO
{
    public TileOutput(Direction direction, Tile tile) : base(direction, tile) { }

    public bool CanOutput(Product product)
    {
        Direction dir = GetCurrentDirection();
        // Check if we even have a neighbour
        if (World.TryGetBuildingTile(tile.GridPosition + dir.Offset(), out Tile neighbour))
        {
            if (neighbour.TryGetInput(dir.Opposite(), out TileInput input) && input.CanInput(product)) ;
        }
    }

    public void Output(Product product)
    {
        //tile.Building.OnInput(product, this);
    }
}

/*

TileOutput
- Represents a place where an item can leave a building
- No components - raw class
- Pseudocode:
  - Variables: direction, tile, building
  - fn Init(tile) => set reference to tile and building (direction set in inspector)
  - fn CanOutput(product) => returns true if there is a place to output the item (checks buildings from World for inputs)
  - fn Output(product) => puts the product into the attached input

*/

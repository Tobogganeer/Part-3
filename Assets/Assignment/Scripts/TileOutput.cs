using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutput
{
    Direction direction;
    Tile tile;

    public TileOutput(Direction direction, Tile tile)
    {
        this.tile = tile;
        this.direction = direction;
    }

    public bool CanOutput(Product product)
    {
        // TODO: Check world for nearby buildings
        throw new System.NotImplementedException();
        //return tile.Building.WillAccept(product, this);
    }

    public void Output(Product product)
    {
        //tile.Building.OnInput(product, this);
    }

    /// <summary>
    /// Returns the current direction, taking the building's rotation into account.
    /// </summary>
    /// <returns></returns>
    public Direction GetCurrentDirection()
    {
        return direction.RotateTo(tile.Building.Rotation);
    }

    /// <summary>
    /// Returns the direction as if the building was facing up.
    /// </summary>
    /// <returns></returns>
    public Direction GetStandardDirection()
    {
        return direction;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInput
{
    Direction direction;
    Tile tile;

    public TileInput(Direction direction, Tile tile)
    {
        this.tile = tile;
        this.direction = direction;
    }

    public bool CanInput(Product product)
    {
        return tile.Building.WillAccept(product, this);
    }

    public void Input(Product product)
    {
        tile.Building.OnInput(product, this);
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

TileInput.cs
- Represents a place where an item can enter a building
- Raw C# class
- Pseudocode:
  - Variables: direction, tile, building
  - fn Init(tile) => set reference to tile and building (direction set in inspector)
  - fn CanInput(product) => returns true if the building will accept the product
  - fn Input(product) => calls the building's OnInput() fn

*/

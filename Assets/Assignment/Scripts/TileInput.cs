using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInput : TileIO
{
    public TileInput(Direction direction, Tile tile) : base(direction, tile) { }

    public bool CanInput(Product product)
    {
        return tile.Building.WillAccept(product, this);
    }

    /// <summary>
    /// Input the <paramref name="product"/> into the building.
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Whether or not the product was input successfully</returns>
    public bool Input(Product product)
    {
        if (!CanInput(product))
            return false;

        tile.Building.OnInput(product, this);
        return true;
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

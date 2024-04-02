using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutput : TileIO
{
    public TileOutput(Direction direction, Tile tile) : base(direction, tile) { }

    public bool CanOutput(Product product)
    {
        // Check if we have a neighbour with an input that we can give our product to
        return TryGetNeighbourInput(GetCurrentDirection(), out TileInput input) && input.CanInput(product);
    }

    /// <summary>
    /// Output the <paramref name="product"/> to the neighbouring tile.
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Whether or not the product was output successfully</returns>
    public bool Output(Product product)
    {
        // Check if it is valid
        if (!CanOutput(product))
            return false;

        // Get the neighbour and send it
        return TryGetNeighbourInput(GetCurrentDirection(), out TileInput input) && input.Input(product);
    }

    bool TryGetNeighbourInput(Direction dir, out TileInput input)
    {
        input = null;
        // If we have a neighbour, try to get the input in the correct direction
        return tile.TryGetNeighbour(dir, out Tile neighbour) && neighbour.TryGetInput(dir.Opposite(), out input);
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

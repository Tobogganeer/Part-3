using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInput
{
    Direction direction;
    Tile tile;
    Building building;
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

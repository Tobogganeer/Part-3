using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileDescriptor
{
    public Vector2Int offset;
    public Direction[] inputs;
    public Direction[] outputs;
}

/*

TileDescriptor.cs
- Used to store tile information in the inspector
- Stores inputs, outputs
- Raw C# class
- Pseudocode:
  - Variables: inputs, outputs
  - fn CreateTile(building) => returns a new tile with the inputs and outputs

*/

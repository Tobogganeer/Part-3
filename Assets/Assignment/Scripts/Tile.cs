using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2Int Position {  get; private set; }
    public TileInput[] Inputs { get; private set; }
    public TileOutput[] Outputs { get; private set; }
    public FactoryBuilding Building { get; private set; }

    public Tile(FactoryBuilding building, TileDescriptor descriptor)
    {
        Position = descriptor.position;
        Building = building;
        Inputs = new TileInput[descriptor.inputs.Length];
        Outputs = new TileOutput[descriptor.outputs.Length];

        // Create the inputs and outputs
        for (int i = 0; i < descriptor.inputs.Length; i++)
            Inputs[i] = new TileInput(descriptor.inputs[i], this);
        for (int i = 0; i < descriptor.outputs.Length; i++)
            Outputs[i] = new TileOutput(descriptor.outputs[i], this);
    }
}

/*

Tile.cs
- Represents a part of a building (occupies one WorldTile)
- Stores a list of inputs and outputs
- Notifies the building when an item it trying to enter the building
- Raw C# class
- Pseudocode:
  - Variables: inputs, outputs, building, rotation
  - Tile(building) => set reference to building, init inputs and outputs, set rotation
  - fn HasInput(direction) => returns true if we have an input in that direction (accounts for rotation)
  - fn HasOutput(direction) => returns true if we have an output in that direction (accounts for rotation)
  - fn TryGetInput(direction, out input) => returns the input if we have it
  - fn TryGetOutput(direction, out output) => returns the output if we have it

*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile
{
    private TileDescriptor descriptor;

    /// <summary>
    /// This tile's position in the world
    /// </summary>
    public Vector2Int GridPosition => Building.GridPosition + Building.Rotation.Rotate(descriptor.offset);
    public Vector2 WorldPosition => World.GridToWorldPosition(GridPosition);
    public TileInput[] Inputs { get; private set; }
    public TileOutput[] Outputs { get; private set; }
    public FactoryBuilding Building { get; private set; }

    private GameObject[] ioGraphics; // Arrows showing input and output

    public Tile(FactoryBuilding building, TileDescriptor descriptor)
    {
        this.descriptor = descriptor;
        Building = building;
        Inputs = new TileInput[descriptor.inputs.Length];
        Outputs = new TileOutput[descriptor.outputs.Length];

        List<TileIO> io = new List<TileIO>();

        // Create the inputs and outputs
        for (int i = 0; i < descriptor.inputs.Length; i++)
            Inputs[i] = new TileInput(descriptor.inputs[i], this);
        for (int i = 0; i < descriptor.outputs.Length; i++)
            Outputs[i] = new TileOutput(descriptor.outputs[i], this);

        io.AddRange(Inputs);
        io.AddRange(Outputs);
        ioGraphics = new GameObject[io.Count];

        // Spawn little arrows for each of our inputs and outputs
        for (int i = 0; i < io.Count; i++)
        {
            Quaternion rotation = io[i].GetCurrentDirection().ToRotation();
            GameObject prefab = io[i] is TileInput ? FactoryManager.Instance.tileInputPrefab : FactoryManager.Instance.tileOutputPrefab;
            ioGraphics[i] = Object.Instantiate(prefab, Vector3.zero, rotation, Building.transform);
            ioGraphics[i].transform.localPosition = (Vector2)GridPosition;
        }
    }

    /// <summary>
    /// Returns true if any input is facing in the given <paramref name="direction"/>.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool HasInput(Direction direction)
    {
        return Inputs.Any((input) => input.GetCurrentDirection() == direction);
    }

    /// <summary>
    /// Returns true if any output is facing in the given <paramref name="direction"/>.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool HasOutput(Direction direction)
    {
        return Outputs.Any((output) => output.GetCurrentDirection() == direction);
    }

    public bool TryGetInput(Direction direction, out TileInput input)
    {
        // There might be a way to linq-ify this? not sure
        input = null;
        foreach (TileInput tileInput in Inputs)
        {
            if (tileInput.GetCurrentDirection() == direction)
            {
                input = tileInput;
                return true;
            }
        }
        return false;
    }

    public bool TryGetOutput(Direction direction, out TileOutput output)
    {
        output = null;
        foreach (TileOutput tileOutput in Outputs)
        {
            if (tileOutput.GetCurrentDirection() == direction)
            {
                output = tileOutput;
                return true;
            }
        }
        return false;
    }


    public bool TryGetNeighbour(Direction direction, out Tile neighbour)
    {
        return World.TryGetBuildingTile(GridPosition + direction.Offset(), out neighbour);
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

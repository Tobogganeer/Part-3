using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundInput : Conveyor
{
    public int maxTilesBetweenEnds = 4;

    Queue<Product> productBuffer;

    protected override void Start()
    {
        base.Start();

        productBuffer = new Queue<Product>(maxTilesBetweenEnds);
        // We always want this queue to be full, even full of nulls
        for (int i = 0; i < maxTilesBetweenEnds; i++)
            productBuffer.Enqueue(null);
    }

    public override void Place()
    {
        base.Place();
        StartCoroutine(PushQueue());
    }

    IEnumerator PushQueue()
    {

    }

    protected override float GetTransportTime()
    {
        GetOutputTile(out int distance);
        // Multiply the time by how many tiles it has to travel
        return base.GetTransportTime() * distance;
    }

    protected override IEnumerator TransportProduct(Product product, TileInput input)
    {
        // There are probably better ways to do this (I tried extracting some functions),
        // but I'm just going to copy the whole function and change what I need to

        // Calculate the start and center pos (output comes later)
        Vector2 center = World.GridToWorldPosition(GridPosition);
        Vector2 inputPosition = center + (Vector2)GetInputDirection(input).Offset() * World.TileSize / 2f;

        // Spawn in the actual item sprite
        ProductObject visuals = product.SpawnObject(inputPosition);
        visuals.transform.SetParent(transform); // Make us the parent so if we are destroyed it is too
        // Move the product from the input to the middle of the tile (takes half the time)
        yield return MoveProductObject(visuals, inputPosition, center, GetTransportTime() / 2f);



        // Hold the item until we can get rid of it
        yield return new WaitUntil(() => HasValidOutput(product));

        // For our purposes, we aren't storing the product any more. Free up the space to let more products flow in.
        Products.Remove(product);

        Vector2 outputPosition = center + (Vector2)GetOutputDirection(product).Offset() * World.TileSize / 2f;
        // Move the product from the middle of the tile to the output (the other half of the time)
        yield return MoveProductObject(visuals, center, outputPosition, GetTransportTime() / 2f);

        OutputProduct(product);
        visuals.Obliterate(); // Goodbye visuals, you've served us well
    }

    protected override Direction GetOutputDirection(Product product)
    {
        // UndergroundInputs have no 'output', it's just the other side of the tile
        return Inputs[0].GetCurrentDirection().Opposite();
    }

    protected override bool HasValidOutput(Product product)
    {
        UndergroundOutput output = GetOutputTile(out _);
        // TODO: Change to account for buffer
        return output != null && output.WillAccept(product, null);
    }

    /// <summary>
    /// Tries to find an UndergroundOutput in front of us, returning null if one cannot be found.
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    /// <remarks><paramref name="distance"/> is measured from end to end - it is not the number of tiles between ends.</remarks>
    UndergroundOutput GetOutputTile(out int distance)
    {
        Direction dir = GetOutputDirection(null);
        Vector2Int offset = dir.Offset();
        Vector2Int checkPos = GridPosition;
        for (int i = 0; i < maxTilesBetweenEnds + 1; i++)
        {
            // MARCH FORWARD(!)
            checkPos += offset;
            // See if there is an output here
            if (World.TryGetBuilding(checkPos, out FactoryBuilding building) && building is UndergroundOutput output)
            {
                // Check if we are facing the same direction
                if (output.Rotation == Rotation)
                {
                    distance = i + 1;
                    return output;
                }
            }
        }

        distance = 0;
        return null;
    }
}

/*

UndergroundConveyor.cs
- 1 tile per end (input and output)
- Input has an input, output has an output (shocking) and inputs on the sides
- Allows products to pass under other buildings and conveyors
- Split into two buildings (unlocked at the same time)
- Input end inherits from Conveyor with different behaviour for transporting (has no output)
- Output end inherits from Conveyor (standard conveyor)
- Input end overrides:
  - Variables: transportDelayPerTile, maxTilesBetweenEnds
  - override fn GetTransportTime() => gets the current output (GetOutput(out distance)), and multiplies the distance by transportDelayPerTile
  - override fn HasValidOutput() => returns whether or not GetOutput() is null and whether that output is accepting input
  - fn GetOutput(out distance) => walks forward for maxTilesBetweenEnds + 1 (to include the output), checking for an output tile facing the correct direction. returns it if found, null otherwise

*/

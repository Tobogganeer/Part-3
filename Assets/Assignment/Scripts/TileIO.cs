using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shared functionality for TileInputs and TileOutputs
public class TileIO
{
    protected Direction direction;
    protected Tile tile;

    public Tile Tile => tile;

    public TileIO(Direction direction, Tile tile)
    {
        this.tile = tile;
        this.direction = direction;
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

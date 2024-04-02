using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up, Right, Down, Left
}

public static class DirectionExtensions
{
    // Methods to rotate directions, including wrapping
    /// <summary>
    /// Rotates this direction left once.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Direction RotateLeft(this Direction direction)
    {
        return direction == Direction.Up ? Direction.Left : direction--;
    }

    /// <summary>
    /// Rotates this direction right once.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Direction RotateRight(this Direction direction)
    {
        return direction == Direction.Left ? Direction.Up : direction++;
    }

    /// <summary>
    /// Rotates this direction as if <paramref name="newUp"/> was upwards.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="newUp"></param>
    /// <returns></returns>
    public static Direction RotateTo(this Direction direction, Direction newUp)
    {
        // Thanks to the enum ordering this is pretty easy
        for (int i = 0; i < (int)newUp; i++)
            direction = direction.RotateRight();

        return direction;
    }
}
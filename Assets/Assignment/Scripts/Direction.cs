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
        return direction == Direction.Up ? Direction.Left : --direction;
    }

    /// <summary>
    /// Rotates this direction right once.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Direction RotateRight(this Direction direction)
    {
        return direction == Direction.Left ? Direction.Up : ++direction;
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

    /// <summary>
    /// Returns this direction as a rotation in degrees.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static float ToDegrees(this Direction direction)
    {
        return -(int)direction * 90f;
    }

    /// <summary>
    /// Returns the direction as a Quaternion rotation.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Quaternion ToRotation(this Direction direction)
    {
        return Quaternion.Euler(0f, 0f, direction.ToDegrees());
    }

    public static Direction Opposite(this Direction direction)
    {
        // If it's stupid but it works then it ain't stupid
        return direction.RotateRight().RotateRight();
    }

    /// <summary>
    /// Rotates the vector <paramref name="v"/> to use this vector as the new up.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector2Int Rotate(this Direction direction, Vector2Int v)
    {
        return direction switch
        {
            Direction.Right => new Vector2Int(v.y, -v.x),
            Direction.Down => -v, // Flip it
            Direction.Left => new Vector2Int(-v.y, v.x),
            _ => v
        };
    }

    /// <summary>
    /// Returns a vector pointing in this direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector2Int Offset(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => Vector2Int.up,
            Direction.Right => Vector2Int.right,
            Direction.Down => Vector2Int.down,
            Direction.Left => Vector2Int.left,
            _ => Vector2Int.zero
        };
    }
}

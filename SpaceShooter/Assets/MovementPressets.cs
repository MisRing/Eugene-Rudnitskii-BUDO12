using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementPressets
{
    public static Vector3 GetMovementByType(float delta, MovementType type)
    {
        switch (type)
        {
            case (MovementType.LeftRight): return GetLeftRight(delta);
            case (MovementType.Circle): return GetCircle(delta);
            case (MovementType.Eight): return GetEight(delta);
            case (MovementType.DoubleEight): return GetDoubleEight(delta);
            default: return Vector3.zero;
        }
    }

    public static Vector3 GetLeftRight(float delta)
    {
        Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * delta), 0f, 0f);

        return direction;
    }

    public static Vector3 GetCircle(float delta)
    {
        Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * delta), 0f, Mathf.Cos(Mathf.Deg2Rad * delta));

        return direction;
    }

    public static Vector3 GetEight(float delta)
    {
        Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * delta), 0f, Mathf.Cos(Mathf.Deg2Rad * delta * 2f) * 0.5f);

        return direction;
    }

    public static Vector3 GetDoubleEight(float delta)
    {
        Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * delta), 0f, Mathf.Cos(Mathf.Deg2Rad * delta * 4f) * 0.5f);

        return direction;
    }
}

public enum MovementType
{
    None,
    LeftRight,
    Circle,
    Eight,
    DoubleEight
}
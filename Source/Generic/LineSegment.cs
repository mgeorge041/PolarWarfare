using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSegment
{
    public Vector2 pointA { get; private set; }
    public Vector2 pointB { get; private set; }
    public Vector2 direction { get; private set; }
    public float length { get; private set; }


    // Constructor
    public LineSegment(Vector2 pointA, Vector2 pointB)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        direction = pointB - pointA;
        length = direction.magnitude;
    }


    // Cross product
    public float Cross(Vector2 vector)
    {
        return Cross(direction, vector);
    }


    // Cross product
    public static float Cross(Vector2 vectorA, Vector2 vectorB)
    {
        return vectorA.x * vectorB.y - vectorA.y * vectorB.x;
    }


    // Gets whether point is between two values (ignores lower and upper comparison to each other)
    public static bool ValueBetween(float value, float lower, float upper)
    {
        if ((lower <= value && value <= upper) || (upper <= value && value <= lower))
            return true;
        return false;
    }


    // Gets whether point is possibly on line
    public bool PointPossiblyOnSegment(Vector2 point)
    {
        if (!ValueBetween(point.x, pointA.x, pointB.x))
            return false;
        if (!ValueBetween(point.y, pointA.y, pointB.y))
            return false;
        return true;
    }


    // Gets whether intersects other segment
    public bool IntersectsLineSegment(LineSegment segment)
    {
        float directionCross = Cross(direction, segment.direction);
        Vector2 pointDiff = segment.pointA - pointA;
        float pointCross = Cross(pointDiff, direction);
        
        // Collinear
        if (directionCross == 0 && pointCross == 0)
        {
            float u1 = Vector2.Dot(pointDiff, direction) / Vector2.Dot(direction, direction);
            float u2 = u1 + Vector2.Dot(segment.direction, direction) / Vector2.Dot(direction, direction);
            if ((0 <= u1 && u1 <= 1) || (0 <= u2 && u2 <= 1))
                return true;
            else
                return false;
        }

        // Parallel
        if (directionCross == 0 && pointCross != 0)
            return false;

        // Meet at endpoint
        float u = Cross(pointDiff, segment.direction) / directionCross;
        float v = Cross(pointDiff, direction) / directionCross;
        if (directionCross != 0 && 0 <= u && u <= 1 && 0 <= v && v <= 1)
            return true;

        return false;
    }
}

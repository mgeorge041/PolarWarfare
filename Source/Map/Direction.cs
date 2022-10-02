using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    // Directions
    public static Vector2Int UL = new Vector2Int(-1, 1);
    public static Vector2Int U = new Vector2Int(0, 1);
    public static Vector2Int UR = new Vector2Int(1, 1);
    public static Vector2Int R = new Vector2Int(1, 0);
    public static Vector2Int DR = new Vector2Int(1, -1);
    public static Vector2Int D = new Vector2Int(0, -1);
    public static Vector2Int DL = new Vector2Int(-1, -1);
    public static Vector2Int L = new Vector2Int(-1, 0);
    public static Vector2Int C = new Vector2Int(0, 0);
    public static Vector2Int ULEven = new Vector2Int(-1, 0);
    public static Vector2Int UREven = new Vector2Int(0, 0);
    public static Vector2Int DREven = new Vector2Int(0, -1);
    public static Vector2Int DLEven = new Vector2Int(-1, -1);

    public static Dictionary<int, Vector2Int> squareDirections = new Dictionary<int, Vector2Int>()
    {
        { 0, U },
        { 1, R },
        { 2, D },
        { 3, L },
    };

    // Get distance between points
    public static float GetDistancePoints(Vector3 pointA, Vector3 pointB)
    {
        float xDist = pointB.x - pointA.x;
        float yDist = pointB.y - pointA.y;
        float dist = Mathf.Sqrt(xDist * xDist + yDist * yDist);
        return dist;
    }


    // Get range between points
    public static float GetRangePoints(Vector3 pointA, Vector3 pointB)
    {
        return DistanceToRange(GetDistancePoints(pointA, pointB));
    }


    // Convert distance to range
    public static float DistanceToRange(float dist)
    {
        return dist / 0.16f;
    }


    // Get distance between squares
    public static int GetDistanceBetweenSquares(Square squareA, Square squareB)
    {
        if (squareA == null || squareB == null || squareA == squareB)
            return 0;

        int distance = Mathf.Abs(squareA.coords.x - squareB.coords.x) + Mathf.Abs(squareA.coords.y - squareB.coords.y);
        return distance;
    }


    // Get distance between grid areas
    public static int GetDistanceBetweenGridAreas(GridArea areaA, GridArea areaB)
    {
        if (areaA == null || areaB == null || areaA == areaB)
            return 0;

        int distance = Mathf.Abs(areaA.coords.x - areaB.coords.x) + Mathf.Abs(areaA.coords.y - areaB.coords.y);
        return distance;
    }


    // Get direction index
    private static int GetDirectionIndex(Vector2Int direction)
    {
        foreach (KeyValuePair<int, Vector2Int> pair in squareDirections)
        {
            if (pair.Value == direction)
                return pair.Key;
        }
        return -1;
    }


    // Get next clockwise direction
    public static Vector2Int GetNextSquareClockwiseDirection(Vector2Int directionIn)
    {
        int index = GetDirectionIndex(directionIn);
        return squareDirections[(index + 1) % squareDirections.Count];
    }


    // Get next counter clockwise direction
    public static Vector2Int GetNextSquareCounterClockwiseDirection(Vector2Int directionIn)
    {
        int index = GetDirectionIndex(directionIn);
        return squareDirections[(index - 1 + squareDirections.Count) % squareDirections.Count];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : GridArea
{
    // Constructor
    public Intersection()
    {
        SetBounds();
        squareCoords = new List<Vector2Int>();
    }
    public Intersection(Vector2Int coords)
    {
        this.coords = coords;
        SetBounds();
        squareCoords = new List<Vector2Int>()
        {
            new Vector2Int(coords.x, coords.y),
            new Vector2Int(coords.x - 1, coords.y),
            new Vector2Int(coords.x - 1, coords.y - 1),
            new Vector2Int(coords.x, coords.y - 1),
        };
    }


    // Set bounds
    protected override void SetBounds()
    {
        bounds = new Bounds(new Vector3(
            SQUARE_WIDTH * coords.x,
            SQUARE_HEIGHT * coords.y),
            new Vector3(SQUARE_WIDTH, SQUARE_HEIGHT));
    }


    // Get neighbor coords
    public override List<Vector2Int> GetSquareCoords(int range)
    {
        List<Vector2Int> neighborCoordsList = new List<Vector2Int>();

        for (int i = -range + 1; i <= range; i++)
        {
            for (int j = -range + 1; j <= range; j++)
            {
                Vector2Int neighborCoords = new Vector2Int(coords.x - i, coords.y - j);
                neighborCoordsList.Add(neighborCoords);
            }
        }

        return neighborCoordsList;
    }


    // Get corner coords
    public override List<Vector2Int> GetCornerCoords(int range)
    {
        List<Vector2Int> cornerCoordsList = new List<Vector2Int>();

        Vector2Int startSquare = coords - range * Vector2Int.one;
        cornerCoordsList.Add(startSquare);
        cornerCoordsList.Add(startSquare + new Vector2Int(0, range));
        cornerCoordsList.Add(startSquare + new Vector2Int(range, range));
        cornerCoordsList.Add(startSquare + new Vector2Int(range, 0));

        return cornerCoordsList;
    }
}

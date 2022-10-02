using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterNS;
using GameMapNS;

public abstract class GridArea
{
    public const float SQUARE_WIDTH = 0.16f;
    public const float SQUARE_HEIGHT = 0.16f;
    public const float SQUARE_HALFWIDTH = 0.08f;
    public const float SQUARE_HALFHEIGHT = 0.08f;
    public Vector2Int coords = Vector2Int.zero;
    public List<Vector2Int> squareCoords;
    public Bounds bounds;
    public TileInfo tileInfo;
    public Character character { get; protected set; } = null;
    public bool hasCharacter { get { return character != null; } }

    public int fCost;
    public int gCost;
    public int hCost { get { return gCost + fCost; } }
    public GridArea pathParent;


    // Constructor
    public GridArea()
    {
        SetBounds();
    }
    public GridArea(Vector2Int coords)
    {
        this.coords = coords;
        SetBounds();
    }


    // Reset
    public void Reset()
    {
        SetCharacter(null);
    }


    // Set bounds
    protected virtual void SetBounds()
    {
        bounds = new Bounds(new Vector3(
            SQUARE_WIDTH * coords.x + SQUARE_WIDTH / 2,
            SQUARE_HEIGHT * coords.y + SQUARE_HEIGHT / 2),
            new Vector3(SQUARE_WIDTH, SQUARE_HEIGHT));
    }


    // Set tile info
    public void SetTileInfo(TileInfo tileInfo)
    {
        this.tileInfo = tileInfo;
    }


    // Set character
    public void SetCharacter(Character character)
    {
        this.character = character;
    }


    // Get neighbor coords
    public virtual List<Vector2Int> GetSquareCoords(int range)
    {
        List<Vector2Int> neighborCoordsList = new List<Vector2Int>();

        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                Vector2Int neighborCoords = new Vector2Int(coords.x + i, coords.y + j);
                neighborCoordsList.Add(neighborCoords);
            }

        }

        return neighborCoordsList;
    }


    // Get corner coords
    public virtual List<Vector2Int> GetCornerCoords(int range)
    {
        List<Vector2Int> cornerCoordsList = new List<Vector2Int>();

        cornerCoordsList.Add(coords + new Vector2Int(-range, range));
        cornerCoordsList.Add(coords + new Vector2Int(range, range));
        cornerCoordsList.Add(coords + new Vector2Int(range, -range));
        cornerCoordsList.Add(coords + new Vector2Int(-range, -range));
        
        return cornerCoordsList;
    }


    // Get neighbor coords in direction
    public Vector2Int GetNeighborCoords(Vector2Int direction)
    {
        return coords + direction;
    }


    // Get movement neighbor coords
    public List<Vector2Int> GetMovementNeighborCoords()
    {
        List<Vector2Int> neighborCoordsList = new List<Vector2Int>();

        neighborCoordsList.AddRange(new List<Vector2Int>() 
            { 
                Direction.U + coords, 
                Direction.R + coords, 
                Direction.D + coords, 
                Direction.L + coords
            }
        );

        return neighborCoordsList;
    }
}

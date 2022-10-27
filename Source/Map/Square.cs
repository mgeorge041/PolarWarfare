using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePieceNS.CharacterNS;

public class Square : GridArea
{
    public Rectangle collider;
    private Dictionary<Vector2Int, Vector3> edgePoints;

    // Constructor
    public Square()
    {
        SetBounds();
    }
    public Square(Vector2Int coords)
    {
        this.coords = coords;
        SetBounds();
        edgePoints = new Dictionary<Vector2Int, Vector3>()
        {
            { Direction.U, bounds.max },
            { Direction.R, new Vector3(bounds.max.x, bounds.max.y - bounds.size.y) },
            { Direction.D, bounds.min },
            { Direction.L, new Vector3(bounds.min.x, bounds.min.y + bounds.size.y) },
        };
        collider = new Rectangle(new Vector2[] {
            bounds.min,
            new Vector2(bounds.min.x + bounds.size.x, bounds.min.y),
            bounds.max,
            new Vector2(bounds.max.x - bounds.size.x, bounds.max.y),
        });
    }


    // Get whether can select character
    public bool HasSelectableCharacter()
    {
        if (hasCharacter && character.canMove)
            return true;
        return false;
    }


    // Get edge points at direction
    public Vector3 GetEdgePointAtDirection(Vector2Int direction)
    {
        return edgePoints[direction];
    }


    // Get whether can place
    public bool CanPlace()
    {
        if (hasCharacter || !tileInfo.canMove)
            return false;
        return true;
    }


    // Get whether can move
    public bool CanMove(Character character)
    {
        if ((hasCharacter && character != this.character) || !tileInfo.canMove)
            return false;
        return true;
    }


    // Square data
    [System.Serializable]
    public class SquareData 
    {
        public string tileName;
        public Vector2Int coords;
        public Character character;
        public string characterName;

        public SquareData(Square square)
        {
            tileName = square.tileInfo.tileName;
            coords = square.coords;
            if (square.hasCharacter)
            {
                character = square.character;
                characterName = square.character.characterName;
            }
        }
    }
}

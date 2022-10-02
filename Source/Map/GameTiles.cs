using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GameTiles
{
    // Tiles
    public static Tile testTile = Resources.Load<Tile>("GameMap/Tiles/Terrain Tiles/Snow Tile");
    public static Tile attackTile = Resources.Load<Tile>("GameMap/Tiles/Attack Tile");
    public static Tile selectTile = Resources.Load<Tile>("GameMap/Tiles/Select Tile");
    public static Tile moveTile = Resources.Load<Tile>("GameMap/Tiles/Move Tile");
    public static Tile placementTile = Resources.Load<Tile>("GameMap/Tiles/Placement Tile");

    // Select tiles
    public static Dictionary<Vector2Int, Tile> selectTilesEvenSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.ULEven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UL Select Tile") },
        { Direction.UREven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UR Select Tile") },
        { Direction.DREven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DR Select Tile") },
        { Direction.DLEven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DL Select Tile") },
    };
    public static Dictionary<Vector2Int, Tile> selectTilesOddSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.UL, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UL Select Tile") },
        { Direction.UR, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UR Select Tile") },
        { Direction.DR, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DR Select Tile") },
        { Direction.DL, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DL Select Tile") },
    };


    // Move tiles
    public static Dictionary<Vector2Int, Tile> moveTilesEvenSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.ULEven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UL Move Tile") },
        { Direction.UREven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UR Move Tile") },
        { Direction.DREven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DR Move Tile") },
        { Direction.DLEven, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DL Move Tile") },
    };
    public static Dictionary<Vector2Int, Tile> moveTilesOddSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.UL, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UL Move Tile") },
        { Direction.UR, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/UR Move Tile") },
        { Direction.DR, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DR Move Tile") },
        { Direction.DL, Resources.Load<Tile>("GameMap/Tiles/Action Tiles/DL Move Tile") },
    };

}

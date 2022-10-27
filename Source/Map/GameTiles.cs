using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GameTiles
{
    private static string actionTilesPath = "GameMap/Tiles/Action Tiles/";


    // Tiles
    public static Tile testTile = Resources.Load<Tile>("GameMap/Tiles/Terrain Tiles/Snow Tile");
    public static Tile attackTile = Resources.Load<Tile>("GameMap/Tiles/Attack Tile");
    public static Tile selectTile = Resources.Load<Tile>("GameMap/Tiles/Select Tile");
    public static Tile moveTile = Resources.Load<Tile>("GameMap/Tiles/Move Tile");
    public static Tile placementTile = Resources.Load<Tile>("GameMap/Tiles/Placement Tile");
    public static AnimatedTile actionArrow = Resources.Load<AnimatedTile>(actionTilesPath + "Action Arrow Anim");
    public static AnimatedTile selectTileAnim = Resources.Load<AnimatedTile>(actionTilesPath + "UL Select Tile Anim");
    public static AnimatedTile moveTileAnim = Resources.Load<AnimatedTile>(actionTilesPath + "UL Move Tile Anim");

    // Select tiles
    public static Dictionary<Vector2Int, Tile> selectTilesEvenSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.ULEven, Resources.Load<Tile>(actionTilesPath + "UL Select Tile") },
        { Direction.UREven, Resources.Load<Tile>(actionTilesPath + "UR Select Tile") },
        { Direction.DREven, Resources.Load<Tile>(actionTilesPath + "DR Select Tile") },
        { Direction.DLEven, Resources.Load<Tile>(actionTilesPath + "DL Select Tile") },
    };
    public static Dictionary<Vector2Int, Tile> selectTilesOddSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.UL, Resources.Load<Tile>(actionTilesPath + "UL Select Tile") },
        { Direction.UR, Resources.Load<Tile>(actionTilesPath + "UR Select Tile") },
        { Direction.DR, Resources.Load<Tile>(actionTilesPath + "DR Select Tile") },
        { Direction.DL, Resources.Load<Tile>(actionTilesPath + "DL Select Tile") },
    };
    public static Dictionary<Vector2Int, float> selectTilesRotationEven = new Dictionary<Vector2Int, float>()
    {
        { Direction.ULEven, 0 },
        { Direction.UREven, 270 },
        { Direction.DREven, 180 },
        { Direction.DLEven, 90 },
    };
    public static Dictionary<Vector2Int, float> selectTilesRotationOdd = new Dictionary<Vector2Int, float>()
    {
        { Direction.UL, 0 },
        { Direction.UR, 270 },
        { Direction.DR, 180 },
        { Direction.DL, 90 },
    };


    // Move tiles
    public static Dictionary<Vector2Int, Tile> moveTilesEvenSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.ULEven, Resources.Load<Tile>(actionTilesPath + "UL Move Tile") },
        { Direction.UREven, Resources.Load<Tile>(actionTilesPath + "UR Move Tile") },
        { Direction.DREven, Resources.Load<Tile>(actionTilesPath + "DR Move Tile") },
        { Direction.DLEven, Resources.Load<Tile>(actionTilesPath + "DL Move Tile") },
    };
    public static Dictionary<Vector2Int, Tile> moveTilesOddSize = new Dictionary<Vector2Int, Tile>()
    {
        { Direction.UL, Resources.Load<Tile>(actionTilesPath + "UL Move Tile") },
        { Direction.UR, Resources.Load<Tile>(actionTilesPath + "UR Move Tile") },
        { Direction.DR, Resources.Load<Tile>(actionTilesPath + "DR Move Tile") },
        { Direction.DL, Resources.Load<Tile>(actionTilesPath + "DL Move Tile") },
    };

}

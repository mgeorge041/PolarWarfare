using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMapNS
{
    [CreateAssetMenu(fileName = "New Tile", menuName = "Tile Info")]
    public class TileInfo : ScriptableObject
    {
        public Tile tile;
        public string tileName;
        public bool canMove;


        // Load tile info
        public static TileInfo LoadTileInfo(string tileName)
        {
            TileInfo tileInfo = Instantiate(Resources.Load<TileInfo>("GameMap/Tiles/Terrain Tiles/" + tileName + " Tile Info"));
            return tileInfo;
        }


        // Load tile info
        public static TileInfo LoadTileInfo()
        {
            return LoadTileInfo("Snow");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;
using CharacterNS;

namespace Tests.GameMapNS_Tests
{
    public class Tests_GameMap
    {
        private GameMap gameMap;

        
        // Setup
        [SetUp]
        public void Setup()
        {
            gameMap = GameMap.InstantiateGameMap();
        }


        // Test create game map
        [Test]
        public void CreatesGameMap()
        {
            Assert.IsNotNull(gameMap);
        }


        // Test create default game map
        [Test]
        public void CreatesDefaultGameMap()
        {
            Assert.AreEqual(100, gameMap.squareCoordsDict.Count);
            Assert.AreEqual(81, gameMap.intersectionDict.Count);
            Assert.AreEqual(GameTiles.testTile, gameMap.squareCoordsDict[Vector2Int.zero].tileInfo.tile);
        }


        // Converts square to world coords
        [Test]
        public void ConvertsSquareToWorldCoords()
        {
            Vector3 worldCoords = gameMap.SquareCoordsToWorldPosition(Vector2Int.zero);
            Vector3 expectedWorldPosition = new Vector3(GridArea.SQUARE_HALFWIDTH, GridArea.SQUARE_HALFHEIGHT, 0);
            Assert.AreEqual(expectedWorldPosition, worldCoords);
        }


        // Converts world to square coords
        [Test]
        public void ConvertsWorldToSquareCoords()
        {
            Vector2Int squareCoords = gameMap.WorldPositionToSquareCoords(new Vector3(GridArea.SQUARE_HALFWIDTH, GridArea.SQUARE_HALFHEIGHT, 0));
            Vector2Int expectedSquareCoords = new Vector2Int(0, 0);
            Assert.AreEqual(expectedSquareCoords, squareCoords);
        }


        // Gets square at square coords
        [Test]
        public void GetsSquareAtSquareCoords()
        {
            Square square = gameMap.GetSquareAtSquareCoords(Vector2Int.zero);
            Assert.AreEqual(Vector2Int.zero, square.coords);
        }


        // Gets square at world coords
        [Test]
        public void GetsSquareAtWorldPosition()
        {
            Vector3 worldPosition = new Vector3(GridArea.SQUARE_HALFWIDTH, GridArea.SQUARE_HALFHEIGHT, 0);
            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            Assert.AreEqual(Vector2Int.zero, square.coords);
        }


        // Gets intersection at intersection coords
        [Test]
        public void GetsIntersectionAtIntersectionCoords()
        {
            Intersection intersection = gameMap.GetIntersectionAtIntersectionCoords(Vector2Int.one);
            Assert.AreEqual(Vector2Int.one, intersection.coords);
        }


        // Gets intersection at world coords
        [Test]
        public void GetsIntersectionAtWorldPosition()
        {
            Vector3 worldPosition = new Vector3(GridArea.SQUARE_WIDTH, GridArea.SQUARE_HEIGHT, 0);
            Intersection intersection = gameMap.GetIntersectionAtWorldPosition(worldPosition);
            Assert.AreEqual(Vector2Int.one, intersection.coords);
        }


        // Gets grid area at world position
        [Test]
        public void GetsGridAreaAtWorldPosition_Square()
        {
            GridArea gridArea = gameMap.GetGridAreaAtWorldPosition(CharacterSizeType.Odd, new Vector3(GridArea.SQUARE_HALFWIDTH, GridArea.SQUARE_HALFHEIGHT, 0));
            Assert.AreEqual(typeof(Square), gridArea.GetType());
        }


        // Gets grid area at world position
        [Test]
        public void GetsGridAreaAtWorldPosition_Intersection()
        {
            GridArea gridArea = gameMap.GetGridAreaAtWorldPosition(CharacterSizeType.Even, new Vector3(GridArea.SQUARE_HALFWIDTH, GridArea.SQUARE_HALFHEIGHT, 0));
            Assert.AreEqual(typeof(Intersection), gridArea.GetType());
        }


        // Gets square tile
        [Test]
        public void GetsSquareTile()
        {
            Square square = gameMap.GetSquareAtSquareCoords(Vector2Int.zero);
            Assert.AreEqual(GameTiles.testTile, gameMap.GetSquareTile(square));
        }


        // Paints squares
        [Test]
        public void PaintsSquares()
        {
            Square square = gameMap.GetSquareAtSquareCoords(Vector2Int.zero);
            gameMap.PaintSquare(square, GameTiles.attackTile);
            Assert.AreEqual(GameTiles.attackTile, gameMap.GetSquareTile(square));
        }
    }
}
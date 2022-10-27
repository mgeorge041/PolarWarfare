using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;
using GamePieceNS.CharacterNS;

namespace Tests.GameMapNS_Tests
{
    public class Tests_Pathfinding
    {
        private GameMap gameMap;
        private Character character2x2A;
        private Character character2x2B;
        private Character character3x3A;
        private Character character3x3B;
        private Vector2Int startCoords;
        private Vector2Int targetCoords;


        // Setup
        [SetUp]
        public void Setup()
        {
            gameMap = GameMap.InstantiateGameMap();
            character2x2A = Character.InstantiateCharacter();
            character2x2B = Character.InstantiateCharacter();
            character3x3A = Character.InstantiateCharacter("Test Character 3x3");
            character3x3B = Character.InstantiateCharacter("Test Character 3x3");
            startCoords = Vector2Int.one;
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_NoObstacles_X()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            targetCoords = new Vector2Int(2, 1);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(2, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_NoObstacles_Y()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            targetCoords = new Vector2Int(1, 2);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(2, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_NoObstacles_XY()
        {
            gameMap.AddCharacterAtCoords(character2x2A, new Vector2Int(5, 5));
            targetCoords = new Vector2Int(2, 2);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(3, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_Obstacles_2x2_X()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            gameMap.AddCharacterAtCoords(character2x2B, new Vector2Int(3, 1));
            targetCoords = new Vector2Int(5, 1);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(9, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_Obstacles_2x2_Y()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            gameMap.AddCharacterAtCoords(character2x2B, new Vector2Int(1, 3));
            targetCoords = new Vector2Int(1, 5);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(9, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_Obstacles_2x2_XY()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            gameMap.AddCharacterAtCoords(character2x2B, new Vector2Int(3, 2));
            targetCoords = new Vector2Int(5, 3);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(9, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_Obstacles_3x3_X()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            gameMap.AddCharacterAtCoords(character3x3A, new Vector2Int(3, 1));
            targetCoords = new Vector2Int(6, 1);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(12, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_Obstacles_3x3_Y()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            gameMap.AddCharacterAtCoords(character3x3A, new Vector2Int(1, 3));
            targetCoords = new Vector2Int(1, 6);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(12, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Intersection_Obstacles_3x3_XY()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            gameMap.AddCharacterAtCoords(character3x3A, new Vector2Int(3, 2));
            targetCoords = new Vector2Int(7, 3);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character2x2A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(13, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_NoObstacles_X()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            targetCoords = new Vector2Int(2, 1);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(2, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_NoObstacles_Y()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            targetCoords = new Vector2Int(1, 2);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(2, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_Obstacles_3x3_X()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            gameMap.AddCharacterAtCoords(character3x3B, new Vector2Int(4, 1));
            targetCoords = new Vector2Int(7, 1);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(13, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_Obstacles_3x3_Y()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            gameMap.AddCharacterAtCoords(character3x3B, new Vector2Int(1, 4));
            targetCoords = new Vector2Int(1, 7);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(13, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_Obstacles_3x3_XY()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            gameMap.AddCharacterAtCoords(character3x3B, new Vector2Int(4, 2));
            targetCoords = new Vector2Int(7, 3);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(13, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_Obstacles_2x2_X()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            gameMap.AddCharacterAtCoords(character2x2A, new Vector2Int(4, 1));
            targetCoords = new Vector2Int(6, 1);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(10, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_Obstacles_2x2_Y()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            gameMap.AddCharacterAtCoords(character2x2A, new Vector2Int(1, 4));
            targetCoords = new Vector2Int(1, 6);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(10, path.Count);
        }


        // Test path
        [Test]
        public void GetsPath_Square_Obstacles_2x2_XY()
        {
            gameMap.AddCharacterAtCoords(character3x3A, startCoords);
            gameMap.AddCharacterAtCoords(character2x2A, new Vector2Int(4, 2));
            targetCoords = new Vector2Int(6, 3);
            List<GridArea> path = Pathfinding.GetPath(gameMap, character3x3A, startCoords, targetCoords);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(10, path.Count);
        }


        // Test max path
        [Test]
        public void GetMaxPath_Intersection_NoObstacles()
        {
            gameMap.AddCharacterAtCoords(character2x2A, startCoords);
            character2x2A.speed = 2;
            List<GridArea> path = Pathfinding.GetMaxPath(gameMap, character2x2A);
            Pathfinding.PrintPath(path);
            Assert.AreEqual(6, path.Count);
        }
    }
}
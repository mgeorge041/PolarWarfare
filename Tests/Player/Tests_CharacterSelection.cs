using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;
using CharacterNS;
using PlayerNS;

namespace Tests.PlayerNS_Tests
{
    public class Tests_CharacterSelection
    {
        private GameMap gameMap;
        private PlayerInputController playerInputController;
        private PlayerStateController playerStateController;
        private Character character1;
        private Character character2;
        private Vector2Int coords1;
        private Vector2Int coords2;
        private Square square1;
        private Square square2;


        // Setup
        [SetUp]
        public void Setup()
        {
            gameMap = GameMap.InstantiateGameMap();
            playerInputController = PlayerInputController.InstantiatePlayerInputController();
            playerStateController = new PlayerStateController(gameMap);
            
            coords1 = new Vector2Int(1, 1);
            coords2 = new Vector2Int(5, 5);

            character1 = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats(), coords1);
            character2 = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats("Test Character 3x3"), coords2);
            
            square1 = gameMap.GetSquareAtSquareCoords(coords1);
            square2 = gameMap.GetSquareAtSquareCoords(coords2);
        }


        // Teardown
        [TearDown]
        public void Teardown()
        {
            C.Destroy(gameMap);
            C.Destroy(playerInputController);
            C.Destroy(character1);
            C.Destroy(character2);
            GameMapEventManager.Unsubscribe();
        }


        // Test selecting 2x2 character
        [Test]
        public void SelectsCharacter_2x2()
        {
            playerInputController.PlayerLeftClicked(square1.bounds.center);
            Assert.AreEqual(GameTiles.selectTilesEvenSize[Direction.ULEven], gameMap.GetSquareTile(new Vector2Int(0, 1)));
            Assert.AreEqual(GameTiles.selectTilesEvenSize[Direction.UREven], gameMap.GetSquareTile(new Vector2Int(1, 1)));
            Assert.AreEqual(GameTiles.selectTilesEvenSize[Direction.DREven], gameMap.GetSquareTile(new Vector2Int(1, 0)));
            Assert.AreEqual(GameTiles.selectTilesEvenSize[Direction.DLEven], gameMap.GetSquareTile(new Vector2Int(0, 0)));
        }


        // Test selecting 3x3 character
        [Test]
        public void SelectsCharacter_3x3()
        {
            playerInputController.PlayerLeftClicked(square2.bounds.center);
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.UL], gameMap.GetSquareTile(new Vector2Int(4, 6)));
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.UR], gameMap.GetSquareTile(new Vector2Int(6, 6)));
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.DR], gameMap.GetSquareTile(new Vector2Int(6, 4)));
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.DL], gameMap.GetSquareTile(new Vector2Int(4, 4)));
        }


        // Test selecting second character
        [Test]
        public void SelectsSecondCharacter()
        {
            playerInputController.PlayerLeftClicked(square1.bounds.center);
            playerInputController.PlayerLeftClicked(square2.bounds.center);
            Assert.AreEqual(GameTiles.testTile, gameMap.GetSquareTile(new Vector2Int(0, 1)));
            Assert.AreEqual(GameTiles.testTile, gameMap.GetSquareTile(new Vector2Int(1, 1)));
            Assert.AreEqual(GameTiles.testTile, gameMap.GetSquareTile(new Vector2Int(1, 0)));
            Assert.AreEqual(GameTiles.testTile, gameMap.GetSquareTile(new Vector2Int(0, 0)));
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.UL], gameMap.GetSquareTile(new Vector2Int(4, 6)));
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.UR], gameMap.GetSquareTile(new Vector2Int(6, 6)));
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.DR], gameMap.GetSquareTile(new Vector2Int(6, 4)));
            Assert.AreEqual(GameTiles.selectTilesOddSize[Direction.DL], gameMap.GetSquareTile(new Vector2Int(4, 4)));
        }
    }
}
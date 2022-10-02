using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;
using PlayerNS;
using CharacterNS;

namespace Tests.ScenesNS_Tests
{
    public class Tests_CharacterTargeting
    {
        private GameMap gameMap;
        private Character character1;
        private Character character2;
        private Vector2Int coords1;
        private Vector2Int coords2;
        private Square square1;
        private Square square2;
        private PlayerStateController gameMapInputController;


        // Setup
        [SetUp]
        public void Setup()
        {
            gameMap = GameMap.InstantiateGameMap();
            character1 = Character.InstantiateCharacter();
            character2 = Character.InstantiateCharacter();
            coords1 = new Vector2Int(1, 1);
            coords2 = new Vector2Int(3, 1);
            square1 = gameMap.GetSquareAtSquareCoords(coords1);
            square2 = gameMap.GetSquareAtSquareCoords(coords2);
            gameMap.AddCharacterAtCoords(character1, coords1);
            gameMap.AddCharacterAtCoords(character2, coords2);
            gameMapInputController = new PlayerStateController(gameMap);
        }


        // Test hovering over other character
        [Test]
        public void HoversSecondCharacter()
        {
            gameMapInputController.LeftClickAtWorldPosition(square1.bounds.center);
            gameMapInputController.HoverAtWorldPosition(square2.bounds.center);
            Assert.AreEqual(GameTiles.attackTile, gameMap.GetSquareTile(square2.coords));
        }


        // Test selecting second character after hover
        [Test]
        public void SelectsSecondCharacterAfterHover()
        {
            gameMapInputController.LeftClickAtWorldPosition(square1.bounds.center);
            gameMapInputController.HoverAtWorldPosition(square2.bounds.center);
            gameMapInputController.LeftClickAtWorldPosition(square2.bounds.center);
            Assert.AreEqual(GameTiles.testTile, gameMap.GetSquareTile(square1.coords));
            Assert.AreEqual(GameTiles.selectTile, gameMap.GetSquareTile(square2.coords));
        }
    }
}
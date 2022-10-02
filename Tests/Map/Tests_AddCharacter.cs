using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;
using CharacterNS;
using PlayerNS.PlayerStateNS;

namespace Tests.ScenesNS_Tests
{
    public class Tests_AddCharacter
    {
        private GameMap gameMap;
        private Character character1;
        private Character character2;
        private Vector2Int coords1;
        private Vector2Int coords2;

        
        // Setup
        [SetUp]
        public void Setup()
        {
            gameMap = GameMap.InstantiateGameMap();
            coords1 = new Vector2Int(1, 1);
            coords2 = new Vector2Int(5, 5);
            character1 = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats(), coords1);
            character2 = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats("Test Character 3x3"), coords2);
        }


        // Test adding 2x2 character to coords
        [Test]
        public void AddsCharacterToCoords_2x2()
        {
            Square squareA = gameMap.GetSquareAtSquareCoords(new Vector2Int(0, 0));
            Square squareB = gameMap.GetSquareAtSquareCoords(new Vector2Int(1, 0));
            Square squareC = gameMap.GetSquareAtSquareCoords(new Vector2Int(0, 1));
            Square squareD = gameMap.GetSquareAtSquareCoords(new Vector2Int(1, 1));
            Assert.AreEqual(character1, squareA.character);
            Assert.AreEqual(character1, squareB.character);
            Assert.AreEqual(character1, squareC.character);
            Assert.AreEqual(character1, squareD.character);

            Assert.Contains(squareA, character1.squares);
            Assert.Contains(squareB, character1.squares);
            Assert.Contains(squareC, character1.squares);
            Assert.Contains(squareD, character1.squares);
        }


        // Test adding 3x3 character to coords
        [Test]
        public void AddsCharacterToCoords_3x3()
        {
            Square squareA = gameMap.GetSquareAtSquareCoords(new Vector2Int(4, 4));
            Square squareB = gameMap.GetSquareAtSquareCoords(new Vector2Int(4, 5));
            Square squareC = gameMap.GetSquareAtSquareCoords(new Vector2Int(4, 6));
            Square squareD = gameMap.GetSquareAtSquareCoords(new Vector2Int(5, 4));
            Square squareE = gameMap.GetSquareAtSquareCoords(new Vector2Int(5, 5));
            Square squareF = gameMap.GetSquareAtSquareCoords(new Vector2Int(5, 6));
            Square squareG = gameMap.GetSquareAtSquareCoords(new Vector2Int(6, 4));
            Square squareH = gameMap.GetSquareAtSquareCoords(new Vector2Int(6, 5));
            Square squareI = gameMap.GetSquareAtSquareCoords(new Vector2Int(6, 6));

            Debug.Log("3x3 character size: " + character2.size);
            Debug.Log("3x3 character size / 2: " + (character2.size / 2));
            Assert.AreEqual(character2, squareA.character);
            Assert.AreEqual(character2, squareB.character);
            Assert.AreEqual(character2, squareC.character);
            Assert.AreEqual(character2, squareD.character);
            Assert.AreEqual(character2, squareE.character);
            Assert.AreEqual(character2, squareF.character);
            Assert.AreEqual(character2, squareG.character);
            Assert.AreEqual(character2, squareH.character);
            Assert.AreEqual(character2, squareI.character);

            Assert.Contains(squareA, character2.squares);
            Assert.Contains(squareB, character2.squares);
            Assert.Contains(squareC, character2.squares);
            Assert.Contains(squareD, character2.squares);
            Assert.Contains(squareE, character2.squares);
            Assert.Contains(squareF, character2.squares);
            Assert.Contains(squareG, character2.squares);
            Assert.Contains(squareH, character2.squares);
            Assert.Contains(squareI, character2.squares);
        }
    }
}
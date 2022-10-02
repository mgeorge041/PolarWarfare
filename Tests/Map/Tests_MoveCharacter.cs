using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;
using CharacterNS;

namespace Tests.GameMapNS_Tests
{
    public class Tests_MoveCharacter
    {
        private GameMap gameMap;
        private Character character1;
        private Vector2Int coords1;
        private Vector2Int coords2;
        private Vector2Int coords3;


        // Setup
        [SetUp]
        public void Setup()
        {
            gameMap = GameMap.InstantiateGameMap();
            character1 = Character.InstantiateCharacter();
            coords1 = new Vector2Int(1, 1);
            coords2 = new Vector2Int(2, 1);
            coords3 = new Vector2Int(2, 2);
            gameMap.AddCharacterAtCoords(character1, coords1);
        }


        // Test move character
        [Test]
        public void MovesCharacter_R1()
        {
            gameMap.MoveCharacterToCoords(character1, coords2);
            Square squareA = gameMap.GetSquareAtSquareCoords(new Vector2Int(1, 0));
            Square squareB = gameMap.GetSquareAtSquareCoords(new Vector2Int(1, 1));
            Square squareC = gameMap.GetSquareAtSquareCoords(new Vector2Int(2, 0));
            Square squareD = gameMap.GetSquareAtSquareCoords(new Vector2Int(2, 1));
            Square squareE = gameMap.GetSquareAtSquareCoords(new Vector2Int(0, 0));
            Square squareF = gameMap.GetSquareAtSquareCoords(new Vector2Int(0, 1));

            // Check new squares
            foreach (Square square in gameMap.squareCoordsDict.Values)
            {
                if (square.hasCharacter)
                    Debug.Log("square: " + square.coords + " has character: " + square.character.characterName);
            }
            Assert.AreEqual(character1, squareA.character);
            Assert.AreEqual(character1, squareB.character);
            Assert.AreEqual(character1, squareC.character);
            Assert.AreEqual(character1, squareD.character);

            // Check old squares
            Assert.IsNull(squareE.character);
            Assert.IsNull(squareF.character);
        }


        // Test move character
        [Test]
        public void MovesCharacter_R1U1()
        {
            gameMap.MoveCharacterToCoords(character1, coords3);
            Square squareA = gameMap.GetSquareAtSquareCoords(new Vector2Int(1, 1));
            Square squareB = gameMap.GetSquareAtSquareCoords(new Vector2Int(1, 2));
            Square squareC = gameMap.GetSquareAtSquareCoords(new Vector2Int(2, 1));
            Square squareD = gameMap.GetSquareAtSquareCoords(new Vector2Int(2, 2));
            Square squareE = gameMap.GetSquareAtSquareCoords(new Vector2Int(0, 0));
            Square squareF = gameMap.GetSquareAtSquareCoords(new Vector2Int(0, 1));
            Square squareG = gameMap.GetSquareAtSquareCoords(new Vector2Int(1, 0));

            // Check new squares
            Assert.AreEqual(character1, squareA.character);
            Assert.AreEqual(character1, squareB.character);
            Assert.AreEqual(character1, squareC.character);
            Assert.AreEqual(character1, squareD.character);

            // Check old squares
            Assert.IsNull(squareE.character);
            Assert.IsNull(squareF.character);
            Assert.IsNull(squareG.character);
        }
    }
}
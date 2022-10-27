using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;
using GamePieceNS.CharacterNS;
using PlayerNS.PlayerStateNS;

namespace Tests.ScenesNS_Tests
{
    public class Tests_CharacterMovement
    {
        private GameMap gameMap;
        private PlayerStateController playerStateController;
        private Character character1;
        private Vector2Int coords1;
        private Vector2Int coords2;
        private Vector2Int coords3;


        // Setup
        [SetUp]
        public void Setup()
        {
            gameMap = GameMap.InstantiateGameMap();
            playerStateController = new PlayerStateController(gameMap);
            character1 = Character.InstantiateCharacter();
            coords1 = new Vector2Int(1, 1);
            coords2 = new Vector2Int(2, 1);
            coords3 = new Vector2Int(2, 2);
            gameMap.AddCharacterAtCoords(character1, coords1);

            // Left click square
            Square square1 = gameMap.GetSquareAtSquareCoords(Vector2Int.zero);
            playerStateController.LeftClickAtWorldPosition(square1.bounds.center);
        }


        // Test move hover target
        [Test]
        public void HoversMoveTarget()
        {
            Intersection intersection1 = gameMap.GetIntersectionAtIntersectionCoords(new Vector2Int(3, 1));
            playerStateController.HoverAtWorldPosition(intersection1.bounds.center);

            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.ULEven], gameMap.GetSquareTile(new Vector2Int(2, 1)));
            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.UREven], gameMap.GetSquareTile(new Vector2Int(3, 1)));
            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.DREven], gameMap.GetSquareTile(new Vector2Int(3, 0)));
            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.DLEven], gameMap.GetSquareTile(new Vector2Int(2, 0)));
        }


        // Test move hover target
        [Test]
        public void HoversSecondTarget()
        {
            Intersection intersection1 = gameMap.GetIntersectionAtIntersectionCoords(new Vector2Int(3, 1));
            Intersection intersection2 = gameMap.GetIntersectionAtIntersectionCoords(new Vector2Int(3, 3));
            playerStateController.HoverAtWorldPosition(intersection1.bounds.center);
            playerStateController.HoverAtWorldPosition(intersection2.bounds.center);

            // Check second hover
            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.ULEven], gameMap.GetSquareTile(new Vector2Int(2, 3)));
            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.UREven], gameMap.GetSquareTile(new Vector2Int(3, 3)));
            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.DREven], gameMap.GetSquareTile(new Vector2Int(3, 2)));
            Assert.AreEqual(GameTiles.moveTilesEvenSize[Direction.DLEven], gameMap.GetSquareTile(new Vector2Int(2, 2)));
        }


        // Test attack circle outline
        [Test]
        public void CreatesAttackCircle()
        {
            Intersection intersection1 = gameMap.GetIntersectionAtIntersectionCoords(new Vector2Int(3, 1));
            playerStateController.HoverAtWorldPosition(intersection1.bounds.center);

            PlayerStateMove currentState = playerStateController.GetCurrentPlayerState<PlayerStateMove>();
            Assert.AreEqual(new Vector2(intersection1.bounds.center.x, intersection1.bounds.center.y), currentState.attackCircleCollider.center);

            float radius = character1.range * GridArea.SQUARE_WIDTH;
            Assert.AreEqual(radius, currentState.attackCircleCollider.radius);
        }


        // Test get edge line
        [Test]
        public void GetsEdgeLine()
        {
            character1.speed = 2;
            Square square1 = gameMap.GetSquareAtSquareCoords(Vector2Int.zero);
            playerStateController.LeftClickAtWorldPosition(square1.bounds.center);
            Assert.AreEqual(17, gameMap.edgePoints.Count);
            foreach (Vector3 edgePoint in gameMap.edgePoints)
            {
                Debug.Log("Edge point: " + edgePoint);
            }
        }
    }
}
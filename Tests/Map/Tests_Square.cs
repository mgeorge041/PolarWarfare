using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;

namespace Tests.GameMapNS_Tests
{
    public class Tests_Square
    {
        private Square square;


        // Setup
        [SetUp]
        public void Setup()
        {
            square = new Square();
        }


        // Test creates square
        [Test]
        public void CreatesSquare()
        {
            Assert.IsNotNull(square);
            Assert.AreEqual(Vector2Int.zero, square.coords);
        }


        // Test bounds X
        [Test]
        public void CreatesBounds_X()
        {
            Assert.IsFalse(square.bounds.Contains(new Vector3(-0.02f, 0.02f)));
            Assert.IsTrue(square.bounds.Contains(new Vector3(0.02f, 0.02f)));
            Assert.IsTrue(square.bounds.Contains(new Vector3(0.14f, 0.02f)));
            Assert.IsFalse(square.bounds.Contains(new Vector3(0.18f, 0.02f)));
        }


        // Test bounds Y
        [Test]
        public void CreatesBounds_Y()
        {
            Assert.IsFalse(square.bounds.Contains(new Vector3(0.02f, -0.02f)));
            Assert.IsTrue(square.bounds.Contains(new Vector3(0.02f, 0.02f)));
            Assert.IsTrue(square.bounds.Contains(new Vector3(0.02f, 0.14f)));
            Assert.IsFalse(square.bounds.Contains(new Vector3(0.02f, 0.18f)));
        }


        // Test getting square coords
        [Test]
        public void GetsSquareCoords_0()
        {
            List<Vector2Int> squareCoords = square.GetSquareCoords(0);
            Assert.AreEqual(1, squareCoords.Count);
            Assert.Contains(Vector2Int.zero, squareCoords);
        }


        // Test getting square coords
        [Test]
        public void GetsSquareCoords_1()
        {
            List<Vector2Int> squareCoords = square.GetSquareCoords(1);
            Assert.AreEqual(9, squareCoords.Count);
            Assert.Contains(new Vector2Int(-1, -1), squareCoords);
            Assert.Contains(new Vector2Int(-1, 0), squareCoords);
            Assert.Contains(new Vector2Int(-1, 1), squareCoords);
            Assert.Contains(new Vector2Int(0, -1), squareCoords);
            Assert.Contains(new Vector2Int(0, 0), squareCoords);
            Assert.Contains(new Vector2Int(0, 1), squareCoords);
            Assert.Contains(new Vector2Int(1, -1), squareCoords);
            Assert.Contains(new Vector2Int(1, 0), squareCoords);
            Assert.Contains(new Vector2Int(1, -1), squareCoords);
        }


        // Test getting neighbor coords
        [Test]
        public void GetsNeigborCoords()
        {
            List<Vector2Int> neighborCoords = square.GetSquareCoords(1);
            Assert.Contains(new Vector2Int(-1, -1), neighborCoords);
            Assert.Contains(new Vector2Int(-1, 0), neighborCoords);
            Assert.Contains(new Vector2Int(-1, 1), neighborCoords);
            Assert.Contains(new Vector2Int(0, -1), neighborCoords);
            Assert.Contains(new Vector2Int(0, 0), neighborCoords);
            Assert.Contains(new Vector2Int(0, 1), neighborCoords);
            Assert.Contains(new Vector2Int(1, -1), neighborCoords);
            Assert.Contains(new Vector2Int(1, 0), neighborCoords);
            Assert.Contains(new Vector2Int(1, -1), neighborCoords);
        }


        // Test getting move neighbor coords
        [Test]
        public void GetsMoveNeighborCoords()
        {
            List<Vector2Int> neighborCoords = square.GetMovementNeighborCoords();
            Assert.Contains(new Vector2Int(-1, 0), neighborCoords);
            Assert.Contains(new Vector2Int(0, -1), neighborCoords);
            Assert.Contains(new Vector2Int(0, 1), neighborCoords);
            Assert.Contains(new Vector2Int(1, 0), neighborCoords);
        }


        // Test getting corner coords
        [Test]
        public void GetsCornerCoords()
        {
            List<Vector2Int> cornerCoords = square.GetCornerCoords(1);
            Assert.Contains(new Vector2Int(-1, 1), cornerCoords);
            Assert.Contains(new Vector2Int(1, 1), cornerCoords);
            Assert.Contains(new Vector2Int(1, -1), cornerCoords);
            Assert.Contains(new Vector2Int(-1, -1), cornerCoords);
        }
    }
}
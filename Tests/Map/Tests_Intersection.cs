using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameMapNS;

namespace Tests.GameMapNS_Tests
{
    public class Tests_Intersection
    {
        private Intersection intersection;


        // Setup
        [SetUp]
        public void Setup()
        {
            intersection = new Intersection(Vector2Int.one);
        }


        // Test creates square
        [Test]
        public void CreatesIntersection()
        {
            Assert.IsNotNull(intersection);
            Assert.AreEqual(Vector2Int.one, intersection.coords);
        }


        // Test bounds X
        [Test]
        public void CreatesBounds_X()
        {
            Assert.IsFalse(intersection.bounds.Contains(new Vector3(0.06f, 0.1f)));
            Assert.IsTrue(intersection.bounds.Contains(new Vector3(0.1f, 0.1f)));
            Assert.IsTrue(intersection.bounds.Contains(new Vector3(0.22f, 0.1f)));
            Assert.IsFalse(intersection.bounds.Contains(new Vector3(0.26f, 0.1f)));
        }


        // Test bounds Y
        [Test]
        public void CreatesBounds_Y()
        {
            Assert.IsFalse(intersection.bounds.Contains(new Vector3(0.1f, 0.06f)));
            Assert.IsTrue(intersection.bounds.Contains(new Vector3(0.1f, 0.1f)));
            Assert.IsTrue(intersection.bounds.Contains(new Vector3(0.1f, 0.22f)));
            Assert.IsFalse(intersection.bounds.Contains(new Vector3(0.1f, 0.26f)));
        }


        // Test getting neighbor coords
        [Test]
        public void GetsNeigborCoords()
        {
            List<Vector2Int> neighborCoords = intersection.GetSquareCoords(1);
            Assert.AreEqual(4, neighborCoords.Count);
            Assert.Contains(new Vector2Int(0, 0), neighborCoords);
            Assert.Contains(new Vector2Int(0, 1), neighborCoords);
            Assert.Contains(new Vector2Int(1, 1), neighborCoords);
            Assert.Contains(new Vector2Int(1, 0), neighborCoords);
        }


        // Test getting square coords
        [Test]
        public void GetsSquareCoords_0()
        {
            List<Vector2Int> squareCoords = intersection.GetSquareCoords(0);
            Assert.AreEqual(0, squareCoords.Count);
        }


        // Test getting square coords
        [Test]
        public void GetsSquareCoords_1()
        {
            intersection.coords = new Vector2Int(2, 2);
            List<Vector2Int> squareCoords = intersection.GetSquareCoords(1);
            Assert.AreEqual(4, squareCoords.Count);
            Assert.Contains(new Vector2Int(1, 1), squareCoords);
            Assert.Contains(new Vector2Int(1, 2), squareCoords);
            Assert.Contains(new Vector2Int(2, 2), squareCoords);
            Assert.Contains(new Vector2Int(2, 1), squareCoords);
        }


        // Test getting square coords
        [Test]
        public void GetsSquareCoords_2()
        {
            List<Vector2Int> squareCoords = intersection.GetSquareCoords(2);
            Assert.AreEqual(16, squareCoords.Count);
            Assert.Contains(new Vector2Int(-1, 2), squareCoords);
            Assert.Contains(new Vector2Int(-1, 1), squareCoords);
            Assert.Contains(new Vector2Int(-1, 0), squareCoords);
            Assert.Contains(new Vector2Int(-1, -1), squareCoords);
            Assert.Contains(new Vector2Int(0, 2), squareCoords);
            Assert.Contains(new Vector2Int(0, 1), squareCoords);
            Assert.Contains(new Vector2Int(0, 0), squareCoords);
            Assert.Contains(new Vector2Int(0, -1), squareCoords);
            Assert.Contains(new Vector2Int(1, 2), squareCoords);
            Assert.Contains(new Vector2Int(1, 1), squareCoords);
            Assert.Contains(new Vector2Int(1, 0), squareCoords);
            Assert.Contains(new Vector2Int(1, -1), squareCoords);
            Assert.Contains(new Vector2Int(2, 2), squareCoords);
            Assert.Contains(new Vector2Int(2, 1), squareCoords);
            Assert.Contains(new Vector2Int(2, 0), squareCoords);
            Assert.Contains(new Vector2Int(2, -1), squareCoords);
        }


        // Test getting square coords
        [Test]
        public void GetsSquareCoords()
        {
            List<Vector2Int> squareCoords = intersection.squareCoords;
            Assert.AreEqual(4, squareCoords.Count);
            Assert.Contains(new Vector2Int(0, 0), squareCoords);
            Assert.Contains(new Vector2Int(0, 1), squareCoords);
            Assert.Contains(new Vector2Int(1, 1), squareCoords);
            Assert.Contains(new Vector2Int(1, 0), squareCoords);
        }


        // Test getting move neighbor coords
        [Test]
        public void GetsMoveNeighborCoords()
        {
            List<Vector2Int> neighborCoords = intersection.GetMovementNeighborCoords();
            Assert.Contains(new Vector2Int(0, 1), neighborCoords);
            Assert.Contains(new Vector2Int(1, 2), neighborCoords);
            Assert.Contains(new Vector2Int(2, 1), neighborCoords);
            Assert.Contains(new Vector2Int(1, 0), neighborCoords);
        }


        // Test getting corner coords
        [Test]
        public void GetsCornerCoords()
        {
            List<Vector2Int> cornerCoords = intersection.GetCornerCoords(1);
            Assert.Contains(new Vector2Int(0, 1), cornerCoords);
            Assert.Contains(new Vector2Int(1, 1), cornerCoords);
            Assert.Contains(new Vector2Int(1, 0), cornerCoords);
            Assert.Contains(new Vector2Int(0, 0), cornerCoords);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.GameMapNS_Tests
{
    public class Tests_Direction
    {
        private Vector3 pointA;
        private Vector3 pointB;


        // Setup
        [SetUp]
        public void Setup()
        {
            pointA = new Vector3(0.08f, 0.08f);
            pointB = new Vector3(0.24f, 0.08f);
        }

        
        // Test get distance
        [Test]
        public void GetsDistanceBetweenPoints()
        {
            float dist = Direction.GetDistancePoints(pointA, pointB);
            Assert.AreEqual(0.16f, dist);
        }


        // Test get distance between squares
        [Test]
        public void GetsDistanceBetweenSquares_X()
        {
            Square squareA = new Square(Vector2Int.zero);
            Square squareB = new Square(new Vector2Int(3, 0));
            int dist = Direction.GetDistanceBetweenSquares(squareA, squareB);
            Assert.AreEqual(3, dist);
        }


        // Test get distance between squares
        [Test]
        public void GetsDistanceBetweenSquares_Y()
        {
            Square squareA = new Square(Vector2Int.zero);
            Square squareB = new Square(new Vector2Int(0, 3));
            int dist = Direction.GetDistanceBetweenSquares(squareA, squareB);
            Assert.AreEqual(3, dist);
        }

        // Test get distance between squares
        [Test]
        public void GetsDistanceBetweenSquares_XY()
        {
            Square squareA = new Square(Vector2Int.zero);
            Square squareB = new Square(Vector2Int.one);
            int dist = Direction.GetDistanceBetweenSquares(squareA, squareB);
            Assert.AreEqual(2, dist);
        }

        // Test get distance between squares
        [Test]
        public void GetsDistanceBetweenSquares_Same()
        {
            Square squareA = new Square(Vector2Int.zero);
            Square squareB = new Square(Vector2Int.zero);
            int dist = Direction.GetDistanceBetweenSquares(squareA, squareB);
            Assert.AreEqual(0, dist);
        }


        // Test get next square direction
        [Test]
        public void GetsNextSquareDirection_Clockwise()
        {
            Vector2Int nextDirection = Direction.GetNextSquareClockwiseDirection(Direction.R);
            Assert.AreEqual(Direction.D, nextDirection);
        }


        // Test get next square direction
        [Test]
        public void GetsNextSquareDirection_Clockwise_Cross0Index()
        {
            Vector2Int nextDirection = Direction.GetNextSquareClockwiseDirection(Direction.L);
            Assert.AreEqual(Direction.U, nextDirection);
        }


        // Test get next square direction
        [Test]
        public void GetsNextSquareDirection_CounterClockwise()
        {
            Vector2Int nextDirection = Direction.GetNextSquareCounterClockwiseDirection(Direction.R);
            Assert.AreEqual(Direction.U, nextDirection);
        }


        // Test get next square direction
        [Test]
        public void GetsNextSquareDirection_CounterClockwise_Cross0Index()
        {
            Vector2Int nextDirection = Direction.GetNextSquareCounterClockwiseDirection(Direction.U);
            Assert.AreEqual(Direction.L, nextDirection);
        }
    }
}
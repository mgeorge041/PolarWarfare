using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.GameMapNS_Tests
{
    public class Tests_Circle
    {
        private Circle circleA;
        

        // Setup
        [SetUp]
        public void Setup()
        {
            Vector2 center = Vector2.zero;
            float radius = 5;
            circleA = new Circle(center, radius);
        }


        // Test create circle
        [Test]
        public void CreatesCircle()
        {
            Assert.IsNotNull(circleA);
            Assert.AreEqual(Vector2.zero, circleA.center);
            Assert.AreEqual(5, circleA.radius);
        }


        // Test contains point
        [Test]
        public void ContainsPoint_Inside()
        {
            Vector2 pointA = new Vector2(1, 1);
            Assert.IsTrue(circleA.ContainsPoint(pointA));
        }


        // Test contains point
        [Test]
        public void ContainsPoint_Outside()
        {
            Vector2 pointA = new Vector2(6, 6);
            Assert.IsFalse(circleA.ContainsPoint(pointA));
        }


        // Test overlaps rectangle
        [Test]
        public void OverlapsRectangle_NoIntersect()
        {

        }


        // Test overlaps rectangle
        [Test]
        public void OverlapsRectangle_Intersect_RectangleWithin()
        {
            Vector2 pointA = new Vector2(3, 3);
            Vector2 pointB = new Vector2(-3, 3);
            Vector2 pointC = new Vector2(-3, -3);
            Vector2 pointD = new Vector2(3, -3);
            Rectangle rectangle = new Rectangle(new Vector2[] { pointA, pointB, pointC, pointD });
            Assert.IsTrue(circleA.OverlapsRectangle(rectangle));
        }


        // Test overlaps rectangle
        [Test]
        public void OverlapsRectangle_Intersect_CircleWithin()
        {
            Vector2 pointA = new Vector2(6, 6);
            Vector2 pointB = new Vector2(-6, 6);
            Vector2 pointC = new Vector2(-6, -6);
            Vector2 pointD = new Vector2(6, -6);
            Rectangle rectangle = new Rectangle(new Vector2[] { pointA, pointB, pointC, pointD });
            Assert.IsTrue(circleA.OverlapsRectangle(rectangle));
        }


        // Test overlaps rectangle
        [Test]
        public void OverlapsRectangle_Intersect_CornerWithin()
        {
            Vector2 pointA = new Vector2(3, 3);
            Vector2 pointB = new Vector2(3, 6);
            Vector2 pointC = new Vector2(6, 6);
            Vector2 pointD = new Vector2(6, 3);
            Rectangle rectangle = new Rectangle(new Vector2[] { pointA, pointB, pointC, pointD });
            Assert.IsTrue(circleA.OverlapsRectangle(rectangle));
        }


        // Test overlaps rectangle
        [Test]
        public void OverlapsRectangle_Intersect_CornerOutside()
        {
            Vector2 pointA = new Vector2(0, 6);
            Vector2 pointB = new Vector2(2, 8);
            Vector2 pointC = new Vector2(8, 2);
            Vector2 pointD = new Vector2(6, 0);
            Rectangle rectangle = new Rectangle(new Vector2[] { pointA, pointB, pointC, pointD });
            Assert.IsTrue(circleA.OverlapsRectangle(rectangle));
        }
    }
}
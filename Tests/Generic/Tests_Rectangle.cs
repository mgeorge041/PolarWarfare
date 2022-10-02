using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.GenericTests
{
    public class Tests_Rectangle
    {
        private Rectangle rectangle;


        // Setup
        [SetUp]
        public void Setup()
        {
            Vector2 pointA = new Vector2(0, 0);
            Vector2 pointB = new Vector2(0, 2);
            Vector2 pointC = new Vector2(2, 2);
            Vector2 pointD = new Vector2(2, 0);
            rectangle = new Rectangle(new Vector2[] { pointA, pointB, pointC, pointD });
        }


        // Test create rectangle
        [Test]
        public void CreatesRectangle()
        {
            Assert.IsNotNull(rectangle);
            Assert.AreEqual(4, rectangle.area);
            Assert.AreEqual(0, rectangle.xMin);
            Assert.AreEqual(2, rectangle.xMax);
            Assert.AreEqual(0, rectangle.yMin);
            Assert.AreEqual(2, rectangle.yMax);
        }


        // Test contains point
        [Test]
        public void ContainsPoint_Outside()
        {
            Vector2 pointE = new Vector2(3, 3);
            Assert.IsFalse(rectangle.ContainsPoint(pointE));
        }


        // Test contains point
        [Test]
        public void ContainsPoint_Inside()
        {
            Vector2 pointE = new Vector2(1, 1);
            Assert.IsTrue(rectangle.ContainsPoint(pointE));
        }


        // Test contains point
        [Test]
        public void ContainsPoint_OnEdge()
        {
            Vector2 pointE = new Vector2(2, 1);
            Assert.IsTrue(rectangle.ContainsPoint(pointE));
        }


        // Test overlaps
        [Test]
        public void Overlaps_NoIntersect()
        {
            Vector2 pointE = new Vector2(0, 3);
            Vector2 pointF = new Vector2(0, 4);
            Vector2 pointG = new Vector2(4, 4);
            Vector2 pointH = new Vector2(4, 3);
            Rectangle rectangleB = new Rectangle(new Vector2[] { pointE, pointF, pointG, pointH });
            Assert.IsFalse(rectangle.Overlaps(rectangleB));
        }


        // Test overlaps
        [Test]
        public void Overlaps_Normal()
        {
            Vector2 pointE = new Vector2(1, 1);
            Vector2 pointF = new Vector2(1, 3);
            Vector2 pointG = new Vector2(3, 3);
            Vector2 pointH = new Vector2(3, 1);
            Rectangle rectangleB = new Rectangle(new Vector2[] { pointE, pointF, pointG, pointH });
            Assert.IsTrue(rectangle.Overlaps(rectangleB));
        }


        // Test overlaps
        [Test]
        public void Overlaps_OtherRectangleWithin()
        {
            Vector2 pointE = new Vector2(1, 1);
            Vector2 pointF = new Vector2(1, 1.5f);
            Vector2 pointG = new Vector2(1.5f, 1.5f);
            Vector2 pointH = new Vector2(1.5f, 1);
            Rectangle rectangleB = new Rectangle(new Vector2[] { pointE, pointF, pointG, pointH });
            Assert.IsTrue(rectangle.Overlaps(rectangleB));
        }


        // Test overlaps
        [Test]
        public void Overlaps_WithinOtherRectangle()
        {
            Vector2 pointE = new Vector2(-1, -1);
            Vector2 pointF = new Vector2(-1, 3);
            Vector2 pointG = new Vector2(3, 3);
            Vector2 pointH = new Vector2(3, -1);
            Rectangle rectangleB = new Rectangle(new Vector2[] { pointE, pointF, pointG, pointH });
            Assert.IsTrue(rectangle.Overlaps(rectangleB));
        }


        // Test overlaps
        [Test]
        public void Overlaps_EdgesAlign()
        {
            Vector2 pointE = new Vector2(2, 0);
            Vector2 pointF = new Vector2(2, 2);
            Vector2 pointG = new Vector2(4, 2);
            Vector2 pointH = new Vector2(4, 0);
            Rectangle rectangleB = new Rectangle(new Vector2[] { pointE, pointF, pointG, pointH });
            Assert.IsTrue(rectangle.Overlaps(rectangleB));
        }
    }
}
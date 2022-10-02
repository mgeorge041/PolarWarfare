using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.GenericTests
{
    public class Tests_LineSegment
    {
        private LineSegment segmentA;
        private Vector2 pointA;
        private Vector2 pointB;


        // Setup
        [SetUp]
        public void Setup()
        {
            pointA = new Vector2(0, 0);
            pointB = new Vector2(4, 4);
            segmentA = new LineSegment(pointA, pointB);

        }


        // Test line segment created
        [Test]
        public void CreatesLineSegment()
        {
            Assert.IsNotNull(segmentA);
            Assert.AreEqual(pointA, segmentA.pointA);
            Assert.AreEqual(pointB, segmentA.pointB);
            Assert.AreEqual(new Vector2(4, 4), segmentA.direction);
        }


        // Test get length
        [Test]
        public void GetsLength()
        {
            Assert.AreEqual(Mathf.Sqrt(32), segmentA.length);
        }


        // Test cross
        [Test]
        public void GetsCross()
        {
            Vector2 pointC = new Vector2(3, 1);
            Vector2 pointD = new Vector2(5, -1);
            Assert.AreEqual(-8, LineSegment.Cross(pointC, pointD));
        }


        // Test point potentially on segment
        [Test]
        public void PointPossiblyOnSegment()
        {
            Vector2 pointP = new Vector2(3, 1);
            Vector2 pointQ = new Vector2(5, 1);
            Vector2 pointR = new Vector2(3, -1);
            Vector2 pointS = new Vector2(1, 3);
            Vector2 pointT = new Vector2(-1, 3);
            Vector2 pointU = new Vector2(1, 5);

            Assert.IsTrue(segmentA.PointPossiblyOnSegment(pointP));
            Assert.IsFalse(segmentA.PointPossiblyOnSegment(pointQ));
            Assert.IsFalse(segmentA.PointPossiblyOnSegment(pointR));
            Assert.IsTrue(segmentA.PointPossiblyOnSegment(pointS));
            Assert.IsFalse(segmentA.PointPossiblyOnSegment(pointT));
            Assert.IsFalse(segmentA.PointPossiblyOnSegment(pointU));
        }


        // Test intersects line segment
        [Test]
        public void IntersectsLineSegment()
        {
            /* Case 1: No intersect
             * Case 2: Intersect
             * Case 3: Intersect at endpoint
             * Case 4: Collinear
             */

            Vector2 pointC = new Vector2(3, 1);
            Vector2 pointD = new Vector2(5, -1);
            
            Vector2 pointE = new Vector2(3, 4);
            Vector2 pointF = new Vector2(4, 4);
            Vector2 pointG = new Vector2(4, 5);
            Vector2 pointH = new Vector2(3, 3);
            Vector2 pointI = new Vector2(5, 5);
            LineSegment segmentB = new LineSegment(pointC, pointD);
            LineSegment segmentC = new LineSegment(pointC, pointE);
            LineSegment segmentD = new LineSegment(pointF, pointG);
            LineSegment segmentE = new LineSegment(pointH, pointI);

            Assert.IsFalse(segmentA.IntersectsLineSegment(segmentB));
            Assert.IsTrue(segmentA.IntersectsLineSegment(segmentC));
            Assert.IsTrue(segmentA.IntersectsLineSegment(segmentD));
            Assert.IsTrue(segmentA.IntersectsLineSegment(segmentE));
        }
    }
}
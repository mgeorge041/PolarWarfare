using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerNS
{
    public class Tests_PlayerState
    {
        private Vector3 vector1;
        private Vector3 vector2;
        private float halfWidth = 0.04f;


        // Setup
        [SetUp]
        public void Setup()
        {
            vector1 = Vector3.zero;
            vector2 = new Vector3(2, 1, 0);
        }


        // Test direction
        [Test]
        public void GetsDirection()
        {
            Vector3 direction = vector2 - vector1;
            Debug.Log("direction: " + direction);
            Vector3 normal = direction.normalized;
            Debug.Log("normal: " + normal);

            Vector3 normalEnd = normal * 0.48f;
            Debug.Log("normal End: " + normalEnd);
            Debug.Log("normal end length: " + normalEnd.magnitude);

            Vector3 endPoint = normalEnd + vector1;
            Debug.Log("end point: " + endPoint);

            Vector3 inverseNormal = new Vector3(normal.y, -normal.x, 0);
            Debug.Log("inverse: " + inverseNormal);

            Vector3 inverseEnd = inverseNormal * halfWidth + vector1;
            Debug.Log("inverse end: " + inverseEnd);

            Vector3 inverseLeftEnd = -inverseNormal * halfWidth + vector1;
            Debug.Log("inverse left end: " + inverseLeftEnd);
        }


        // Test line segments
        [Test]
        public void GetsIntersects()
        {
            Vector2 pointA = new Vector2(0, 0);
            Vector2 pointB = new Vector2(4, 4);
            Vector2 pointC = new Vector2(3, 1);
            Vector2 pointD = new Vector2(5, -1);
            
            float slope1 = (pointA.y - pointB.y) / (pointA.x - pointB.x);
            float slope2 = (pointC.y - pointD.y) / (pointC.x - pointD.x);
            Debug.Log("slope 1: " + slope1);
            Debug.Log("slope 2: " + slope2);

            float pointPx = (slope1 * pointA.x - pointA.y - slope2 * pointC.x + pointC.y) / (slope1 - slope2);
            float pointPy = slope1 * (pointPx - pointA.x) + pointA.y;
            Debug.Log("point px: " + pointPx);
            Debug.Log("point py: " + pointPy);
            Assert.IsTrue((pointPx <= pointA.x && pointPx >= pointB.x) || (pointPx <= pointB.x && pointPx >= pointA.x));
            Assert.IsFalse((pointPx <= pointC.x && pointPx >= pointD.x) || (pointPx <= pointD.x && pointPx >= pointC.x));
            Assert.IsTrue((pointPy <= pointA.y && pointPy >= pointB.y) || (pointPy <= pointB.y && pointPy >= pointA.y));
            Assert.IsFalse((pointPy <= pointC.y && pointPy >= pointD.y) || (pointPy <= pointD.y && pointPy >= pointC.y));
        }
    }
}
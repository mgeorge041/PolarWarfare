using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{
    public Vector2 center { get; private set; }
    public float radius { get; private set; }


    // Constructor
    public Circle()
    {
        center = Vector2.zero;
        radius = 0;
    }
    public Circle(Vector2 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }


    // Update center
    public void UpdateCenter(Vector2 center)
    {
        this.center = center;
    }


    // Update radius
    public void UpdateRadius(float radius)
    {
        this.radius = radius;
    }


    // Get whether point in circle
    public bool ContainsPoint(Vector2 point)
    {
        Vector2 dist = point - center;
        if (dist.magnitude <= radius)
            return true;
        return false;
    }


    // Get whether overlaps rectangle
    public bool OverlapsRectangle(Rectangle rectangle)
    {
        // Check center
        if (rectangle.ContainsPoint(center))
            return true;

        // Check rectangle corners
        foreach (Vector2 corner in rectangle.vertices)
        {
            if (ContainsPoint(corner))
                return true;
        }

        // Check line segments
        foreach (LineSegment segment in rectangle.segments)
        {
            // Get normal vector to each rectangle line segment
            Vector2 normal = new Vector2(segment.direction.y, -segment.direction.x);
            Vector2 normalRadius = normal.normalized * radius;

            // Check if normal intersects segment
            LineSegment newSegment = new LineSegment(center, center + normalRadius);
            if (newSegment.IntersectsLineSegment(segment))
                return true;
        }
        return false;
    }


    // Print
    public override string ToString()
    {
        return center + "; r: " + radius;
    }
}

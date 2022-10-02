using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle
{
    public Vector2[] vertices;
    public LineSegment[] segments { get; private set; }
    public float xMin { get; private set; }
    public float yMin { get; private set; }
    public float xMax { get; private set; }
    public float yMax { get; private set; }
    public float area { get; private set; }


    // Constructor
    public Rectangle(Vector2[] vertices)
    {
        Initialize(vertices);
    }


    // Initialize
    private void Initialize(Vector2[] vertices)
    {
        if (vertices.Length != 4)
            throw new System.ArgumentException("Must be 4 points in rectangle.");
        this.vertices = vertices;
        SetMinMax();
        CreateSegments();
        SetArea();
    }


    // Update rectangle
    public void Update(Vector2[] vertices)
    {
        Initialize(vertices);
    }


    // Set min and max
    private void SetMinMax()
    {
        if (vertices.Length == 0)
            return;

        xMin = vertices[0].x;
        yMin = vertices[0].y;
        xMax = vertices[0].x;
        yMax = vertices[0].y;
        foreach (Vector2 vertex in vertices) 
        {
            if (vertex.x < xMin)
                xMin = vertex.x;
            if (vertex.y < yMin)
                yMin = vertex.y;
            if (vertex.x > xMax)
                xMax = vertex.x;
            if (vertex.y > yMax)
                yMax = vertex.y;
        }
    }


    // Get vertices
    public Vector3[] GetVertices()
    {
        return new Vector3[]
        {
            vertices[0],
            vertices[1],
            vertices[2],
            vertices[3],
        };
    }


    // Create line segments
    private void CreateSegments()
    {
        segments = new LineSegment[]
        {
            new LineSegment(vertices[0], vertices[1]),
            new LineSegment(vertices[1], vertices[2]),
            new LineSegment(vertices[2], vertices[3]),
            new LineSegment(vertices[3], vertices[0]),
        };

        //if (Vector2.Dot(segments[0].direction, -segments[3].direction) != 0)
        //    throw new System.ArgumentException("Rectangle segments not at 90 degree angles.");
    }


    // Set area
    private void SetArea()
    {
        area = segments[0].length * segments[1].length;
    }


    // Get whether point in rectangle
    public bool ContainsPoint(Vector2 point)
    {
        Vector2 vectorPoint = point - vertices[0];
        float dot1 = Vector2.Dot(vectorPoint, segments[0].direction);
        float dot2 = Vector2.Dot(segments[0].direction, segments[0].direction);
        float dot3 = Vector2.Dot(vectorPoint, -segments[3].direction);
        float dot4 = Vector2.Dot(-segments[3].direction, -segments[3].direction);

        if ((0 <= dot1 && dot1 <= dot2) && (0 <= dot3 && dot3 <= dot4))
            return true;
        return false;
    }


    // Determine if overlaps
    public bool Overlaps(Rectangle rectangle)
    {
        if (xMax < rectangle.xMin)
            return false;

        if (yMax < rectangle.yMin)
            return false;

        if (xMin > rectangle.xMax)
            return false;

        if (yMin > rectangle.yMax)
            return false;

        // Check line segment intersections
        foreach (LineSegment segment in segments)
        {
            foreach (LineSegment otherSegment in rectangle.segments)
            {
                if (segment.IntersectsLineSegment(otherSegment))
                    return true;
            }
        }

        // Check all points within other rectangle
        if (LineSegment.ValueBetween(vertices[0].x, rectangle.xMin, rectangle.xMax))
        {
            foreach (Vector2 point in vertices)
            {
                if (!rectangle.ContainsPoint(point))
                    return false;
            }
        }

        // Check all other rectangle points within this rectangle
        else
        {
            foreach (Vector2 point in rectangle.vertices)
            {
                if (!ContainsPoint(point))
                    return false;
            }
        }
        
        return true;
    }


    // Get whether overlaps circle
    public bool OverlapsCircle(Circle circle)
    {
        // Check center
        if (ContainsPoint(circle.center))
            return true;

        // Check line segments
        foreach (LineSegment segment in segments)
        {
            // Get normal vector to each rectangle line segment
            Vector2 normal = new Vector2(segment.direction.y, -segment.direction.x);
            Vector2 normalRadius = normal.normalized * circle.radius;

            // Check if normal intersects segment
            LineSegment newSegment = new LineSegment(circle.center, circle.center + normalRadius);
            if (newSegment.IntersectsLineSegment(segment))
                return true;
        }
        return false;
    }


    // Print string
    public override string ToString()
    {
        return "(" + vertices[0] + ", " + vertices[1] + ", " + vertices[2] + ", " + vertices[3] + ")";
    }
}

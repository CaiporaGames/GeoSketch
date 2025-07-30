using System.Collections.Generic;
using UnityEngine;

public static class ShapeGenerator
{
    public static List<Vector3> GenerateEquilateralTriangle(float size = 1f)
    {
        float height = Mathf.Sqrt(3f) / 2f * size;
        return new List<Vector3>
        {
            new Vector3(-size / 2f, -height / 3f),
            new Vector3(size / 2f, -height / 3f),
            new Vector3(0f, height * 2f / 3f)
        };
    }
    public static List<Vector3> GenerateSquare(float size = 1f)
    {
        float half = size / 2f;
        return new List<Vector3>
        {
            new Vector3(-half, -half),
            new Vector3(half, -half),
            new Vector3(half, half),
            new Vector3(-half, half)
        };
    }
    public static List<Vector3> GenerateRegularPolygon(int sides, float radius = 1f)
    {
        List<Vector3> points = new();
        float angleStep = 360f / sides;

        for (int i = 0; i < sides; i++)
        {
            float angle = Mathf.Deg2Rad * (angleStep * i);
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            points.Add(new Vector3(x, y, 0));
        }

        return points;
    }

}
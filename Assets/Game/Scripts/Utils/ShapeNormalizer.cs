using System.Collections.Generic;
using UnityEngine;

public static class ShapeNormalizer
{
    public static List<Vector2> Normalize(List<Vector3> points)
    {
        var bounds = CalculateBounds(points);
        Vector3 center = bounds.center;
        float maxDim = Mathf.Max(bounds.size.x, bounds.size.y);

        List<Vector2> result = new();
        foreach (var p in points)
            result.Add((p - center) / maxDim);

        return result;
    }

    private static Bounds CalculateBounds(List<Vector3> points)
    {
        if (points.Count == 0) return new Bounds();

        Bounds bounds = new Bounds(points[0], Vector3.zero);
        for (int i = 1; i < points.Count; i++)
            bounds.Encapsulate(points[i]);

        return bounds;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ShapeModel
{
    public List<Vector3> Points { get; } = new();

    public void AddPoint(Vector3 point)
    {
        Points.Add(point);
    }

    public void Clear()
    {
        Points.Clear();
    }

    public int SideCount => Points.Count;

    public bool IsClosed(float tolerance = 0.1f)
    {
        if (Points.Count < 3) return false;
        return Vector3.Distance(Points[0], Points[^1]) <= tolerance;
    }

    public List<float> GetAngles()
    {
        var angles = new List<float>();

        for (int i = 0; i < Points.Count; i++)
        {
            Vector2 a = Points[(i - 1 + Points.Count) % Points.Count] - Points[i];
            Vector2 b = Points[(i + 1) % Points.Count] - Points[i];

            float angle = Vector2.Angle(a, b);
            angles.Add(angle);
        }

        return angles;
    }
}

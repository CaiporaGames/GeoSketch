using System.Collections.Generic;
using UnityEngine;

public class ShapeTemplateComparer
{
    private readonly SOShapeTemplate _template;

    public ShapeTemplateComparer(SOShapeTemplate template)
    {
        _template = template;
    }

    public bool CompareToShape(ShapeModel drawnShape)
    {
        if (drawnShape.SideCount != _template.cornerPoints.Count)
            return false;

        if (!drawnShape.IsClosed(_template.closureTolerance))
            return false;

        // Compare angles
        List<float> drawnAngles = drawnShape.GetAngles();
        for (int i = 0; i < drawnAngles.Count; i++)
        {
            float templateAngle = CalculateTemplateAngle(i);
            float diff = Mathf.Abs(drawnAngles[i] - templateAngle);

            if (diff > _template.angleTolerance)
                return false;
        }

        return true;
    }

    private float CalculateTemplateAngle(int i)
    {
        var points = _template.cornerPoints;

        Vector2 a = points[(i - 1 + points.Count) % points.Count] - points[i];
        Vector2 b = points[(i + 1) % points.Count] - points[i];

        return Vector2.Angle(a, b);
    }
}

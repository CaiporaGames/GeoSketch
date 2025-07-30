using System.Collections.Generic;
using UnityEngine;

public class ShapeTemplateComparer
{
    private readonly SOShapeTemplate _template;

    public ShapeTemplateComparer(SOShapeTemplate template)
    {
        _template = template;
    }
    /* 
         var drawn = ShapeNormalizer.Normalize(drawnShape.Points);
    var template = ShapeNormalizer.Normalize(_template.GetCornerPoints());

    if (drawn.Count != template.Count)
        return false;

    float maxDistance = 0.2f; // Adjust tolerance here

    for (int i = 0; i < drawn.Count; i++)
    {
        if (Vector2.Distance(drawn[i], template[i]) > maxDistance)
            return false;
    }

    return true;
     */
    public bool CompareToShape(ShapeModel drawnShape)
    {
        var drawn = ShapeNormalizer.Normalize(drawnShape.Points);
        var template = ShapeNormalizer.Normalize(_template.GetCornerPoints());

        if (drawn.Count != template.Count)
            return false;

        float maxDistance = 0.2f; // Adjust tolerance here
        if (drawnShape.SideCount != _template.GetCornerPoints().Count)
            return false;

        if (!drawnShape.IsClosed(_template.closureTolerance))
            return false;
            
        for (int i = 0; i < drawn.Count; i++)
        {
            if (Vector2.Distance(drawn[i], template[i]) > maxDistance)
                return false;
        }

       /*  // Compare angles
          List<float> drawnAngles = drawnShape.GetAngles();
          for (int i = 0; i < drawnAngles.Count; i++)
          {
              float templateAngle = CalculateTemplateAngle(i);
              float diff = Mathf.Abs(drawnAngles[i] - templateAngle);

              if (diff > _template.angleTolerance)
                  return false;
          } */

        return true;
    }

    private float CalculateTemplateAngle(int i)
    {
        var points = _template.GetCornerPoints();

        Vector2 a = points[(i - 1 + points.Count) % points.Count] - points[i];
        Vector2 b = points[(i + 1) % points.Count] - points[i];

        return Vector2.Angle(a, b);
    }
}

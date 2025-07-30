using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects / Shape Template", fileName = "Shape Template")]
public class SOShapeTemplate : ScriptableObject
{
    public string shapeName;
    public float closureTolerance = 0.2f;
    public float angleTolerance = 10f; // degrees
    public ShapeType type;
    public float size = 1f;

    public List<Vector3> GetCornerPoints()
    {
        return type switch
        {
            ShapeType.EquilateralTriangle => ShapeGenerator.GenerateEquilateralTriangle(size),
            ShapeType.Square => ShapeGenerator.GenerateSquare(size),
            ShapeType.Pentagon => ShapeGenerator.GenerateRegularPolygon(5, size),
            ShapeType.Hexagon => ShapeGenerator.GenerateRegularPolygon(6, size),
            _ => new List<Vector3>()
        };
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects / Shape Template", fileName = "Shape Template")]
public class SOShapeTemplate : ScriptableObject
{
    public string shapeName;
    public List<Vector3> cornerPoints = new();
    public float closureTolerance = 0.2f;
    public float angleTolerance = 10f; // degrees
}

using UnityEngine;

public class ShapeCompletionChecker
{
    private readonly ShapeModel _model;

    public ShapeCompletionChecker(ShapeModel model)
    {
        _model = model;
    }

    public bool IsShapeComplete(float closureTolerance = 0.1f)
    {
        Debug.Log($"Checking shape completion: { _model.SideCount } sides, closed: { _model.IsClosed(closureTolerance) }");
        return _model.SideCount >= 3 && _model.IsClosed(closureTolerance);
    }

    public string[] ClassifyAngles()
    {
        var angles = _model.GetAngles();
        string[] types = new string[angles.Count];

        for (int i = 0; i < angles.Count; i++)
        {
            if (angles[i] < 90f)
                types[i] = "Acute";
            else if (Mathf.Approximately(angles[i], 90f))
                types[i] = "Right";
            else
                types[i] = "Obtuse";
        }

        return types;
    }
}

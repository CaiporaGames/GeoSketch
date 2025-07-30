using System.Collections.Generic;
using UnityEngine;

public class SnapPointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _snapPointPrefab;
    [SerializeField] private ShapeDrawingInputHandler _drawingHandler;
    [SerializeField] private SOShapeTemplate _template;

    private readonly List<Transform> _spawnedPoints = new();

    void Start()
    {
        SpawnSnapPoints();
    }

    public void SpawnSnapPoints()
    {
        ClearExisting();

        List<Vector3> corners = _template.GetCornerPoints();
        foreach (var pos in corners)
        {
            var snap = Instantiate(_snapPointPrefab, pos, Quaternion.identity, transform);
            _spawnedPoints.Add(snap.transform);
        }

        _drawingHandler.SetSnapTargets(_spawnedPoints);
    }

    private void ClearExisting()
    {
        foreach (var snap in _spawnedPoints)
        {
            if (snap != null)
                Destroy(snap.gameObject);
        }
        _spawnedPoints.Clear();
    }
}

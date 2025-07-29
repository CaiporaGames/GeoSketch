using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ShapeDrawingInputHandler : MonoBehaviour, IGameSystem
{
    [SerializeField] private SOShapeTemplate _currentTemplate;
    [SerializeField] private float _minPointDistance = 0.1f;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _snapTolerance = 0.2f;
    [SerializeField] private List<Transform> _snapTargets;

    private readonly List<Vector3> _points = new();
    public IReadOnlyList<Vector3> DrawnPoints => _points;
    private IUpdateManager _updater;
    private Action<float> _onUpdate;

    private ShapeTemplateComparer _templateComparer;
    private ShapeModel _shapeModel;
    private ShapeCompletionChecker _completionChecker;
    private Camera _mainCamera;
    public async UniTask InitializeAsync()
    {
        _updater = ServiceLocator.Resolve<IUpdateManager>();
        _onUpdate = OnUpdate;
        _updater.Register(_onUpdate);
        _mainCamera = Camera.main;
        _shapeModel = new ShapeModel();
        _completionChecker = new ShapeCompletionChecker(_shapeModel);
        _templateComparer = new ShapeTemplateComparer(_currentTemplate);
        await UniTask.Yield();
    }

    private void OnUpdate(float deltaTime)
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 worldPos = GetMouseWorldPosition(); // your logic
            _shapeModel.AddPoint(worldPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_completionChecker.IsShapeComplete())
            {
                Debug.Log("✅ Shape complete!");
                OnPlayerFinishedDrawing();
                var angleTypes = _completionChecker.ClassifyAngles();
                foreach (var type in angleTypes)
                    Debug.Log(type); // later you'll color-code these
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        // Try snapping
        foreach (var snapPoint in _snapTargets)
        {
            if (Vector2.Distance(worldPos, snapPoint.position) < _snapTolerance)
            {
                worldPos = snapPoint.position;
                break;
            }
        }
        if (_points.Count == 0 || Vector3.Distance(_points[^1], worldPos) > _minPointDistance)
        {
            _points.Add(worldPos);
            UpdateLine();
        }

        return worldPos;
    }

    private void UpdateLine()
    {
        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPositions(_points.ToArray());
    }

    private void OnDisable()
    {
        // stop getting updates when it’s disabled
        _updater.Unregister(_onUpdate);
    }
    
    private void OnPlayerFinishedDrawing()
    {
        if (_templateComparer.CompareToShape(_shapeModel))
        {
            Debug.Log("🎉 Correct Shape!");
        }
        else
        {
            Debug.Log("❌ Try Again!");
        }
    }
}

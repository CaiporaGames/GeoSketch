using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ShapeDrawingInputHandler : MonoBehaviour, IGameSystem
{
    private readonly List<Vector3> _points = new();
    [SerializeField] private float _minPointDistance = 0.1f;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _snapTolerance = 0.2f;
    [SerializeField] private List<Transform> _snapTargets;

    public IReadOnlyList<Vector3> DrawnPoints => _points;
    private IUpdateManager _updater;
    private Action<float> _onUpdate;

    private Camera _mainCamera;
    public async UniTask InitializeAsync()
    {
        _updater = ServiceLocator.Resolve<IUpdateManager>();
        _onUpdate = OnUpdate;
        _updater.Register(_onUpdate);
        _mainCamera = Camera.main;
        await UniTask.Yield();
    }

    private void OnUpdate(float deltaTime)
    {
        if (Input.GetMouseButton(0))
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
        }
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
}

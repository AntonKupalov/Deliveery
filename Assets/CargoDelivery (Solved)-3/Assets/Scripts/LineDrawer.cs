using System;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public event Action Drawn;
    
    [SerializeField] 
    private float _minDistanceBetweenPoints = 0.1f;
    [SerializeField] 
    private float _depth = 10;

    private LineRenderer _lineRenderer;
    private Camera _camera;
    private bool _canDraw = true;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!_canDraw)
        {
            return;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _canDraw = false;
            Drawn?.Invoke();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            _lineRenderer.positionCount = 1;
        }

        if (!Input.GetMouseButton(0))
        {
            return;
        }        
        
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _depth);
        var drawingPoint = _camera.ScreenToWorldPoint(mousePosition);
        
        if (Vector3.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), drawingPoint) > _minDistanceBetweenPoints)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, drawingPoint);
        }
    }

    public Vector3[] GetPoints()
    {
        var points = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(points);
        return points;
    }
}
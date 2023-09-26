using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] 
    private LineDrawer _lineDrawer;
    
    [SerializeField] 
    private float _speed = 3;

    private Vector3[] _wayPoints;
    private Vector3 _endPosition;
    private int _currentPointIndex;

    private void Awake()
    {
        _lineDrawer.Drawn += OnLineDrawn;
    }

    public void Stop()
    {
        _wayPoints = null;
    }

    private void OnLineDrawn()
    {
        _wayPoints = _lineDrawer.GetPoints();
        SetNextPoint();
    }
    
    private void SetNextPoint()
    {
        var nextPointIndex = _currentPointIndex++;
        if (nextPointIndex >= _wayPoints.Length)
        {
            return;
        }
        _endPosition = _wayPoints[nextPointIndex];
    }

    private void Update()
    {
        if (_wayPoints == null)
        {
            return;
        }
        
        if (Vector3.Distance(transform.position, _endPosition) < 0.1f)
        {
            SetNextPoint();
        }
        
        transform.position = Vector3.MoveTowards(transform.position, _endPosition, 
            _speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        _lineDrawer.Drawn -= OnLineDrawn;
    }
}
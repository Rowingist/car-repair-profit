using System.Collections;
using UnityEngine;

public class MoneyDropAnimation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _resetTime;

    private Transform _endPoint;
    private float _elapsedTime;

    private void Start()
    {
        transform.position = _startPoint.position;
    }

    public void SetPositionPoint(Transform end)
    {
        _endPoint = end;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_endPoint)
        {
            transform.position = Vector3.Lerp(transform.position, _endPoint.position, Time.deltaTime * _speed);
        }

        if(_elapsedTime >= _resetTime)
        {
            _elapsedTime = 0;
            transform.position = _startPoint.position;
        }
    }
}

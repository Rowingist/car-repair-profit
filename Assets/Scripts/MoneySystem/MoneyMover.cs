using UnityEngine;

public class MoneyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _startPosition;
    private Vector3 _tragetPoint;
    private bool _disableAfterReach;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            _tragetPoint, Time.deltaTime * _speed);

        if(Vector3.Distance(transform.position, _tragetPoint) <= 0.3f)
        {
            enabled = false;
            if (_disableAfterReach)
            {
                gameObject.SetActive(false);
                transform.position = _startPosition;
            }
        }
    }

    public void SetTargetPosition(bool disable, Vector3 targetPoint)
    {
        _startPosition = transform.position;
        _disableAfterReach = disable;
        _tragetPoint = targetPoint;
    }
}

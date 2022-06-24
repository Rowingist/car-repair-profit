using UnityEngine;

public class MoneyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _start;
    private Vector3 _tragetPoint;
    private bool _disableAfterReach;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            _tragetPoint, Time.deltaTime * _speed);

        if(Vector3.Distance(transform.position, _tragetPoint) <= 0.3f)
        {
            enabled = false;
            if (_disableAfterReach)
            {
                gameObject.SetActive(false);
                transform.position = _start.position;
                transform.parent = _start;
            }
        }
    }

    public void SetTargetPosition(bool disable, Transform target, Transform bank = null)
    {
        if (bank)
        {
            transform.position = bank.transform.position;
            transform.parent = null;
            _start = bank.transform;
        }
        else
        {
            _start = transform;
        }

        _disableAfterReach = disable;
        _tragetPoint = target.position;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }
}

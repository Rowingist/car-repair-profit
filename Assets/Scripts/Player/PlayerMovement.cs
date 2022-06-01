using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _viewToRotate;

    private void FixedUpdate()
    {
        _rigidbody.velocity = (transform.forward * _joystick.Vertical + _joystick.Horizontal * transform.right) * _moveSpeed;

        Rotate();
    }

    private void Rotate()
    {
        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(_rigidbody.velocity);
            lookRotation.x = 0;
            lookRotation.z = 0;

            _viewToRotate.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}

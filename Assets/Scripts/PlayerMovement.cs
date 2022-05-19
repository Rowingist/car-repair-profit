using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] private JoystickTemp _joystick;
    // [SerializeField] private float _sensitivity;

    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // transform.position += new Vector3(_joystick.Value.x, 0f, _joystick.Value.y) * Time.deltaTime * _sensitivity;

        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            Rotate();
    }

    private void Rotate()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_rigidbody.velocity);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
}


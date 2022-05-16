using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _sensitivity;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 lookRotation = new Vector3(_joystick.Value.x, 0f, _joystick.Value.y);
        transform.position += new Vector3(_joystick.Value.x, 0f, _joystick.Value.y) * Time.deltaTime * _sensitivity;

        Rotate(lookRotation);
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }
}

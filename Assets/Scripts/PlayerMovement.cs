using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _sensitivity;

    private void Update()
    {
        transform.position += new Vector3(_joystick.Value.x, 0f, _joystick.Value.y) * Time.deltaTime * _sensitivity;
    }
}

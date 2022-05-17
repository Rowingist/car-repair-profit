using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    //[SerializeField] private Animator _animator;
    //[SerializeField] private JoystickTemp _joystick;

    private Animator _animator;
    [SerializeField] private FloatingJoystick _joystick;


    private const string Speed = "Speed";


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            if (Math.Abs(_joystick.Horizontal) > Math.Abs(_joystick.Vertical))  
                PlayWalkAnimation(Math.Abs(_joystick.Horizontal));
            else
                PlayWalkAnimation(Math.Abs(_joystick.Vertical));
        }
        else
            PlayIdleAnimation();
    }


    private void PlayWalkAnimation(float value)
    {
        _animator.SetFloat(Speed, value);
    }

    private void PlayIdleAnimation()
    {
        _animator.SetFloat(Speed, 0);
    }
}

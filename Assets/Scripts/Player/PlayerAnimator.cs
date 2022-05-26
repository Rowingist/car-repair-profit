using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    //[SerializeField] private Animator _animator;
    //[SerializeField] private JoystickTemp _joystick;

    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Stock _playerBag;
    [SerializeField] private float _transitionSpeed = 10f;
    [SerializeField] private int _caryingLayerIndex = 1;

    private const string SPEED = "Speed";
    private Animator _animator;
    private float _currentLayerWeight = 0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        TransitToCaryingWeight();

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
        _animator.SetFloat(SPEED, value);
    }

    private void PlayIdleAnimation()
    {
        _animator.SetFloat(SPEED, 0);
    }

    private void TransitToCaryingWeight()
    {
        int caryingStack = _playerBag.NotEmpty ? 1 : 0;
        _currentLayerWeight = Mathf.Lerp(_currentLayerWeight, caryingStack, Time.deltaTime * _transitionSpeed);
        _animator.SetLayerWeight(_caryingLayerIndex, _currentLayerWeight);
    }
}

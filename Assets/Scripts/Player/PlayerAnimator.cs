using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;

    private const string Speed = "Speed";

    private void OnEnable()
    {
        _joystick.Pressed += OnJoyscikDown;
        _joystick.Released += OnJoyscikUp;
    }

    private void OnJoyscikUp()
    {
        PlayIdleAnimation();
    }

    private void OnJoyscikDown()
    {
        PlayWalkAnimation();
    }

    private void OnDisable()
    {
        _joystick.Pressed -= OnJoyscikDown;
        _joystick.Released -= OnJoyscikUp;
    }

    private void PlayWalkAnimation()
    {
        _animator.SetFloat(Speed, 1);
    }

    private void PlayIdleAnimation()
    {
        _animator.SetFloat(Speed, 0);
    }
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickActiveZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private JoystickTemp _joystick;

    public void OnPointerDown(PointerEventData eventData)
    {
        _joystick.Down(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystick.Up(eventData);
    }
}

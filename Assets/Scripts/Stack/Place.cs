using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Place : MonoBehaviour
{
    private Wheel _whell;

    public bool IsAvailible { get; private set; }

    private void Start()
    {
        IsAvailible = true;
    }

    public void Reserve(Wheel whell)
    {
        IsAvailible = false;
        _whell = whell;
    }

    public void Free()
    {
        IsAvailible = true;
    }

    public void ClearStack()
    {
        IsAvailible = true;
    }
}
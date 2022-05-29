using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Place : MonoBehaviour
{
    private Whell _whell;
    private PartOne _part;


    public bool IsAvailible { get; private set; }

    private void Start()
    {
        IsAvailible = true;
    }

    public void Reserve(Whell whell)
    {
        IsAvailible = false;
        _whell = whell;
    }

    public void ReservePart(PartOne part)//
    {
        IsAvailible = false;
        _part = part;
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
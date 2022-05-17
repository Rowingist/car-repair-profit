using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Place : MonoBehaviour
{
    private Whell _whell;

    public bool IsAvailible { get; private set; }

    public event UnityAction<Place> PlaceFree;
    private void Start()
    {
        IsAvailible = true;
    }
    public void Reserve(Whell whell)
    {
        IsAvailible = false;
        _whell = whell;
        _whell.GetComponent<Collectable>().Taken += Free;
    }

    public void Free()
    {
        IsAvailible = true;
        PlaceFree?.Invoke(this);
        _whell.GetComponent<Collectable>().Taken -= Free;
    }
}

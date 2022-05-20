using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public event UnityAction<int> OrderPlaced;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            OrderPlaced?.Invoke(CalculatePartsCount());
        }
    }

    private int CalculatePartsCount()
    {
        return Random.Range(1, 10);
    }
}

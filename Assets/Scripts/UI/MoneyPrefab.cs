using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPrefab : MonoBehaviour
{
    public static Action MainScoreChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            MainScoreChanged?.Invoke();

            Destroy(this.gameObject);
        }
    }

}

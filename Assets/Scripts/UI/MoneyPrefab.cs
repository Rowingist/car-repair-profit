using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPrefab : MonoBehaviour
{
    public static Action PlayersMoneyChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            PlayersMoneyChanged?.Invoke();

            Destroy(this.gameObject);
        }
    }

}

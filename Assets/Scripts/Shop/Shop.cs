using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Shop : MonoBehaviour
{
    [SerializeField] private MoneyOnScreen _moneyOnScreen;

    private int _purchasePrice = 3;

    public event UnityAction<int> OrderPlaced;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (_moneyOnScreen._currentMoney > _purchasePrice)
            {
                OrderPlaced?.Invoke(CalculatePartsCount());

            }
        }
    }

    private int CalculatePartsCount()
    {
        int totalPartCount = _moneyOnScreen._currentMoney / _purchasePrice;

        _moneyOnScreen.MakePurchase(_purchasePrice * totalPartCount);

        return totalPartCount;
    }

    private void Update()
    {
    }
}

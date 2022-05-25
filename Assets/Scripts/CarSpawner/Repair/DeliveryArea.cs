using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


public class DeliveryArea : MonoBehaviour
{
    private CarRepair _car;

    public event UnityAction CarArrivaedToDelivery;
    public event UnityAction PlayerTakeTheCar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CarRepair car))
        {
            CarArrivaedToDelivery?.Invoke();

            _car = car;
        }

        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (_car.IsInDeliveryZone)
            {
                player.transform.SetParent(_car.transform);

                player.gameObject.SetActive(false);

                PlayerTakeTheCar?.Invoke();
            }
        }
    }
}

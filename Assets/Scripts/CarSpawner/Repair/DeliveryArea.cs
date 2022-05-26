using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


public class DeliveryArea : MonoBehaviour
{
    private bool _isCarArrivedToDelivery = false;
    private CarRepair _carRepair;

    public event UnityAction CarArrivedToDelivery;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (_isCarArrivedToDelivery)
            {
                Debug.Log("Player enter in delivery");
                player.transform.SetParent(_carRepair.transform);
                player.gameObject.SetActive(false);
                // PlayerTakeTheCar?.Invoke();
            }
        }

        if (other.gameObject.TryGetComponent(out CarRepair carRepair))
        {
            _carRepair = carRepair;

            CarArrivedToDelivery?.Invoke();
            _isCarArrivedToDelivery = true;
        }
    }


    //private CarRepair _car;

    //public event UnityAction CarArrivaedToDelivery;
    //public event UnityAction PlayerTakeTheCar;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out CarRepair car))
    //    {
    //        CarArrivaedToDelivery?.Invoke();

    //        _car = car;
    //    }

    //    if (other.gameObject.TryGetComponent(out Player player))
    //    {
    //        if (_car.IsInDeliveryZone)
    //        {
    //            player.transform.SetParent(_car.transform);

    //            player.gameObject.SetActive(false);

    //            PlayerTakeTheCar?.Invoke();
    //        }
    //    }
    //}
}

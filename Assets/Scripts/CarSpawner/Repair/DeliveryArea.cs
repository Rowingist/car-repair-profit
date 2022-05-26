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
    public event UnityAction PlayerTakeTheCar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (_isCarArrivedToDelivery)
            {
                player.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0); // нужно другое решение скрытия плеера

                player.transform.SetParent(_carRepair.transform);

                PlayerTakeTheCar?.Invoke();
            }
        }

        if (other.gameObject.TryGetComponent(out CarRepair carRepair))
        {
            _carRepair = carRepair;
            _carRepair.InitDelivery(this);

            CarArrivedToDelivery?.Invoke();
            _isCarArrivedToDelivery = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CarRepair carRepair))
        {
            _isCarArrivedToDelivery = false;
        }
    }
}

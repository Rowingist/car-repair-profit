using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class Car : MonoBehaviour
{
    [SerializeField] private Driver _driverPrefab;
    [SerializeField] private Transform _driverPlace;
    [SerializeField] private Transform _afterRepairPlace;

    private Driver _driver;
    private CarSpawner _carSpawner;
    private DeliveryArea _deliveryArea;
   // private Car _car;

    private void Start()
    {
        _deliveryArea.CarArrivaedToDelivery += DropDriver;
       // _deliveryArea.PlayerTakeTheCar  += MoveToRepair; // логика для плеера, если он садится в машину
    }

    private void OnDisable()
    {
        _deliveryArea.CarArrivaedToDelivery -= DropDriver;
       // _deliveryArea.PlayerTakeTheCar -= MoveToRepair;// логика для плеера, если он садится в машину
    }

    public void InitSpawner(CarSpawner carSpawner, DeliveryArea deliveryArea)
    {
        _carSpawner = carSpawner;
        _deliveryArea = deliveryArea;
    }

    private void DropDriver()
    {
        _driver = Instantiate(_driverPrefab, _driverPlace.position, _driverPlace.rotation, null);
        _driver.transform.SetParent(null);
    }
  
    private void MoveToRepair() // если надо ехать в зону. Добавить метод для возвращения из зоны
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawner._repairPointPoint.position, 3f));
    }

    public void MoveDriverToCar()
    {
        _driver.transform.DOMove(_afterRepairPlace.position, 4);
    }

    public void MoveAfterRepair()
    {
        Destroy(_driver.gameObject, 0.1f);
        Sequence sequence = DOTween.Sequence();
        // sequence.Append(transform.DOMove(_carSpawner._afterRepairPointPoint.position, 2f));
        sequence.Append(transform.DOMove(_carSpawner._spawnPoint.position, 2f));

        Destroy(gameObject, 3);
    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawner._deliveryPoint.position, 2f));
    }
}

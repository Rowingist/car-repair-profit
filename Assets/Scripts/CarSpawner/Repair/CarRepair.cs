using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class CarRepair : MonoBehaviour
{
    [SerializeField] private Driver _driverPrefab;
    [SerializeField] private Transform _driverPlace;
    [SerializeField] private Transform _afterRepairPlace;

    private Driver _driver;
    private CarSpawnRepair _carSpawnerRepair;
    private DeliveryArea _deliveryArea;
    private CarRepair _car;

    void Start()
    {
        _deliveryArea.CarArrivaedToDelivery += DropDriver;
        _deliveryArea.PlayerTakeTheCar += MoveToRepair;
    }

    private void OnDisable()
    {
        _deliveryArea.CarArrivaedToDelivery -= DropDriver;
        _deliveryArea.PlayerTakeTheCar -= MoveToRepair;
    }

    public void InitSpawner(CarSpawnRepair carSpawner, DeliveryArea deliveryArea)
    {
        _carSpawnerRepair = carSpawner;
        _deliveryArea = deliveryArea;
    }

    private void DropDriver()
    {
        _driver = Instantiate(_driverPrefab, _driverPlace.position, _driverPlace.rotation, null);
        // _driver.transform.SetParent(this.transform);
        _driver.transform.SetParent(null);

    }

    private void MoveToRepair()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerRepair._repairPointPoint.position, 3f));
    }

    public void MoveDriverToCar()
    {
        _driver.transform.DOMove(_afterRepairPlace.position, 4);
    }


    public void MoveAfterRepair()
    {
        Destroy(_driver.gameObject, 0.1f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerRepair._afterRepairPoint.position, 2f));
        Destroy(gameObject, 3);

        //_driver.gameObject.SetActive(false);

    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerRepair._deliveryPoint.position, 2f));
    }
}

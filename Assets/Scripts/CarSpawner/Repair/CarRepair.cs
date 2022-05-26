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
    private CarSpawner _carSpawnerRepair;
    private DeliveryArea _deliveryArea;
    private CarRepair _car;
    private BoxCollider _boxCollider;

    public bool IsInDeliveryZone = true; // свойство

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _deliveryArea.CarArrivaedToDelivery += DropDriver;
        _deliveryArea.PlayerTakeTheCar += MoveToRepair;
    }

    private void OnDisable()
    {
        _deliveryArea.CarArrivaedToDelivery -= DropDriver;
        _deliveryArea.PlayerTakeTheCar -= MoveToRepair;
    }

    public void InitSpawner(CarSpawner carSpawner, DeliveryArea deliveryArea)
    {
        _carSpawnerRepair = carSpawner;
        _deliveryArea = deliveryArea;
    }

    private void DropDriver()
    {
        StartCoroutine(SpawnOnTimer());
    }

    private IEnumerator SpawnOnTimer()
    {
        float timeLeft = 1;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            yield return null;
        }

        _driver = Instantiate(_driverPrefab, _driverPlace.position, _driverPlace.rotation);
    }

    private void MoveToRepair()
    {
        IsInDeliveryZone = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerRepair._repairPointPoint.position, 3f));
    }

    public void MoveAfterRepair()
    {
        _boxCollider.gameObject.SetActive(false);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerRepair._afterRepairPoint.position, 2f));
    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerRepair._deliveryPoint.position, 2f));
    }
}

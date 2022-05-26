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

    private CarSpawnerNewBox _carSpawnerNewBox;
    private DeliveryArea _deliveryArea;
    private Upload _upload;
    private Driver _driver;

    private void OnDisable()
    {
        _deliveryArea.CarArrivedToDelivery -= DropDriver;
        _deliveryArea.PlayerTakeTheCar -= MoveToRepair;
    }

    public void InitSpawner(CarSpawnerNewBox carSpawner, Upload upload)
    {
        _carSpawnerNewBox = carSpawner;
        _upload = upload;
    }

    public void InitDelivery(DeliveryArea deliveryArea)
    {
        _deliveryArea = deliveryArea;
        _deliveryArea.CarArrivedToDelivery += DropDriver;
        _deliveryArea.PlayerTakeTheCar += MoveToRepair;
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
        _driver = Instantiate(_driverPrefab, _driverPlace.position, _driverPlace.rotation, null);
        _driver.Init(_upload);
    }


    public void MoveAfterRepair()
    {
        Sequence sequence = DOTween.Sequence();

        _driver.transform.SetParent(this.transform);
        sequence.Append(transform.DOMove(_carSpawnerNewBox._afterRepairPoint.position, 2f));
        Destroy(gameObject, 2);
    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerNewBox._deliveryPoint.position, 2f));
    }

    private void MoveToRepair()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerNewBox._repairPoint.position, 2f));
    }
}

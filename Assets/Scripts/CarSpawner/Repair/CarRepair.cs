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
    private Upload _upload;
    private Driver _driver;

    void Start()
    {
        _upload.CarArrivedToDelivery += DropDriver;
    }

    private void OnDisable()
    {
        _upload.CarArrivedToDelivery -= DropDriver; 
    }

    public void InitSpawner(CarSpawnerNewBox carSpawner, Upload upload)
    {
        _carSpawnerNewBox = carSpawner;
        _upload = upload;
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
        _driver = Instantiate(_driverPrefab, _driverPlace.position, _driverPlace.rotation, this.transform);
        _driver.Init(_upload);
    }


    public void MoveAfterRepair()
    {
        _driver.gameObject.SetActive(false);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerNewBox._spawnPoint.position, 2f));

        Destroy(gameObject, 3);
    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerNewBox._deliveryPoint.position, 2f));
    }

    //private void MoveToRepair()
    //{
    //    IsInDeliveryZone = false;
    //    Sequence sequence = DOTween.Sequence();
    //    //  sequence.Append(transform.DOMove(_carSpawnerNewBox._repairPointPoint.position, 3f));
    //}

}

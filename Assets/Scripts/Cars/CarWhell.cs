using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class CarWhell : MonoBehaviour
{
    [SerializeField] private Driver _driverPrefab;
    [SerializeField] private Transform _driverPlace;

    private CarSpawnerWhell _carSpawnerNew;
    private Upload _upload;
    private Driver _driver;

    private void Start()
    {
        _upload.CarArrivedToDelivery += DropDriver;
    }

    private void OnDisable()
    {
        _upload.CarArrivedToDelivery -= DropDriver;
    }

    public void InitSpawner(CarSpawnerWhell carSpawner, Upload upload)
    {
        _carSpawnerNew = carSpawner;
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

        _driver = Instantiate(_driverPrefab, _driverPlace.position, _driverPlace.rotation, null);
        _driver.Init(_upload);
    }

    public void MoveAfterRepair()
    {
        Destroy(_driver.gameObject);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerNew._spawnPoint.position, 2f));

        Destroy(gameObject, 2);
    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerNew._deliveryPoint.position, 2f));
    }
}

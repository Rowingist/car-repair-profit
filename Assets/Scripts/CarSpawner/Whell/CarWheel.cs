using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class CarWheel : MonoBehaviour
{
    [SerializeField] private Driver _driverPrefab;
    [SerializeField] private Transform _driverPlace;

    private Driver _driver;
    private CarSpawnerWhell _carSpawnerWheel;
    private WheelArea _deliveryArea;

    private void Start()
    {
        _deliveryArea.CarArrivaedToWhell += DropDriver;
    }

    private void OnDisable()
    {
        _deliveryArea.CarArrivaedToWhell -= DropDriver;
    }

    public void InitSpawner(CarSpawnerWhell carSpawner, WheelArea deliveryArea)
    {
        _carSpawnerWheel = carSpawner;
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

    public void MoveAfterRepair()
    {
        _driver.gameObject.SetActive(false);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerWheel._spawnPoint.position, 2f));

        Destroy(gameObject, 3);
    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerWheel._deliveryPoint.position, 2f));
    }
}

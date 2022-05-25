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

    private Driver _driver;
    private CarSpawnerWhell _carSpawnerWhell;
    private WhellArea _deliveryArea;

    private void Start()
    {
        _deliveryArea.CarArrivaedToWhell += DropDriver;
    }

    private void OnDisable()
    {
        _deliveryArea.CarArrivaedToWhell -= DropDriver;
    }

    public void InitSpawner(CarSpawnerWhell carSpawner, WhellArea deliveryArea)
    {
        _carSpawnerWhell = carSpawner;
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
        sequence.Append(transform.DOMove(_carSpawnerWhell._spawnPoint.position, 2f));

        Destroy(gameObject, 3);
    }

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawnerWhell._deliveryPoint.position, 2f));
    }
}

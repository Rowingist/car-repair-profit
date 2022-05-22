using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<Car> _carsPrefabs;
    [SerializeField] private DeliveryArea _deliveryArea;
    [SerializeField] public Transform _spawnPoint;

    private bool _isGarageFree = true;
    private Car _currentCar;
    public Transform _deliveryPoint;  

    private void Start()
    {
        InstantiateCar();
    }

    private void OnEnable()
    {
        _deliveryArea.CarFixed += OnSpawnNew;
    }

    private void OnDisable()
    {
        _deliveryArea.CarFixed -= OnSpawnNew;
    }

    private void OnSpawnNew()
    {
        _isGarageFree = true;

        _currentCar.MoveAfterRepair();
        _currentCar = null;
        InstantiateCar();
    }

    private void InstantiateCar()
    {
        Car newCar = Instantiate(_carsPrefabs[CalculateNumberPrebab()], _spawnPoint.position, _spawnPoint.rotation, null);
        newCar.InitSpawner(this);
        newCar.MoveToGarage();
        _currentCar = newCar;
        _isGarageFree = false;
    }

    private int CalculateNumberPrebab()
    {
        return Random.Range(0, _carsPrefabs.Count);
    }
}

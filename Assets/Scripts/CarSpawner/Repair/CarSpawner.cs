using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<CarRepair> _carsPrefabs;
    [SerializeField] private RepairArea _repairArea;
    [SerializeField] private DeliveryArea _deliveryArea;

    public Transform _spawnPoint; // свойста
    public Transform _deliveryPoint;
    public Transform _repairPointPoint;
    public Transform _afterRepairPoint;

    private bool _isGarageFree = true;
    private CarRepair _currentCar;

    private void Start()
    {
        InstantiateCar();
    }

    private void OnEnable()
    {
        _repairArea.CarFixed += OnSpawnNew;
    }

    private void OnDisable()
    {
         _repairArea.CarFixed -= OnSpawnNew;
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
        CarRepair newCar = Instantiate(_carsPrefabs[CalculateNumberPrebab()], _spawnPoint.position, _spawnPoint.rotation, null);
        newCar.InitSpawner(this, _deliveryArea);
        newCar.MoveToGarage();
        _currentCar = newCar;
        _isGarageFree = false;
    }

    private int CalculateNumberPrebab()
    {
        return Random.Range(0, _carsPrefabs.Count);
    }
}
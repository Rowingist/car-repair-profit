using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CarSpawnerWhell : MonoBehaviour
{
    [SerializeField] private List<CarWheel> _carsPrefabs;
    [SerializeField] private WheelArea _whellArea;

    public Transform _spawnPoint; // �������
    public Transform _deliveryPoint;

    private bool _isGarageFree = true;
    private CarWheel _currentCar;

    private void Start()
    {
        InstantiateCar();
    }

    private void OnEnable()
    {
        _whellArea.CarFixed += OnSpawnNew;
    }

    private void OnDisable()
    {
        _whellArea.CarFixed -= OnSpawnNew;
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
        CarWheel newCar = Instantiate(_carsPrefabs[CalculateNumberPrebab()], _spawnPoint.position, _spawnPoint.rotation, null);
        newCar.InitSpawner(this, _whellArea); 
        newCar.MoveToGarage();
        _currentCar = newCar;
        _isGarageFree = false;
    }

    private int CalculateNumberPrebab()
    {
        return Random.Range(0, _carsPrefabs.Count);
    }
}
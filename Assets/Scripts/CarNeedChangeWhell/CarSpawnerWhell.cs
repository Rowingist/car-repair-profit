using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerWhell : MonoBehaviour
{
    [SerializeField] private List<CarWhell> _carsPrefabs;
    public Transform _spawnPoint;
    public Transform _deliveryPoint;

    private Upload _upload;
    private bool _isGarageFree = true;
    private CarWhell _currentCar;
    
    private void Awake()
    {
        _upload = GetComponent<Upload>();
    }

    private void Start()
    {
        InstantiateCar();
    }

    private void OnEnable()
    {
        _upload.CarFixed += OnSpawnNew;
    }

    private void OnDisable()
    {
        _upload.CarFixed -= OnSpawnNew;
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
        CarWhell newCar = Instantiate(_carsPrefabs[CalculateNumberPrebab()], _spawnPoint.position, _spawnPoint.rotation, null);
        newCar.InitSpawner(this, _upload);
        newCar.MoveToGarage();
        _currentCar = newCar;
        _isGarageFree = false;
    }

    private int CalculateNumberPrebab()
    {
        return Random.Range(0, _carsPrefabs.Count);
    }
}

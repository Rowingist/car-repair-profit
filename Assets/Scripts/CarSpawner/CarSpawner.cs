using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<Car> _carsPrefabs;
    [SerializeField] private DeliveryArea _deliveryArea;
    [SerializeField] public Transform _spawnPoint;   

    private float _elapsedTime = 0;
    private float _currentSpawnDelay = 5; 
    private bool _isGarageFree = true;
    
    public Transform _deliveryPoint; // свойство
    public Transform _fixedCarPoint; // свойство

    private void OnEnable()
    {
        _deliveryArea.CarFixed += OnSpawnNew;
    }
   

    private void OnDisable()
    {
        _deliveryArea.CarFixed -= OnSpawnNew;
    }

    private void Update()
    {
        Debug.Log(_elapsedTime);
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _currentSpawnDelay && _isGarageFree)
        {
            InstantiateCar();

            _elapsedTime = 0;
        }
    }
    private void OnSpawnNew()
    {
        Debug.Log("Машина починена");

        _isGarageFree = true;
    }

    private void InstantiateCar()
    {
        Car car = Instantiate(_carsPrefabs[CalculateNumberPrebab()], _spawnPoint.position, _spawnPoint.rotation);
        car.InitSpawner(this);
        _isGarageFree = false;
    }

    private int CalculateNumberPrebab()
    {
        return Random.Range(0, _carsPrefabs.Count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Car _carTemplate;

    private float _elapsedTime = 0;
    private float _currentSpawnDelay = 3f;
    private bool _isSpawned = false;
    
    public Transform _deliveryPoint; // свойство

    public event UnityAction Spawned;


    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _currentSpawnDelay)
        {
            InstantiateCar();

            _elapsedTime = 0;
        }
    }

    private void InstantiateCar()
    {
        Car car = Instantiate(_carTemplate, _spawnPoint.position, _spawnPoint.rotation);
        car.transform.SetParent(this.transform);
        car.InitSpawner(this);
        Spawned?.Invoke();
    }


}

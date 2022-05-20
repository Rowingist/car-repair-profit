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

    private float _elapsedTime = 0;
    private float _currentSpawnDelay = 5;
    private bool _isGarageFree = true;

    public Car _car; //temp

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

        _car.gameObject.SetActive(false);
        //_car.MoveAfterRepair();
        //this._car.MoveAfterRepair();
    }

    private void InstantiateCar()
    {
        Car newsds = Instantiate(_carsPrefabs[CalculateNumberPrebab()], _spawnPoint.position, _spawnPoint.rotation, null);
        newsds.InitSpawner(this);
        _car = newsds;
        _isGarageFree = false;
    }

    private int CalculateNumberPrebab()
    {
        return Random.Range(0, _carsPrefabs.Count);
    }

    private IEnumerator HideOn(Car car)
    {
        float timeLeft = 10;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        // car.gameObject.SetActive(false);
        //car.transform.position = new Vector3(5, 5, 5);
        Destroy(car.gameObject);
    }
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CarHandle : MonoBehaviour
{
    [SerializeField] private Animator _carAnimator;
    [SerializeField] private string _getIntoGarage = "GetIntoGarage";
    [SerializeField] private string _driveToLift = "DriveToLift";
    [SerializeField] private string _liftUp = "LiftUp";
    [SerializeField] private string _liftDown = "LiftDown";
    [SerializeField] private string _driveToExit = "DriveToExit";
    [SerializeField] private string _leftGarage = "LeftGarage";
    [SerializeField] private CarDoor _carPlatform;
    [SerializeField] private PullingArea _pullingArea;
    [SerializeField] private GameObject[] _vihiclePrefabs;
    [SerializeField] private Transform _carSpawnPoint;
    [SerializeField] private Spawner _moneySpawner;
    [SerializeField] private PlayerToCarTransition _sitInCar;

    private int _activeCarIndex;
    
    private List<GameObject> _cars = new List<GameObject>();

    private void OnEnable()
    {
        _carPlatform.Entered += OnDriveInGarage;
        _pullingArea.Complited += OnLiftDown;
    }

    private void Start()
    {
        InitCars();
        OnGetIntoGarage();
    }

    private void OnDisable()
    {
        _carPlatform.Entered -= OnDriveInGarage;
        _pullingArea.Complited += OnLiftDown;
    }

    private void InitCars()
    {
        for (int i = 0; i < _vihiclePrefabs.Length; i++)
        {
            GameObject newCar = Instantiate(_vihiclePrefabs[i], _carSpawnPoint.position,
                                            _carSpawnPoint.rotation, _carSpawnPoint);
            newCar.gameObject.SetActive(false);
            _cars.Add(newCar);
        }
    }

    private void OnDriveInGarage()
    {
        StartCoroutine(Driwing());
    }

    private IEnumerator Driwing()
    {
        yield return new WaitForSeconds(0.55f);
        if (_pullingArea.Stock.Filled)
        {
            _carAnimator.SetTrigger(_driveToExit);
            _pullingArea.Stock.CleanStock();
            yield break;
        }
        else
        {
            _carAnimator.SetTrigger(_driveToLift);
            yield break;
        }
    }

    private void OnLiftDown()
    {
        _pullingArea.Stock.BlockSock();
        _carAnimator.SetTrigger(_liftDown);
    }

    public void OnLiftUp()
    {
        _carAnimator.SetTrigger(_liftUp);
        _pullingArea.Stock.UnblockSock();
    }

    public void OnLeftGarage()
    {
        _moneySpawner.DelayedSpawn(100);
        _carAnimator.SetTrigger(_leftGarage);
    }

    public void OnGetIntoGarage()
    {
        ChangeCar();
        _pullingArea.Stock.BlockSock();
        _carAnimator.SetTrigger(_getIntoGarage);
    }

    public void ChangeCar()
    {
        if (_cars[_activeCarIndex].gameObject.activeSelf)
            _cars[_activeCarIndex].gameObject.SetActive(false);
        
        int random = Random.Range(0, _cars.Count);
        _activeCarIndex = random;
        _cars[_activeCarIndex].gameObject.SetActive(true);
    }

    public void GetPlayerOut()
    {
        _sitInCar.Exit(_carPlatform.transform);
    }
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;  

public class CarHandle : MonoBehaviour
{
    [SerializeField] private int _amount = 10;

    [SerializeField] private Animator _carAnimator;
    [SerializeField] private string _getIntoGarage = "GetIntoGarage";
    [SerializeField] private string _driveToLift = "DriveToLift";
    [SerializeField] private string _liftUp = "LiftUp";
    [SerializeField] private string _liftDown = "LiftDown";
    [SerializeField] private string _driveToExit = "DriveToExit";
    [SerializeField] private string _leftGarage = "LeftGarage";
    [SerializeField] private string _startWashing = "StartWashing"; //shram script
    [SerializeField] private CarDoor _carPlatform;
    [SerializeField] private PullingArea _pullingArea;
    [SerializeField] private GameObject[] _vihiclePrefabs;
    [SerializeField] private Transform _carSpawnPoint;
    [SerializeField] private PlayerToCarTransition _sitInCar;
    [SerializeField] private Stock _stock; //shram script
    [SerializeField] private MoneySpawner _moneySpawner;

    private int _activeCarIndex;
    private bool _isInBox = false; //shram
    private bool _isCarTutorComlete = false;//shram
    private bool _isShopTutorComlete = false; //shram
    private bool _isWhellChangeTutorComplete = false; //shram
    private int _arrivedCarCount; //shram
    private CarCleaner _carCleaner;

    private List<GameObject> _cars = new List<GameObject>();

    public bool IsInBox => _isInBox; //shram script

    public event UnityAction CarArrived; //shram script
    public event UnityAction CarUpOnLift; //shram script
    public event UnityAction CarInzone; //shram script
    public event UnityAction CarGoOut; //shram script
    public event UnityAction CarWashed;

    private void OnEnable()
    {
        _carPlatform.Entered += OnDriveInGarage;
        _pullingArea.Completed += OnLiftDown;
    }

    private void Start()
    {
        InitCars();
        OnGetIntoGarage();
    }

    private void OnDisable()
    {
        _carPlatform.Entered -= OnDriveInGarage;
        _pullingArea.Completed -= OnLiftDown;
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
        StartCoroutine(Driving());
    }

    private IEnumerator Driving()
    {
        yield return new WaitForSeconds(0.15f);
        if (_pullingArea.Stock.Filled)
        {
            _carAnimator.SetTrigger(_driveToExit);
            _pullingArea.Stock.Clear();
            //   _pullingArea.Stock.IncreaceMaxAllowedCapacity(Random.Range(0, 5));
            _pullingArea.Stock.SetRandomCapacity();
            yield break;
        }
        else
        {
            _carAnimator.SetTrigger(_driveToLift);
            yield break;
        }
    }

    public void DriveToExit()
    {
        _carAnimator.SetTrigger(_driveToExit);
    }

    private void OnLiftDown()
    {
        _pullingArea.Stock.Block();
        _carAnimator.SetTrigger(_liftDown);
    }

    public void OnLiftUp()
    {
        _carAnimator.SetTrigger(_liftUp);
        _pullingArea.Stock.Unblock();
        _isInBox = true; //shram
        CarInzone?.Invoke();

        if (!_isShopTutorComlete)
        {
            CarUpOnLift?.Invoke();//shram
            _isShopTutorComlete = true;
        }
    }

    public void OnLeftGarage()
    {
        int needWhellCarToUnlockArea = 1;

        //_moneySpawner.StartSpawn(_amount);
        _moneySpawner.StartSpawn(Random.Range(_amount, _amount *2));
        _carAnimator.SetTrigger(_leftGarage);
        CarWashed?.Invoke();

        _isInBox = false; //shram

        if (!_isWhellChangeTutorComplete && _arrivedCarCount >= needWhellCarToUnlockArea)
        {
            CarGoOut?.Invoke();
            _isWhellChangeTutorComplete = true;
        }
        _arrivedCarCount++;
    }

   
    public void OnGetIntoGarage()
    {
        ChangeCar();
        _pullingArea.Stock.Block();
        _carAnimator.SetTrigger(_getIntoGarage);
    }

    public void ChangeCar()
    {
        if (_cars[_activeCarIndex].gameObject.activeSelf)
            _cars[_activeCarIndex].gameObject.SetActive(false);

        int random = Random.Range(0, _cars.Count);
        _activeCarIndex = random;
        _cars[_activeCarIndex].gameObject.SetActive(true);

        _carCleaner = _cars[_activeCarIndex].GetComponentInChildren<CarCleaner>();
        _carCleaner.SetDryColor();
    }

    public void GetPlayerOut()
    {
        _sitInCar.Exit(_carPlatform.transform);
    }

    public void PushButtonStartWash() //shram scripts
    {
        _stock.FillAllCells();
        StartCoroutine(WaitWashing());
    }

    public void OnArrived()
    {
        if (!_isCarTutorComlete)
        {
            CarArrived?.Invoke();
            _isCarTutorComlete = true;
        }
    }

    private IEnumerator WaitWashing() //shram scripts
    {
        float timeLeft = 3.5f;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        WashCar();
    }

    private void WashCar()
    {
        _carAnimator.SetTrigger(_startWashing);
        _carCleaner = _cars[_activeCarIndex].GetComponentInChildren<CarCleaner>();
        _carCleaner.ChangeCleanMesh();
    }
}

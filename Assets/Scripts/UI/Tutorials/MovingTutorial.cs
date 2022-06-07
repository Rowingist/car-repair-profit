using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;

public class MovingTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playCamera;
    [SerializeField] private CinemachineVirtualCamera _moneyCamera;
    [SerializeField] private CinemachineVirtualCamera _whellCamera;
    [SerializeField] private CinemachineVirtualCamera _carsDoorCamera;
    [SerializeField] private CinemachineVirtualCamera _shopCamera;
    [SerializeField] private CinemachineVirtualCamera _rackCamera;
    [SerializeField] private CinemachineVirtualCamera _washCamera;

    [SerializeField] private Image _tutorialMesh;
    [SerializeField] private GetFirstMoneyTutorial _moneyArea;
    [SerializeField] private CarHandle _carHandle;
    [SerializeField] private Shop _shop;

    private bool _isMoneyTutorComlete = false;

    private void OnEnable()
    {
        _moneyArea.PlayerExitFromMoneyArea += SetWhellChangeAreaCamera;
        _carHandle.CarArrived += SetCarDoorCamera;
        _carHandle.CarUpOnLift += SetShopCamera;
        _carHandle.CarGoOut += SetWashCamera;
        _shop.PlayerExitFromShop += SetRackCamera;
    }

    private void Start()
    {
        _playCamera.Priority = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !_isMoneyTutorComlete)
        {
            _tutorialMesh.gameObject.SetActive(false);

            SetMoneyAreaCamera();
        }
    }

    private void OnDisable()
    {
        _moneyArea.PlayerExitFromMoneyArea -= SetWhellChangeAreaCamera;
        _carHandle.CarArrived -= SetCarDoorCamera;
        _carHandle.CarUpOnLift -= SetShopCamera;
        _shop.PlayerExitFromShop -= SetRackCamera;
        _carHandle.CarGoOut -= SetWashCamera;
    }

    private void SetMainCamera(CinemachineVirtualCamera currentCamera) // 0 фокус на главную камеру
    {
        currentCamera.Priority = 0;
        _playCamera.Priority = 1;
    }

    private void SetMoneyAreaCamera() // 1 начал первое движение, фокус на деньги
    {
        _moneyCamera.Priority = 1;
        _playCamera.Priority = 0;
        _isMoneyTutorComlete = true;

        StartCoroutine(ShowOnTimer(_moneyCamera));
    }

    private void SetWhellChangeAreaCamera() // 2 вышел из моней, показать цех колес
    {
        _playCamera.Priority = 0;
        _whellCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_playCamera));
    }

    private void SetCarDoorCamera() // 3 приехала перва€ машина
    {
        _playCamera.Priority = 0;
        _carsDoorCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_carsDoorCamera));
    }

    private void SetShopCamera() // 4 машина подъемнике, фокус в магазин
    {
        _playCamera.Priority = 0;
        _shopCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_shopCamera));
    }

    private void SetRackCamera() //5 плеер в магазине, фокус на полки
    {
        _playCamera.Priority = 0;
        _rackCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_rackCamera));
    }

    private void SetWashCamera() // 6 ћашина уехала, фокус на мойку
    {
        _playCamera.Priority = 0;
        _washCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_washCamera));
    }


    private IEnumerator ShowOnTimer(CinemachineVirtualCamera currentCamera)
    {
        float timeLeft = 3f;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        SetMainCamera(currentCamera);
    }
}

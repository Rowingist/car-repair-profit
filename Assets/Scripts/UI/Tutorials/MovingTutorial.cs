using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class MovingTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playCamera;
    [SerializeField] private CinemachineVirtualCamera _moneyCamera;
    [SerializeField] private CinemachineVirtualCamera _washCamera;
    [SerializeField] private CinemachineVirtualCamera _whellCamera;
    [SerializeField] private CinemachineVirtualCamera _carsDoorCamera;
    [SerializeField] private CinemachineVirtualCamera _shopCamera;
    [SerializeField] private CinemachineVirtualCamera _rackCamera;
    [SerializeField] private CinemachineVirtualCamera _repairCamera;
    [SerializeField] private CinemachineVirtualCamera _paintCamera;
    [SerializeField] private CinemachineVirtualCamera _paintBallonCamera;

    [SerializeField] private Image _tutorialMesh;
    [SerializeField] private GetFirstMoneyTutorial _moneyArea;
    [SerializeField] private CarHandle _carHandle;
    [SerializeField] private Shop _shop;
    [SerializeField] private WashingHandle _washingHandle;
    [SerializeField] private EngineRepairCount _engineRepairCount;
    [SerializeField] private PaintingCount _paintingCount;

    private bool _isMoneyTutorComlete = false;
    private bool _isWashingTutorialComplete = false;
    public bool _isWhellTutorialComplete = false;// свойство


    public event UnityAction WashingTutorialShowed;

    private void OnEnable()
    {
        _moneyArea.PlayerExitFromMoneyArea += SetWashCamera;
        _washingHandle.ManyCarsWashed += SetWhellChangeAreaCamera;
        _carHandle.CarArrived += SetCarDoorCamera;
        _carHandle.CarUpOnLift += SetShopCamera;
        _shop.PlayerExitFromShop += SetRackCamera;
        _carHandle.CarGoOut += SetRepairCamera;
        _engineRepairCount.CarExitFromEngine += SetPaintCamera;
        _paintingCount.CarExitFromPainting += OnSetPaintBallonCamera;
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
        _moneyArea.PlayerExitFromMoneyArea -= SetWashCamera;
        _washingHandle.ManyCarsWashed -= SetWhellChangeAreaCamera;
        _carHandle.CarArrived -= SetCarDoorCamera;
        _carHandle.CarUpOnLift -= SetShopCamera;
        _shop.PlayerExitFromShop -= SetRackCamera;
        _carHandle.CarGoOut -= SetRepairCamera;
        _engineRepairCount.CarExitFromEngine -= SetPaintCamera;
        _paintingCount.CarExitFromPainting -= OnSetPaintBallonCamera;

    }

    private void SetMainCamera(CinemachineVirtualCamera currentCamera)
    {
        currentCamera.Priority = 0;
        _playCamera.Priority = 1;
    }

    private void SetMoneyAreaCamera()
    {
        _moneyCamera.Priority = 1;
        _playCamera.Priority = 0;
        _isMoneyTutorComlete = true;

        StartCoroutine(ShowOnTimer(_moneyCamera));
    }

    private void SetWashCamera()
    {
        _playCamera.Priority = 0;
        _washCamera.Priority = 1;
        _isWashingTutorialComplete = true;

        StartCoroutine(ShowOnTimer(_washCamera));

        WashingTutorialShowed?.Invoke();
    }

    private void SetWhellChangeAreaCamera()
    {
        _playCamera.Priority = 0;
        _whellCamera.Priority = 1;
        _isWhellTutorialComplete = true;
        StartCoroutine(ShowOnTimer(_playCamera));
    }

    private void SetCarDoorCamera()
    {
        _playCamera.Priority = 0;
        _carsDoorCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_carsDoorCamera));
    }

    private void SetShopCamera()
    {
            _playCamera.Priority = 0;
            _shopCamera.Priority = 1;
            StartCoroutine(ShowOnTimer(_shopCamera));
    }

    private void SetRackCamera()
    {
        if (_isWashingTutorialComplete)
        {
            _playCamera.Priority = 0;
            _rackCamera.Priority = 1;
            StartCoroutine(ShowOnTimer(_rackCamera));
        }
    }

    private void SetRepairCamera()
    {
        _playCamera.Priority = 0;
        _repairCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_repairCamera));
    }

    private void SetPaintCamera()
    {
        _playCamera.Priority = 0;
        _paintCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_paintCamera));
    }

    private void OnSetPaintBallonCamera()
    {
        _playCamera.Priority = 0;
        _paintBallonCamera.Priority = 1;

        StartCoroutine(ShowOnTimer(_paintBallonCamera));
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

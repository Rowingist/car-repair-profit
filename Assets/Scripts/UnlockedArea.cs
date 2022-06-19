using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UnlockedArea : MonoBehaviour
{
    [SerializeField] private GameObject _washingArea;
    [SerializeField] private GameObject _washLock;
    [SerializeField] private GameObject _gateWhellArea;
    [SerializeField] private GameObject _gateWhellLock;

    [SerializeField] private GameObject _whellArea;
    [SerializeField] private GameObject _whellLock;

    [SerializeField] private GameObject _rackAreaWhell;
    [SerializeField] private GameObject _rackLockWhell;
    [SerializeField] private GameObject _rackAreaEngine;
    [SerializeField] private GameObject _rackLockEngine;
    [SerializeField] private GameObject _rackAreaPaint;
    [SerializeField] private GameObject _rackLockPaint;

    [SerializeField] private GameObject _repairArea;
    [SerializeField] private GameObject _repairLock;
    [SerializeField] private GameObject _gateEngineArea;
    [SerializeField] private GameObject _gateEngineLock;
    // [SerializeField] private GateOpen _gateRepair; 

    [SerializeField] private GameObject _paintArea;
    [SerializeField] private GameObject _paintLock;
    //  [SerializeField] private GateOpen _gatePaintOne;
    //  [SerializeField] private GateOpen _gatePaintTwo; 
    [SerializeField] private GameObject _gatePaintArea;
    [SerializeField] private GameObject _gatePaintLock;

    [SerializeField] private CarHandle _carHandle;

    [SerializeField] private WashingHandle _washingHandle;
    [SerializeField] private MovingTutorial _movingTutorial;
    [SerializeField] private EngineRepairCount _engineRepairCount;


    private void OnEnable()
    {
        _movingTutorial.WashingTutorialShowed += OnOpenWashArea;
        _movingTutorial.RackTutorialShowed += OnOpenRacks;
        _washingHandle.ManyCarsWashed += OnOpenWhellArea;
        _carHandle.CarGoOut += OnOpenRepairArea;
        _engineRepairCount.CarExitFromEngine += OnOpenPaintArea;
    }

    private void OnDisable()
    {
        _movingTutorial.WashingTutorialShowed -= OnOpenWashArea;
        _movingTutorial.RackTutorialShowed -= OnOpenRacks;
        _washingHandle.ManyCarsWashed -= OnOpenWhellArea;
        _carHandle.CarGoOut -= OnOpenRepairArea;
        _engineRepairCount.CarExitFromEngine -= OnOpenPaintArea;
    }

    private void OnOpenWashArea()
    {
        StartCoroutine(WaitUnlockTimer(_washingArea, _washLock));
    }

    private void OnOpenWhellArea()
    {
        //  _gateWashOne.ShutterUp();
        StartCoroutine(WaitUnlockTimer(_gateWhellArea, _gateWhellLock));

        StartCoroutine(WaitUnlockTimer(_whellArea, _whellLock));
    }

    private void OnOpenRacks()
    {
        StartCoroutine(WaitUnlockTimer(_rackAreaWhell, _rackLockWhell));
    }
  
    private void OnOpenRepairArea()
    {
        //  _gateRepair.ShutterUp();
        StartCoroutine(WaitUnlockTimer(_gateEngineArea, _gateEngineLock));
        StartCoroutine(WaitUnlockTimer(_repairArea, _repairLock));
        StartCoroutine(WaitUnlockTimer(_rackAreaEngine, _rackLockEngine));

    }

    private void OnOpenPaintArea()
    {
        // _gatePaintOne.ShutterUp();
        // _gatePaintTwo.ShutterUp();
        StartCoroutine(WaitUnlockTimer(_gatePaintArea, _gatePaintLock));
        StartCoroutine(WaitUnlockTimer(_paintArea, _paintLock));
        StartCoroutine(WaitUnlockTimer(_rackAreaPaint, _rackLockPaint));
    }

    private IEnumerator WaitUnlockTimer(GameObject currentArea, GameObject currentLock)
    {
        float timeLeft = 2.1f;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        currentArea.gameObject.SetActive(true);
        currentLock.gameObject.SetActive(false);

        ChangeScaleEffect(currentArea);
    }

    private void ChangeScaleEffect(GameObject currentArea)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(currentArea.transform.DOScale(1.5f, 0.5f));
        sequence.Insert(0.5f, currentArea.transform.DOScale(1f, 0.5f));
    }
}

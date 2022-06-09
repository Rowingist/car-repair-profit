using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UnlockedArea : MonoBehaviour
{
    [SerializeField] private GameObject _washingArea;
    [SerializeField] private GameObject _washLock;
    [SerializeField] private GameObject _whellgArea;
    [SerializeField] private GameObject _whellLock;
    [SerializeField] private GameObject _repairArea;
    [SerializeField] private GameObject _repairLock;
    [SerializeField] private GameObject _paintArea;
    [SerializeField] private GameObject _paintLock;
    [SerializeField] private CarHandle _carHandle;
    [SerializeField] private WashingHandle _washingHandle;
    [SerializeField] private MovingTutorial _movingTutorial;

    private void OnEnable()
    {
        _movingTutorial.WashingTutorialShowed += OnOpenWashArea;
        _washingHandle.ManyCarsWashed += OnOpenWhellArea;
        _carHandle.CarGoOut += OnOpenRepairArea;
    }

    private void OnDisable()
    {
        _movingTutorial.WashingTutorialShowed -= OnOpenWashArea;
        _washingHandle.ManyCarsWashed -= OnOpenWhellArea;
        _carHandle.CarGoOut -= OnOpenRepairArea;
    }

    private void OnOpenWashArea()
    {
        StartCoroutine(WaitUnlockTimer(_washingArea, _washLock));
    }

    private void OnOpenWhellArea()
    {
        StartCoroutine(WaitUnlockTimer(_whellgArea, _whellLock));
    }
  
    private void OnOpenRepairArea()
    {
        StartCoroutine(WaitUnlockTimer(_repairArea, _repairLock));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UnlockedArea : MonoBehaviour
{
    [SerializeField] private GameObject _washingArea;
    [SerializeField] private GameObject _washLock;
    [SerializeField] private GameObject _repairArea;
    [SerializeField] private GameObject _repairLock;
    [SerializeField] private GameObject _paintArea;
    [SerializeField] private GameObject _paintLock;
    [SerializeField] private CarHandle _carHandle;
    [SerializeField] private PaintingHandle _washingHandle;

    private void OnEnable()
    {
        _carHandle.CarGoOut += OnOpenWashArea;
        _washingHandle.ManyCarsWashed += OnOpenRepairArea;
    }

    private void OnDisable()
    {
        _carHandle.CarGoOut -= OnOpenWashArea;
        _washingHandle.ManyCarsWashed += OnOpenRepairArea;
    }

    private void OnOpenWashArea()
    {
        StartCoroutine(WaitUnlockTimer(_washingArea, _washLock));
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

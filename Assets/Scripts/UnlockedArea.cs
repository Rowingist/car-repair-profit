using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedArea : MonoBehaviour
{
    [SerializeField] private GameObject _currentArea;
    [SerializeField] private GameObject _lockIcon;
    [SerializeField] private CarHandle _carHandle;

    private void OnEnable()
    {
        _carHandle.CarGoOut += OnShow;
    }

    private void OnDisable()
    {
        _carHandle.CarGoOut -= OnShow;
    }

    private void OnShow()
    {
        StartCoroutine(WaitUnlockTimer());
    }

    private IEnumerator WaitUnlockTimer() 
    {
        float timeLeft = 2.1f;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        _currentArea.gameObject.SetActive(true);
        _lockIcon.gameObject.SetActive(false);
    }
}

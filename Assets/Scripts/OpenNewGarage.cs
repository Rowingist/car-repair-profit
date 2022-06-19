using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class OpenNewGarage : MonoBehaviour
{
    [SerializeField] private TMP_Text _wash;
    [SerializeField] private TMP_Text _whell;
    [SerializeField] private TMP_Text _engine;
    [SerializeField] private TMP_Text _paint;

    [SerializeField] private CarHandle _washCarHandle;
    [SerializeField] private CarHandle _whellCarHandle;
    [SerializeField] private CarHandle _engineCarHandle;
    [SerializeField] private CarHandle _engineCarHandle1;

    [SerializeField] private CarHandle _paintCarHandle;
    [SerializeField] private GameObject _congratulationText;

    private int _washCarCount = 25;
    private int _whellCarCount = 15;
    private int _engineCarCount = 20;
    private int _paintCarCount = 7;

    public event UnityAction LevelComplete;

    private void Start()
    {
        UpdateText(_washCarCount, _whellCarCount, _engineCarCount, _paintCarCount);
    }

    private void OnEnable()
    {
        _washCarHandle.CarWashed += CalculateReadyCarsWash;
        _whellCarHandle.CarWashed += CalculateReadyCarsWhell;
        _engineCarHandle.CarWashed += CalculateReadyCarsEngine;
        _engineCarHandle1.CarWashed += CalculateReadyCarsEngine;
        _paintCarHandle.CarWashed += CalculateReadyCarsPaint;
    }

    private void OnDisable()
    {
        _washCarHandle.CarWashed -= CalculateReadyCarsWash;
        _whellCarHandle.CarWashed -= CalculateReadyCarsWhell;
        _engineCarHandle.CarWashed -= CalculateReadyCarsEngine;
        _engineCarHandle1.CarWashed -= CalculateReadyCarsEngine;
        _paintCarHandle.CarWashed -= CalculateReadyCarsPaint;
    }

    private void CalculateReadyCarsWash()
    {
        _washCarCount--;

        if (_washCarCount < 0)
            _washCarCount = 0;

        UpdateText(_washCarCount, _whellCarCount, _engineCarCount, _paintCarCount);
    }

    private void CalculateReadyCarsWhell()
    {
        _whellCarCount--;

        if (_whellCarCount < 0)
            _whellCarCount = 0;

        UpdateText(_washCarCount, _whellCarCount, _engineCarCount, _paintCarCount);
    }

    private void CalculateReadyCarsEngine()
    {
        _engineCarCount--;

        if (_engineCarCount < 0)
            _engineCarCount = 0;

        UpdateText(_washCarCount, _whellCarCount, _engineCarCount, _paintCarCount);
    }

    private void CalculateReadyCarsPaint()
    {
        _paintCarCount--;

        if (_paintCarCount < 0)
            _paintCarCount = 0;

        UpdateText(_washCarCount, _whellCarCount, _engineCarCount, _paintCarCount);
    }

    private void UpdateText(int wash, int whell, int engine, int paint)
    {
        _wash.text = wash.ToString();
        _whell.text = whell.ToString();
        _engine.text = engine.ToString();
        _paint.text = paint.ToString();

        CheckLevleComplete();
    }

    private void CheckLevleComplete()
    {
        if (_whellCarCount == 0 && _whellCarCount == 0 && _engineCarCount == 0 && _paintCarCount == 0)
        {
            LevelComplete?.Invoke();
            _congratulationText.gameObject.SetActive(true);
            StartCoroutine(ShowOnTimer());
        }
    }

    private IEnumerator ShowOnTimer()
    {
        float timeLeft = 6f;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        _congratulationText.gameObject.SetActive(false);
    }
}

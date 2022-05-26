using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoneyOnScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _textInWidget;

    private int _currentPoint;

    private void OnEnable()
    {
        Upload.MainScoreChanged += OnScoreChanged;
        MoneyPrefab.MainScoreChanged += OnScoreChanged;
    }

    void Start()
    {
        _currentPoint = 0;
        _textInWidget.text = _currentPoint.ToString();
    }

    private void OnDisable()
    {
        Upload.MainScoreChanged -= OnScoreChanged;
        MoneyPrefab.MainScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _currentPoint++;
        _textInWidget.text = _currentPoint.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoneyOnScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _textInWidget;

    public int _currentMoney; // свойство сделать

    private void OnEnable()
    {
        MoneyPrefab.PlayersMoneyChanged += OnMoneyChanged;
    }

    void Start()
    {
        _currentMoney = 0;
        _textInWidget.text = _currentMoney.ToString();
    }

    private void OnDisable()
    {
        MoneyPrefab.PlayersMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged()
    {
        _currentMoney++;
        _textInWidget.text = _currentMoney.ToString();
    }

    public void MakePurchase(int moneySpent)
    {
        _currentMoney -= moneySpent;
        _textInWidget.text = _currentMoney.ToString();
    }
}

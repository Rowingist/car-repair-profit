using System;
using TMPro;
using UnityEngine;

public class OrderIcon : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _stock.AddedItem += OnUpdateText;
    }

    private void Update()
    {
        OnUpdateText();
    }

    private void OnDisable()
    {
        _stock.AddedItem -= OnUpdateText;
    }

    private void OnUpdateText()
    {
        _text.text = "x" + _stock.GetDemandedCount().ToString();
    }
}

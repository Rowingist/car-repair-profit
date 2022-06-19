using TMPro;
using UnityEngine;
using System;

public class DropMoneyToOpenGate : MonoBehaviour
{
    [SerializeField] private GameObject _rootZone;
    [SerializeField] private GateOpen _gate;
    [SerializeField] private int _zonePrice;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private int _oppenedCellsAmount;

    private int _startPrice;

    public event Action Sold;

    private void Start()
    {
        _startPrice = _zonePrice;
    }

    private void Update()
    {
        UpdateText(_zonePrice);
    }

    public void Push(int value)
    {
        _zonePrice -= value;
        if (_zonePrice <= 0)
        {
            _gate.ShutterUp();

            Sold?.Invoke();
            _rootZone.SetActive(false);
        }
        UpdateText(_zonePrice);
    }

    public int GetZonePrice()
    {
        return _startPrice;
    }

    private void UpdateText(int value)
    {
        _price.text = value.ToString();
    }
}

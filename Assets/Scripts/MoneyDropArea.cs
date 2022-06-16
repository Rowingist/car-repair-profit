using System.Collections;
using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyDropArea : MonoBehaviour
{
    [SerializeField] private GameObject _bodyTurnOff;
    [SerializeField] private GameObject _zoneToOpen;
    [SerializeField] private int _zonePrice;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private bool _isSellingZones;
    [SerializeField] private ParticleSystem _spawnAreaParticle;
    [SerializeField] private Stock _relatedStack;
    [SerializeField] private int _oppenedCellsAmount;

    [SerializeField] private ParticleSystem _openZoneParticle;
    [SerializeField] private GameObject _congratulationMessage;

    private int _startPrice;

    public event Action Sold;

    private void Start()
    {
        if (_isSellingZones)
            UpdateText(_zonePrice);

        _startPrice = _zonePrice;
    }

    public void Push(int value)
    {
        if (_isSellingZones)
        {
            _zonePrice -= value;
            if (_zonePrice <= 0)
            {
                _congratulationMessage.gameObject.SetActive(true);
                Destroy(_congratulationMessage.gameObject, 4f);

                _zoneToOpen.SetActive(true);
                _openZoneParticle.Play();

                _relatedStack.IncreaceMaxAllowedCapacity(_oppenedCellsAmount);
                ChangeScaleEffect(_zoneToOpen);
                Sold?.Invoke();
                _bodyTurnOff.SetActive(false);

            }
            UpdateText(_zonePrice);
        }
    }

    public int GetZonePrice()
    {
        return _startPrice;
    }

    private void UpdateText(int value)
    {
        _price.text = value.ToString();
    }
    private void ChangeScaleEffect(GameObject currentArea)
    {
        _spawnAreaParticle.Play();
        currentArea.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
    }
}

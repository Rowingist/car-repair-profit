using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

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

    private int _currentPrice;

    public event Action Sold;

    public Player Player {get; private set;}

    private void Start()
    {
        if (_isSellingZones)
            UpdateText(_zonePrice);

        _currentPrice = _zonePrice;
    }

    public void UpdateState(int value)
    {
        _currentPrice -= value;
        UpdateText(_currentPrice);

        if (_currentPrice == 0)
        {
            _zoneToOpen.gameObject.SetActive(true);
            _spawnAreaParticle.Play();
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
            Player = player;
    }

    public void Deactivate()
    {
        _bodyTurnOff.gameObject.SetActive(false);
    }

    public int GetZonePrice()
    {
        return _currentPrice;
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

using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyDropArea : MonoBehaviour
{
    [SerializeField] private GameObject _dropArea;
    [SerializeField] private GameObject _zoneToOpen;
    [SerializeField] private int _zonePrice;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private bool _isSellingZones;
    [SerializeField] private ParticleSystem _spawnAreaParticle;

    public event Action Sold;

    private void Start()
    {
        if (_isSellingZones)
            UpdateText(_zonePrice);
    }

    public void Push(int value)
    {
        if (_isSellingZones)
        {
            _zonePrice -= value;
            if (_zonePrice <= 0)
            {
                _zoneToOpen.SetActive(true);
                ChangeScaleEffect(_zoneToOpen);
                _dropArea.gameObject.SetActive(false);
                Sold?.Invoke();
            }
            UpdateText(_zonePrice);
        }
    }

    private void UpdateText(int value)
    {
        _price.text = value.ToString();
    }
    private void ChangeScaleEffect(GameObject currentArea)
    {
       currentArea.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
    }
}

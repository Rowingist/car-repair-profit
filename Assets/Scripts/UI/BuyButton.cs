using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _sourceIcon;
    [SerializeField] private int _relatedPrice;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Stock _relatedStock;
    [SerializeField] private GameObject _relatedArea;
    [SerializeField] private Image _blockImage;

    public event Action<int, ItemType> Clicked;

    public ItemType ItemType => _itemType;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnImmitClickedAction);
    }

    private void Start()
    {
        SettingButton(_relatedPrice);
    }

    private void Update()
    {
        _blockImage.gameObject.SetActive(!_relatedArea.gameObject.activeSelf);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnImmitClickedAction);
    }

    private void OnImmitClickedAction()
    {
        if (_relatedStock.Filled)
            return;

        Clicked?.Invoke(_relatedPrice, _itemType);
    }

    public void SettingButton(Image icon)
    {
        _sourceIcon.sprite = icon.sprite;
    }

    public void SettingButton(int price)
    {
        _relatedPrice = price;
        _priceText.text = price.ToString();
    }

    public void SettingButtonName(string name)
    {
        _name.text = name;
    }
}

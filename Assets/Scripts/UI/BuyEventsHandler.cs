using UnityEngine;

public class BuyEventsHandler : MonoBehaviour
{
    [SerializeField] private BuyButton[] _buyButtons;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        for (int i = 0; i < _buyButtons.Length; i++)
        {
            _buyButtons[i].Clicked += OnPlayerPay;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _buyButtons.Length; i++)
        {
            _buyButtons[i].Clicked -= OnPlayerPay;
        }
    }

    private void OnPlayerPay(int amount, ItemType itemType)
    {
        _player.SetByingItemType(itemType);
        _player.Pay(amount);
    }
}

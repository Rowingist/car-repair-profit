using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _walletPoint;
    [SerializeField] private Data _data;

    private IWallet _wallet = new Wallet(0);
    private MoneyDropArea _moneyDropArea;
    private DropMoneyToOpenGate _dropMoneyToOpenGate;

    public event Action<ItemType> Payed;
    public event Action GotCash;
    public event Action WithdrowCash;

    public ItemType BuyingItemType { get; private set; }
    public PlayerStayingOn PlayerStayingOn { get; private set; }
    public Transform WalletPoint => _walletPoint;

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {

    }

    
    private void OpenGates(Transform point)
    {
        int price = _dropMoneyToOpenGate.GetZonePrice();
        bool isAbleToPay = _wallet.TryWithdraw(price / 25);
        if (isAbleToPay)
        {
            _dropMoneyToOpenGate.Push(price / 25);
            WithdrowCash?.Invoke();
            _data.SetCurrentSoft(_wallet.GetCashAmount());
        }
    }

    public void Replenish(int value)
    {
        _wallet.Replenish(value);
        GotCash?.Invoke();
        _data.SetCurrentSoft(_wallet.GetCashAmount());
        _data.Save();
    }

    public void PayInShop(int cash)
    {
        if (_wallet.TryWithdraw(cash))
        {
            Payed?.Invoke(BuyingItemType);
            _data.SetCurrentSoft(_wallet.GetCashAmount());
            _data.Save();
        }
    }

    public void PayForZone(int cash)
    {
        if (_wallet.TryWithdraw(cash))
        {
            WithdrowCash?.Invoke();
            _data.SetCurrentSoft(_wallet.GetCashAmount());
            _data.Save();
        }
    }

    public int GetWalletCash()
    {
        return _wallet.GetCashAmount();
    }

    public void SetByingItemType(ItemType itemType)
    {
        BuyingItemType = itemType;
    }

    public void EnterShop()
    {
        PlayerStayingOn = PlayerStayingOn.ShopArea;
    }

    public void ExitShop()
    {
        PlayerStayingOn = PlayerStayingOn.Floor;
    }
}

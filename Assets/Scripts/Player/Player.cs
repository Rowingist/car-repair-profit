using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _walletPoint;
    [SerializeField] private GameObject _animationActivator;
    [SerializeField] private MoneyDropAnimation _dropAnimation;
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
        if (other.TryGetComponent(out MoneyDropArea moneyDropArea))
        {
            _moneyDropArea = moneyDropArea;
            _moneyDropArea.Sold += OnDisableAnimation;
        }

        if (other.TryGetComponent(out DropMoneyToOpenGate dropMoneyToOpenGate))//
        {
            _dropMoneyToOpenGate = dropMoneyToOpenGate;
            _dropMoneyToOpenGate.Sold += OnDisableAnimation;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out MoneyDropArea moneyDropArea))
        {
            OpeningNewZone(moneyDropArea.transform);
        }

        if (other.TryGetComponent(out DropMoneyToOpenGate dropMoneyToOpenGate))//
        {
            OpenGates(dropMoneyToOpenGate.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnDisableAnimation();
    }

    private void OnDisable()
    {
        if (_moneyDropArea)
            _moneyDropArea.Sold -= OnDisableAnimation;
    }

    private void OpeningNewZone(Transform dropPoint)
    {
        int price = _moneyDropArea.GetZonePrice();
        bool isAbleToPay = _wallet.TryWithdraw(price / 25);
        if (isAbleToPay)
        {
            _animationActivator.SetActive(true);
            SetMoneyDropPoint(dropPoint);
            _moneyDropArea.Push(price / 25);
            WithdrowCash?.Invoke();
            _data.SetCurrentSoft(_wallet.GetCashAmount());
        }
        else
        {
            _animationActivator.SetActive(false);
        }
    }
    
    private void OpenGates(Transform point)
    {
        int price = _dropMoneyToOpenGate.GetZonePrice();
        bool isAbleToPay = _wallet.TryWithdraw(price / 25);
        if (isAbleToPay)
        {
            _animationActivator.SetActive(true);
            SetMoneyDropPoint(point);
            _dropMoneyToOpenGate.Push(price / 25);
            WithdrowCash?.Invoke();
            _data.SetCurrentSoft(_wallet.GetCashAmount());
        }
        else
        {
            _animationActivator.SetActive(false);
        }
    }

    private void OnDisableAnimation()
    {
        _animationActivator.SetActive(false);
    }

    private void SetMoneyDropPoint(Transform point)
    {
        _dropAnimation.SetPositionPoint(point);
    }

    public void Replenish(int value)
    {
        _wallet.Replenish(value);
        GotCash?.Invoke();
        _data.SetCurrentSoft(_wallet.GetCashAmount());
    }

    public void Pay(int cash)
    {
        if (_wallet.TryWithdraw(cash))
        {
            Payed?.Invoke(BuyingItemType);
            WithdrowCash?.Invoke();
            _data.SetCurrentSoft(_wallet.GetCashAmount());
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

public enum PlayerStayingOn
{
    ShopArea,
    PullArea,
    PushArea,
    Floor
}

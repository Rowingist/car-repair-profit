using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Area
{
    [SerializeField] private Transform _walletPoint;

    private float _spentTimeAfterPut;
    private IWallet _wallet = new Wallet(0);
    private Stock _onStayingStock;

    public event Action Payed;
    public event Action GotCash;

    public ItemType ByingItemType { get; private set;
    }
    private void Update()
    {
        _spentTimeAfterPut += Time.deltaTime;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PullingArea onStaingArea))
        {
            _onStayingStock = onStaingArea.Stock;
        }
    }

    public void Push(Item item)
    {
        if (item)
        {
            if(item.ItemType == ItemType.Money)
            {
                GameCash cash = (GameCash)item;
                _wallet.Replenish(cash.Value);
                GotCash?.Invoke();
                Stock.MoveToDestination(cash, _walletPoint);
                cash.Collect();
                return;
            }

            if (_spentTimeAfterPut >= ActionInterval)
            {
                _spentTimeAfterPut = 0;
                Stock.Push(item);
            }
        }
    }


    public Item Pull()
    {
        if(_onStayingStock)
            if (_onStayingStock.Blocked)
                return null;
        

        if(_onStayingStock.StockType == StockType.Single)
        {
            print(_onStayingStock.StockType);

            if (_onStayingStock) 
            {
                if (Stock.GetTopItem().ItemType == _onStayingStock.ItemsType)
                    return Stock.Pull();
            }
        }
        else if(_onStayingStock.StockType == StockType.Multiple)
        {
            return Stock.Pull();
        }

        return null;
    }

    public void Pay(int cash)
    {
        bool canPay = _wallet.TryWithdraw(cash);
        if (canPay)
            Payed?.Invoke();
    }

    public int GetWalletCash()
    {
        return _wallet.GetCashAmount();
    }

    public void SetByingItemType(ItemType itemType)
    {
        ByingItemType = itemType;
    }
}

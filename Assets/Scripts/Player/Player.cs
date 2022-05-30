using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Area
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private Transform _walletPoint;

    private float _spentTimeAfterPut;
    private IWallet _wallet = new Wallet(0);
    private Stock _onStayingStock;

    public event Action Payed;
    public event Action GotCash;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnPay);
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

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnPay);
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
        _wallet.Withdraw(cash);
    }

    public int GetWalletCash()
    {
        return _wallet.GetCashAmount();
    }

    private void OnPay()
    {
        _wallet.Withdraw(10);
        Payed?.Invoke();
    }
}

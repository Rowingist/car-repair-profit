using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _walletPoint;
    [SerializeField] private PushingArea _pushArea;
    [SerializeField] private PullingArea _pullArea;

    private IWallet _wallet = new Wallet(0);

    public event Action Payed;
    public event Action GotCash;

    public ItemType ByingItemType { get; private set; }
    public PlayerStayingOn PlayerStayingOn { get; private set; }

    private void Start()
    {
        _wallet.Replenish(1000);
        _pushArea.enabled = false;
        _pullArea.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PushingArea>()) { _pullArea.enabled = true; }
        else if (other.GetComponent<PullingArea>()) { _pushArea.enabled = true; }     
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Area>())
        {
            _pushArea.enabled = false;
            _pullArea.enabled = false;
        }
    }

    //public void Push(Item item)
    //{
    //    if (item)
    //    {
    //        if (item.ItemType == ItemType.Money)
    //        {
    //            GameCash cash = (GameCash)item;
    //            _wallet.Replenish(cash.Value);
    //            GotCash?.Invoke();
    //            cash.Collect();
    //            return;
    //        }

    //        _stock.PushToLastFreeCell(item);
    //    }
    //}

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

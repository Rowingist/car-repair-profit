using UnityEngine;

public class Wallet : IWallet
{
    private int _cash;

    public Wallet(int pointsAmount)
    {
        _cash = pointsAmount;
    }

    public void Replenish(int cash)
    {
        if (cash >= int.MaxValue)
        {
            Debug.LogError("Maximum value of money reached.");
            return;
        }

        _cash += cash;
    }

    public bool TryWithdraw(int cash)
    {
        if (cash > _cash)
        {
            Debug.LogError("Not enough money");
            return false;
        }

        _cash -= cash;
        return true;
    }

    public int GetCashAmount()
    {
        return _cash;
    }
}

public interface IWallet
{
    public void Replenish(int cash);
    public bool TryWithdraw(int cash);
    public int GetCashAmount();
}

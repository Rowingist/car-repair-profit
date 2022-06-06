using System.Collections;
using UnityEngine;

public class MoneyArea : Area
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if (Stock.Empty)
                return;

            Stock.PullFast(player.transform);
            StartCoroutine(ReplneishingWallet(Stock.GetCount(), player));
            StartCoroutine(Deactiwating());
        }
    }

    private IEnumerator ReplneishingWallet(int moneyAmount, Player player)
    {
        int target = player.GetWalletCash() + moneyAmount;
        while (player.GetWalletCash() <= target)
        {
            player.Replenish(1);
            yield return null;
        }
    }

    private IEnumerator Deactiwating()
    {
        yield return new WaitForSeconds(0.05f);
        Stock.HideInPool();
    }
}

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
            StartCoroutine(ReplneishingWallet(Stock.GetCount() + 1, player));
            StartCoroutine(Deactiwating());
        }
    }

    private IEnumerator ReplneishingWallet(int moneyAmount, Player player)
    {
        int target = player.GetWalletCash() + moneyAmount;
        while (player.GetWalletCash() <= target)
        {
            player.Replenish(target / 35);
            yield return null;
        }
        player.Replenish(-player.GetWalletCash() + target);
    }

    private IEnumerator Deactiwating()
    {
        yield return new WaitForSeconds(1f);
        Stock.HideInPool();
    }
}

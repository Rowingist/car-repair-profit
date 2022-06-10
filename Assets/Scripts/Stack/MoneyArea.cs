using System.Collections;
using UnityEngine;

public class MoneyArea : Area
{
    [SerializeField] private ParticleSystem _smokePoofEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if (Stock.Empty)
                return;

            Stock.PullFast(player.WalletPoint);
            StartCoroutine(ReplneishingWallet(Stock.GetCount() + 1, player));
            StartCoroutine(Deactiwating());
        }
    }

    private IEnumerator ReplneishingWallet(int moneyAmount, Player player)
    {
        int target = player.GetWalletCash() + moneyAmount;
        while (player.GetWalletCash() <= target)
        {
            player.Replenish(target / 7);
            yield return null;
        }
        player.Replenish(-player.GetWalletCash() + target);
    }

    private IEnumerator Deactiwating()
    {
        yield return new WaitForSeconds(0.4f);
        Stock.HideInPool();
        yield return new WaitForSeconds(0.1f);
        _smokePoofEffect.Play();
    }
}

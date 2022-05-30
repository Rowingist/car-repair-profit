using UnityEngine;

public class PushingArea : Area
{
    private float _spentTimeAfterPush;

    private void Update()
    {
        _spentTimeAfterPush += Time.deltaTime;
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (_spentTimeAfterPush >= ActionInterval)
        {
            _spentTimeAfterPush = 0;
            if (Player)
            {
                if (Player.Stock.Filled && Stock.StockType != StockType.ForMoney)
                    return;

                Item transmitting = Stock.Pull();
                if (transmitting)
                {
                    Player.Push(transmitting);
                }
            }

        }
    }
}

using UnityEngine;

public class PullingArea : Area
{
    private float _spentTimeAfterPut;

    private void Update()
    {
        _spentTimeAfterPut += Time.deltaTime;
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (Stock.Filled)
            return;

        if (Player)
        {
            if (Player.Stock.Empty)
                return;

            if (_spentTimeAfterPut >= ActionInterval)
            {
                _spentTimeAfterPut = 0;
                Item transmitting = Player.Pull();
                if (transmitting)
                {
                    Stock.Push(transmitting);
                }

            }
        }
    }
}

using UnityEngine;

public class PushingArea : Area
{
    private float _spentTimeAfterPush;

    private void Update()
    {
        _spentTimeAfterPush += Time.deltaTime;
    }

    public void OnTriggerStay(Collider other)
    {
        if (ConnectedArea)
        {
            if (ConnectedArea.Stock.Blocked)
                return;
            if (ConnectedArea.Stock.Filled)
                return;
        }

        if (_spentTimeAfterPush >= TransitionInterval)
        {
            _spentTimeAfterPush = 0;

            Item transmitting = Stock.Pull(Stock.ItemsType);
            if (transmitting)
            {
                ConnectedArea.Push(transmitting);
            }
        }
    }
}

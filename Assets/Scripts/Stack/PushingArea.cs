using UnityEngine;

public class PushingArea : Area
{
    private float _spentTimeAfterLastPush;

    private void Update()
    {
        _spentTimeAfterLastPush += Time.deltaTime;
    }

    public void OnTriggerStay(Collider other)
    {
        if (ConnectedArea)
        {
            if (ConnectedArea.Stock.Blocked)
                return;
            if (ConnectedArea.Stock.Filled)
                return;

            if (_spentTimeAfterLastPush >= TransitionInterval)
            {
                _spentTimeAfterLastPush = 0;

                Item transmitting = Stock.Remove(Stock.ItemsType);
                if (transmitting)
                {
                    ConnectedArea.Add(transmitting);
                }
            }
        }
    }
}

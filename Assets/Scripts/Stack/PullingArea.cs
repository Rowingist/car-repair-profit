using System;
using UnityEngine;

public class PullingArea : Area
{
    private float _spentTimeAfterPut;

    public event Action Completed;

    private void Update()
    {
        _spentTimeAfterPut += Time.deltaTime;
    }

    public void OnTriggerStay(Collider other)
    {
        if (ConnectedArea)
        {
            if (Stock.Blocked)
                return;
            if (Stock.Filled)
            {
                Completed?.Invoke();
                return;
            }
        }

        if (_spentTimeAfterPut >= TransitionInterval)
        {
            _spentTimeAfterPut = 0;
            Item transmitting = ConnectedArea.Pull();
            if (transmitting)
            {
                Stock.PushToLastFreeCell(transmitting);
            }
        }
    }
}

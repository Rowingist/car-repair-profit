using System;
using UnityEngine;

public class PullingArea : Area
{
    private float _spentTimeAfterLastPull;

    public event Action Completed;

    private void Update()
    {
        _spentTimeAfterLastPull += Time.deltaTime;
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
            if (_spentTimeAfterLastPull >= TransitionInterval)
            {
                _spentTimeAfterLastPull = 0;
                Item transmitting = ConnectedArea.Remove();
                if (transmitting)
                {
                    Add(transmitting);
                }
            }
        }
    }
}

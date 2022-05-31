using System;
using System.Collections;
using UnityEngine;

public class PullingArea : Area
{
    [SerializeField] private bool _disablingItems;

    private float _spentTimeAfterPut;

    public event Action Complited;

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
                if(_disablingItems)
                {
                    StartCoroutine(DisablingItem(transmitting));  
                }
            }
        }

        if (Stock.Filled)
            Complited?.Invoke();
    }

    private IEnumerator DisablingItem(Item transmitting)
    {
        yield return new WaitForSeconds(ActionInterval + 0.2f);
        transmitting.gameObject.SetActive(false);
    }
}

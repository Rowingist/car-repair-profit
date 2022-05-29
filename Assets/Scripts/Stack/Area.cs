using System;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private Stock _stock;

    protected float ActionInterval = 0.2f;
    protected Player Player;

    public Stock Stock => _stock;

    public event Action Entered;
    public event Action Left;

    private void Start()
    {
        if (_stock.StockType == StockType.ForMoney)
            ActionInterval = 0.01f;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Entered?.Invoke();
            Player = player;
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            Player = player;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Left?.Invoke();
        }
    }
}

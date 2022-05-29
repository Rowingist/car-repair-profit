using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private Stock _stock;

    protected float ActionInterval = 0.2f;
    protected Player Player;
    
    public Stock Stock => _stock;

    private void Start()
    {
        if (_stock.StockType == StockType.ForMoney)
            ActionInterval = 0.01f;
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            Player = player;
        }
    }
}

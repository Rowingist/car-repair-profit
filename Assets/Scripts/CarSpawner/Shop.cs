using UnityEngine;

public class Shop : Area
{
    [SerializeField] private ShopView _shopView;



    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (Player)
            _shopView.gameObject.SetActive(true);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (Player)
            _shopView.gameObject.SetActive(false);
    }


}

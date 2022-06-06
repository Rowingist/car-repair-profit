using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Stock _stock;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Payed += TakeItem;
    }

    private void Start()
    {
        Initialize(_itemPrefab);
    }

    private void OnDisable()
    {
        _player.Payed -= TakeItem;
    }

    private void TakeItem(ItemType demandedItemType)
    {
        if (TryGetObject(out GameObject itemObject))
        {
            if (itemObject.TryGetComponent(out Item item))
            {
                if (item.ItemType == demandedItemType)
                {
                    if (_stock.Filled)
                        return;

                    SetItem(itemObject);
                    _stock.PushToLastFreeCell(item);
                }
            }

        }
    }

    private void SetItem(GameObject item)
    {
        item.transform.position = transform.position;
        item.SetActive(true);
    }
}

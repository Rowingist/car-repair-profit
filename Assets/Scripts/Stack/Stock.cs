using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CellsSequence))]
public class Stock : MonoBehaviour
{
    [SerializeField] private GameObject _itemsPool;
    [SerializeField] private StockType _stockType;
    [SerializeField] private ItemType _itemsType;
    [SerializeField] private int _maxAllovedCapacity;
    [SerializeField] private int _currentMaxCapacity;
    [SerializeField] private int _currentMinCapacity;
    [SerializeField] private bool _needResetAfterRemove;
    [SerializeField] private float _addItemDuration = 0.5f;

    private CellsSequence _cells;

    private List<Item> _items = new List<Item>();

    public event Action AddedItem;
    public event Action RemovedItem;

    public bool Empty => _items.Count == 0;
    public bool Filled => _items.Count == _cells.GetCount();
    public bool Blocked { get; private set; }
    public ItemType ItemsType => _itemsType;
    public int Lifespan { get; private set; }

    private void Awake()
    {
        _cells = GetComponent<CellsSequence>();
    }

    public void AddToLastFreeCell(Item item)
    {
        if (Blocked || Filled)
            return;

        if (item)
        {
            if (_stockType == StockType.Single && ItemsType == item.ItemType)
            {
                Add(item);
                return;
            }

            if (_stockType == StockType.Multiple)
            {
                Add(item);
            }
        }
    }

    private void Add(Item item)
    {
        _items.Add(item);
        item.ItemMover.Transmit(_addItemDuration, _cells.GetCellByNumber(_items.Count - 1).transform);
        item.ItemMover.Scale();
        AddedItem?.Invoke();
        Lifespan += 1;
    }

    public Item Remove(ItemType itemType)
    {
        if (Empty)
            return null;

        Item removingItem = null;
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].ItemType == itemType)
            {
                removingItem = _items[i];
                break;
            }
        }

        if (removingItem)
        {
            removingItem.transform.parent = null;
            _items.Remove(removingItem);
            ResetItemsPositions();
        }

        return removingItem;
    }

    private void ResetItemsPositions()
    {
        if (_needResetAfterRemove)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].ItemMover.Transmit(0.2f, _cells.GetCellByNumber(i).transform);
            }
        }
    }

    public void Block(bool blocked)
    {
        Blocked = blocked;
    }

    public void Clear()
    {
        if (_itemsPool)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].transform.parent = _itemsPool.transform;
                _items[i].gameObject.SetActive(false);
            }
        }

        _items.Clear();
    }

    public int GetDemandedCount()
    {
        return _maxAllovedCapacity - _items.Count;
    }

    public int GetCount()
    {
        return _items.Count;
    }

    public void IncreaceMaxAllowedCapacity(int value)
    {
        _maxAllovedCapacity += value;
    }

    public void SetRandomCapacity()
    {
        _maxAllovedCapacity = UnityEngine.Random.Range(_currentMinCapacity, _currentMaxCapacity);
    }
}

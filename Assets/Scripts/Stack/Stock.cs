using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CellsSequence))]
public class Stock : MonoBehaviour 
{ 
    [SerializeField] private int _stockHeight;
    [SerializeField] private StockType _stockType = StockType.Single;
    [SerializeField] private ItemType _itemsType;

    private CellsSequence _cellsSequense;
    
    private Stack<Item> _items = new Stack<Item>();

    public event Action TakenItem;
    public event Action DroppedItem;

    public bool NotEmpty => _items.Count > 0;

    private void Start()
    {
        _cellsSequense = GetComponent<CellsSequence>();
        _cellsSequense.Build(_stockHeight);
    }

    public void Put(Item item)
    {
        bool isFull = _items.Count >= _stockHeight;
        if (isFull)
        {
            Debug.Log("Bag is full");
            return;
        }

        if(_stockType == StockType.Single)
        {
            if(item.ItemType == _itemsType)
            {
                _items.Push(item);
                TakenItem?.Invoke();
                return;
            }
        }    

        _items.Push(item);
        TakenItem?.Invoke();
    }

    public void DropOutOneItem()
    {
        bool isEmpty = _items.Count == 0;
        if (isEmpty)
        {
            Debug.Log("Bag is empty");
            return;
        }

        _items.Pop();
        DroppedItem?.Invoke();
    }

    public void Increase(int count)
    {
        _stockHeight += count;
    }

    public Item GetPeekItem()
    {
        return _items.Peek();
    }
}

public enum StockType
{
    Single,
    Multiple
}

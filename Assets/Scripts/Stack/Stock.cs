using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(CellsSequence))]
public class Stock : MonoBehaviour
{
    [SerializeField] private StockType _stockType;
    [SerializeField] private ItemType _itemsType;

    private CellsSequence _cellsSequense;

    private List<Item> _itemsPlacedInStack = new List<Item>();

    public event Action TakenItem;
    public event Action DroppedItem;

    public bool Empty => _itemsPlacedInStack.Count == 0;
    public bool Filled => _cellsSequense.CheckIfAllCellsAreNonEmpty();
    public bool Blocked { get; private set; }
    public StockType StockType => _stockType;
    public ItemType ItemsType => _itemsType;

    private void Start()
    {
        _cellsSequense = GetComponent<CellsSequence>();
    }

    public void PushToLastFreeCell(Item item)
    {
        if (Blocked)
            return;

        if (Filled)
            return;

        if (_stockType == StockType.Single)
        {
            if (ItemsType == item.ItemType)
            {
                Push(item);
                return;
            }
        }

        if (item.ItemType == ItemType.Money)
        {
            MoveToDestination(item, transform);
            return;
        }

        if (_stockType == StockType.Multiple)
        {
            Push(item);
        }
    }

    private void Push(Item item)
    {
        Cell lastEmpty = _cellsSequense.GetFirstEmptyCell();
        if (lastEmpty)
        {
            _itemsPlacedInStack.Add(item);
            LocateInEmptyCell(lastEmpty, item);
            lastEmpty.Fill();
            TakenItem?.Invoke();
        }
    }

    private void LocateInEmptyCell(Cell empty, Item item)
    {
        MoveToDestination(item, empty.transform);
        item.transform.rotation = empty.transform.rotation;
        item.transform.parent = empty.transform;
    }

    private void MoveToDestination(Item item, Transform destination)
    {
        item.ItemMover.SetDestination(destination);
        item.ItemMover.Move();
    }

    public Item Pull(ItemType itemType)
    {
        if (Empty)
            return null;

        if (_stockType == StockType.Single)
        {
            return UnparentTopItem();
        }

        if (_stockType == StockType.Multiple)
        {
            return Unparent(itemType);
        }

        return null;
    }

    private Item UnparentTopItem()
    {
        Item removing = _itemsPlacedInStack[_itemsPlacedInStack.Count - 1];
        removing.transform.parent = null;
        _itemsPlacedInStack.Remove(removing);
        _cellsSequense.GetTopNonEmptyCell().Clear();
        return removing;
    }

    private Item Unparent(ItemType itemType)
    {
        List<Item> items = new List<Item>();
        foreach (var item in _itemsPlacedInStack)
        {
            if (item.ItemType == itemType)
                items.Add(item);
        }

        int removeIndex = items.Count - 1;
        print(removeIndex + " Item index");
        items[removeIndex].transform.parent = null;
        _itemsPlacedInStack.RemoveAt(removeIndex);
        _cellsSequense.GetCellByNumber(removeIndex).Clear();
        print(_cellsSequense.GetCellByNumber(removeIndex) + " Cell by index");
        print(_cellsSequense.GetCellByNumber(removeIndex).IsEmpty + " Cell isEmpty");

        //ShiftDown();
        return items[removeIndex];
    }

    private void ShiftDown()
    {
        for (int i = 0; i < _cellsSequense.GetCount() + 1; i++)
        {
            if (_cellsSequense.GetCellByNumber(i).IsEmpty)
            {

            }
        }
    }

    public Item GetTopItem()
    {
        return _itemsPlacedInStack[_itemsPlacedInStack.Count - 1];
    }

    public void BlockSock()
    {
        Blocked = true;
    }

    public void UnblockSock()
    {
        Blocked = false;
    }

    public void Clear()
    {
        foreach (var item in _itemsPlacedInStack)
        {
            Destroy(item.gameObject);
        }
        _cellsSequense.ClearAllCells();
        _itemsPlacedInStack.Clear();
    }

    public int GetDemandedCount()
    {
        return _cellsSequense.GetCount() - _itemsPlacedInStack.Count;
    }

    public void FillAllCells() // shram
    {
        _cellsSequense.FillAllCells();
    }
}


public enum StockType
{
    Single,
    Multiple,
    ForMoney
}

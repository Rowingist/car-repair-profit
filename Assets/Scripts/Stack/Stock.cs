using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CellsSequence), typeof(ItemMover))]
public class Stock : MonoBehaviour
{
    [SerializeField] private StockType _stockType;
    [SerializeField] private ItemType _itemsType;
    

    private CellsSequence _cellsSequense;
    private ItemMover _itemMover;

    private Stack<Item> _itemsPlacedInStack = new Stack<Item>();

    public event Action TakenItem;
    public event Action DroppedItem;

    public bool Empty => _itemsPlacedInStack.Count == 0;
    public bool Filled => _cellsSequense.AllCellsAreFilled();
    public StockType StockType => _stockType;

    private void Start()
    {
        _cellsSequense = GetComponent<CellsSequence>();
        _itemMover = GetComponent<ItemMover>();
    }

    public void Push(Item item)
    {
        if (Filled)
            return;

        if (_stockType == StockType.Single)
        {
            if (item.ItemType == _itemsType)
            {
                LocateAccordingToCells(item);
                TakenItem?.Invoke();
                return;
            }
            return;
        }

        LocateAccordingToCells(item);
        TakenItem?.Invoke();
    }

    public Item Pull()
    {
        if (!Empty)
        {
            DroppedItem?.Invoke();
            UnparentTop();
            Item removing = GetTopItem();
            _itemsPlacedInStack.Pop();
            return removing;
        }

        return null;
    }

    private void LocateAccordingToCells(Item item)
    {
        Cell firstEmpty = _cellsSequense.GetFreeCellLocation();
        if (firstEmpty)
        {
            _itemsPlacedInStack.Push(item);
            _itemMover.SetTarget(item);
            _itemMover.SetInitialPosition(item.transform.position);
            item.transform.position = firstEmpty.transform.position;
            item.transform.localRotation = firstEmpty.transform.rotation;
            item.transform.parent = firstEmpty.transform;
            firstEmpty.Fill();
        }
    }

    private void UnparentTop()
    {
        Cell lastFilled = _cellsSequense.GetFirstFilledCell();
        if (lastFilled)
        {
            lastFilled.Clear();
            GetTopItem().transform.parent = null;
        }
    }

    public Item GetTopItem()
    {
        return _itemsPlacedInStack.Peek();
    }
}

public enum StockType
{
    Single,
    Multiple,
    ForMoney
}

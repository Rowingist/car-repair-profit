using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CellsSequence))]
public class Stock : MonoBehaviour
{
    [SerializeField] private GameObject _itemsPool;
    [SerializeField] private StockType _stockType;
    [SerializeField] private ItemType _itemsType;
    [SerializeField] private int _maxAllovedCapacity;

    private CellsSequence _cellsSequense;

    private List<Item> _itemsPlacedInStack = new List<Item>();

    public event Action TakenItem;
    public event Action DroppedItem;

    public bool Empty => _itemsPlacedInStack.Count == 0;
    public bool Filled => _maxAllovedCapacity - _itemsPlacedInStack.Count == 0;
    public bool Blocked { get; private set; }
    public StockType StockType => _stockType;
    public ItemType ItemsType => _itemsType;
    public int Lifespan { get; private set; }

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

        if (item)
        {
            if (_stockType == StockType.Single)
            {
                if (ItemsType == item.ItemType)
                {
                    Push(item);
                    return;
                }
            }

            if (_stockType == StockType.Multiple)
            {
                Push(item);
            }
        }
    }

    private void Push(Item item)
    {
        Cell lastEmpty = _cellsSequense.GetFirstEmptyCell();
        if (lastEmpty)
        {
            if (item)
            {
                _itemsPlacedInStack.Add(item);
                Lifespan += 1;
                LocateInEmptyCell(lastEmpty, item);
                lastEmpty.Fill();
                TakenItem?.Invoke();
            }
        }
    }

    private void LocateInEmptyCell(Cell empty, Item item)
    {
        MoveToDestination(item, empty.transform);
        item.transform.rotation = empty.transform.rotation;
        item.transform.parent = empty.transform;
    }

    public void MoveToDestination(Item item, Transform destination)
    {
        item.ItemMover.SetDestination(destination);
        item.ItemMover.Move();
    }

    public Item Pull(ItemType itemType)
    {
        if (Empty)
            return null;

        Item toRemove;
        Unparent(out toRemove, itemType);
        return toRemove;
    }

    public void PullFast(Transform destination)
    {
        StartCoroutine(Pulling(destination));
    }

    private IEnumerator Pulling(Transform destination)
    {
        for (int i = 0; i < _itemsPlacedInStack.Count; i++)
        {
            MoveToDestination(_itemsPlacedInStack[i], destination);
            yield return null;
        }
    }

    public void HideInPool()
    {
        for (int i = 0; i < _itemsPlacedInStack.Count; i++)
        {
            _itemsPlacedInStack[i].transform.parent = _itemsPool.transform;
            StartCoroutine(Scaling(_itemsPlacedInStack[i], 0.2f));
            _cellsSequense.GetCellByNumber(i).Clear();
        }
        _itemsPlacedInStack.Clear();
    }

    private IEnumerator Scaling(Item item, float duration)
    {
        float t = 0;
        while (t < 1)
        {
            item.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            t += Time.deltaTime / duration;
            yield return null;
        }
        item.transform.localScale = Vector3.one;
        item.gameObject.SetActive(false);
    }

    private void Unparent(out Item item, ItemType itemType)
    {
        Item toDelete = null;
        for (int i = _itemsPlacedInStack.Count - 1; i >= 0; --i)
        {
            if (_itemsPlacedInStack[i].ItemType == itemType)
            {
                toDelete = _itemsPlacedInStack[i];
                break;
            }
        }

        if (toDelete == null)
        {
            item = null;
            return;
        }

        toDelete.transform.parent = null;
        item = toDelete;
        _itemsPlacedInStack.Remove(toDelete);
        _cellsSequense.ClearAllCells();

        for (int i = 0; i < _itemsPlacedInStack.Count; i++)
        {
            LocateInEmptyCell(_cellsSequense.GetCellByNumber(i), _itemsPlacedInStack[i]);
            _cellsSequense.GetCellByNumber(i).Fill();
        }
    }

    public void Block()
    {
        Blocked = true;
    }

    public void Unblock()
    {
        Blocked = false;
    }

    public void Clear()
    {
        for (int i = 0; i < _cellsSequense.GetCount(); i++)
        {
            _cellsSequense.GetCellByNumber(i).Clear();
        }

        if (_itemsPool)
        {
            for (int i = 0; i < _itemsPlacedInStack.Count; i++)
            {
                _itemsPlacedInStack[i].transform.parent = _itemsPool.transform;
                _itemsPlacedInStack[i].gameObject.SetActive(false);
            }
        }

        _itemsPlacedInStack.Clear();
    }

    public int GetDemandedCount()
    {
        return _maxAllovedCapacity - _itemsPlacedInStack.Count;
    }

    public int GetCount()
    {
        return _itemsPlacedInStack.Count;
    }

    public void FillAllCells() // shram
    {
        _cellsSequense.FillAllCells();
    }

    public void IncreaceMaxAllowedCapacity(int value)
    {
        _maxAllovedCapacity += value;
    }
}


public enum StockType
{
    Single,
    Multiple
}

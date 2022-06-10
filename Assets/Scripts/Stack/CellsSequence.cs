using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellsSequence : MonoBehaviour
{
    [SerializeField] private float _spacingX;
    [SerializeField] private float _spacingY;
    [SerializeField] private float _spacingZ;
    [SerializeField] private Vector3Int _lenghtXHeightXWidth = new Vector3Int(1, 1, 1);
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Vector3 _rotationAfterBuild;
    [SerializeField] private Vector3 _itemsInStackRotation;

    private List<Cell> _cells = new List<Cell>();

    private void Start()
    {
        Build(_cellPrefab);
    }

    public void Build(Cell cellPrefab)
    {
        Vector2 offset = new Vector2((_lenghtXHeightXWidth.x - 1f) * 0.5f, (_lenghtXHeightXWidth.z - 1f) * 0.5f);
        for (int i = 0; i < _lenghtXHeightXWidth.y; i++)
        {
            for (int j = 0; j < _lenghtXHeightXWidth.x; j++)
            {
                for (int k = 0; k < _lenghtXHeightXWidth.z; k++)
                {
                    Vector3 position = new Vector3((j - offset.x) * _spacingX, _spacingY * i, (k - offset.y) * _spacingZ);
                    Cell newCell = Instantiate(cellPrefab, (position + transform.position), Quaternion.Euler(_itemsInStackRotation), transform);
                    _cells.Add(newCell);
                }
            }
        }
        transform.localRotation = Quaternion.Euler(_rotationAfterBuild);
    }

    public Cell GetFirstEmptyCell()
    {
        return _cells.First(c => c.IsEmpty == true);
    }

    public Cell GetTopNonEmptyCell()
    {
        return _cells.Last(c => c.IsEmpty == false);
    }

    public Cell GetTopEmptyCell()
    {
        return _cells.Last(c => c.IsEmpty == true);
    }

    public Cell GetCellByNumber(int index)
    {
        return _cells.ElementAt(index);
    }

    public bool CheckIfAllCellsAreNonEmpty()
    {
        var firstEmpty = _cells.FirstOrDefault(c => c.IsEmpty == true);
        return firstEmpty == null;
    }

    public void ClearAllCells()
    {
        foreach (var cell in _cells)
            cell.Clear();
    }

    public int GetCount()
    {
        return _cells.Count;
    }

    public void FillAllCells() // shram
    {
        foreach (var cell in _cells)
        {
            cell.Fill();
        }
    }
}
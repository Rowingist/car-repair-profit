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

    private Stack<Cell> _cells = new Stack<Cell>();

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
                    Cell newCell = Instantiate(cellPrefab, (position + transform.position), transform.rotation, transform);
                    _cells.Push(newCell);
                }
            }
        }
    }

    public Cell GetFreeCellLocation()
    {
        var firstEmpty = _cells.LastOrDefault(c => c.IsEmpty == true);
        return firstEmpty;
    }

    public Cell GetFirstFilledCell()
    {
        var firstFilled = _cells.FirstOrDefault(c => c.IsEmpty == false);
        return firstFilled;
    }

    public bool AllCellsAreFilled()
    {
        var firstFilled = _cells.FirstOrDefault(c => c.IsEmpty == true);
        return firstFilled == null;
    }

    public void ClearAllCells()
    {
        foreach (var cell in _cells)
        {
            cell.Clear();
        }
    }

    public int GetCount()
    {
        return _cells.Count;
    }
}
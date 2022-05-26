using UnityEngine;

public class CellsSequence : MonoBehaviour
{
    [SerializeField] private float _spacingX;
    [SerializeField] private float _spacingY;
    [SerializeField] private float _spacingZ;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private SequenseType _figure = SequenseType.Quad1X1;

    public void Build(int floors)
    {
        switch (_figure)    
        {
            case SequenseType.Quad1X1:
                BuildQuad1X1(floors);
                break;
            case SequenseType.Quad2x2:
                BuildFigure(floors, 2, 2);
                break;
            case SequenseType.Quad3x3:
                BuildFigure(floors, 3, 3);
                break;
            case SequenseType.Quad4x4:
                BuildFigure(floors, 4, 4);
                break;
            case SequenseType.Quad5x5:
                BuildFigure(floors, 5, 5);
                break;
            case SequenseType.Rectangle1x2:
                BuildFigure(floors, 1, 2);
                break;
            case SequenseType.Rectangle1x3:
                BuildFigure(floors, 1, 3);
                break;
            case SequenseType.Rectangle1x4:
                BuildFigure(floors, 1, 4);
                break;
            case SequenseType.Rectangle1x5:
                BuildFigure(floors, 1, 5);
                break;
            case SequenseType.Rectangle2x3:
                BuildFigure(floors, 2, 3);
                break;
            case SequenseType.Rectangle2x4:
                BuildFigure(floors, 2, 4);
                break;
            case SequenseType.Rectangle2x5:
                BuildFigure(floors, 2, 5);
                break;
            case SequenseType.Rectangle3x4:
                BuildFigure(floors, 3, 4);
                break;
            case SequenseType.Rectangle3x5:
                BuildFigure(floors, 3, 5);
                break;
            case SequenseType.Rectangle4x5:
                BuildFigure(floors, 4, 5);
                break;
            default:
                break;
        }
    }

    private void BuildQuad1X1(int y)
    {
        for (int i = 0; i < y; i++)
        {
            Vector3 floorHeight = transform.position;
            floorHeight.y += _spacingY * i;
            Instantiate(_cellPrefab, floorHeight, Quaternion.identity, transform);
        }
    }

    private void BuildFigure(int y, int x, int z)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    Vector3 position = new Vector3(j / _spacingX, transform.position.y + _spacingY * i, k / _spacingZ);
                    GameObject newCell = Instantiate(_cellPrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}

public enum SequenseType
{
    Quad1X1,
    Quad2x2,
    Quad3x3,
    Quad4x4,
    Quad5x5,
    Rectangle1x2,
    Rectangle1x3,
    Rectangle1x4,
    Rectangle1x5,
    Rectangle2x3,
    Rectangle2x4,
    Rectangle2x5,
    Rectangle3x4,
    Rectangle3x5,
    Rectangle4x5
}
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool _isEmpty;

    public bool IsEmpty => _isEmpty;

    private void Start()
    {
        _isEmpty = true;
    }

    public void Fill()
    {
        _isEmpty = false;
    }

    public void Clear()
    {
        _isEmpty = true;
    }
}

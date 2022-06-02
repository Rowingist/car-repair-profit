using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsEmpty { get; private set; }

    private void Start()
    {
        IsEmpty = true;
    }

    public void Fill()
    {
        IsEmpty = false;
    }

    public void Clear()
    {
        IsEmpty = true;
    }
}

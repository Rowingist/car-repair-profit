using System;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private float _transitionInterval = 0.2f;

    public event Action Entered;
    public event Action Left;

    public Area ConnectedArea { get; private set; }
    public Stock Stock => _stock;
    public float TransitionInterval => _transitionInterval;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Area>())
            Entered?.Invoke();

        if (other.TryGetComponent(out PullingArea pullArea))
        {
            ConnectedArea = pullArea;
        }
        else if (other.TryGetComponent(out PushingArea pushArea))
        {
            ConnectedArea = pushArea;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Area>())
        {
            Left?.Invoke();
        }
    }

    public void Push(Item item)
    {
        _stock.PushToLastFreeCell(item);
    }

    public Item Pull()
    {
        return _stock.Pull(ConnectedArea.Stock.ItemsType);
    }
}

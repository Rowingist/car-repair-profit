using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private Stack _stack;

    private int _count = 0;

    public int Count => _count;

    public Stack Stack => _stack;

    public bool _isFull => _count >= _stack.Places.Count;

    public event UnityAction<int> BrickCollected;
    public event UnityAction<int> BrickSell;

    public void Put()
    {
        _count++;
    }

    public Whell Sell()
    {
        Whell whell = null;

        if (_count > 0)
        {
            _count--;

            whell = transform.GetChild(_count).GetComponent<Whell>();

            BrickSell?.Invoke(_count);
        }

        return whell;
    }
}

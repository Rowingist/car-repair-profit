using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private Stack _stack;
    [SerializeField] private BoxCollider _brickCollector;

    private int _count = 0;

    public int Count => _count;

    public Stack Stack => _stack;

    private bool _isFull => _count >= _stack.Places.Count;

    public event UnityAction<int> BrickCollected;
    public event UnityAction<int> BrickSell;

    public void Put()
    {
        _count++;

      //  BrickCollected?.Invoke(_count);

        if (_isFull)
            _brickCollector.enabled = false;
    }

    public Whell Sell()
    {
        Whell whell = null;

        if (_count > 0)
        {
            _count--;

            whell = transform.GetChild(_count).GetComponent<Whell>();

            BrickSell?.Invoke(_count);

            if (_brickCollector.enabled == false && _isFull == false)
                _brickCollector.enabled = true;
        }

        return whell;
    }
}

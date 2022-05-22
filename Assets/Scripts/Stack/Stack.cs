using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stack : MonoBehaviour
{
    [SerializeField] private List<Place> _places;

    private int _currentCollected;

    public IReadOnlyList<Place> Places => _places;

    private int _needToBuy => _places.Count;

    public event UnityAction<int, int> AmountChanged;
    public event UnityAction Placed;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
            _places.Add(transform.GetChild(i).GetComponent<Place>());
    }

    private void Start()
    {
        AmountChanged?.Invoke(_needToBuy, _currentCollected);
    }

    public void ClearPlaces()
    {
        for (int i = 0; i < _places.Count; i++)
            _places[i].ClearStack();
    }

    public void Add()
    {
        _currentCollected++;
        Placed?.Invoke();

        AmountChanged?.Invoke(_needToBuy, _currentCollected);
    }
}

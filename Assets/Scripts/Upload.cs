using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Upload : MonoBehaviour
{
    public int _neeedToFix = 3; // свойство

    private int _currentUpload = 0; 
    private float _collectionDelay = 0.2f;
    private Coroutine CollectCoroutine;
    private BoxCollider _boxCollider;
    private bool _isCarArrivedToRepair = false;

    public event UnityAction CarFixed;
    public event UnityAction CarArrivedToDelivery;
    public event UnityAction<int,int> CountPartChanged;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (_isCarArrivedToRepair)
                CollectCoroutine = StartCoroutine(CollectFrom(player));
        }

        if (other.gameObject.TryGetComponent(out CarWhell carWhell))
        {
            CarArrivedToDelivery?.Invoke();
            _isCarArrivedToRepair = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (CollectCoroutine != null)
                StopCoroutine(CollectCoroutine);
        }

        if (other.gameObject.TryGetComponent(out CarWhell carWhell))
        {
            _isCarArrivedToRepair = false;
        }
    }

    private IEnumerator CollectFrom(Player player)
    {
        Whell whell = null;

        while (Physics.CheckBox(_boxCollider.center, _boxCollider.size))
        {
            whell = player.Bag.Sell();

            if (whell != null)
            {
                _currentUpload++;
                CountPartChanged?.Invoke(_currentUpload, _neeedToFix);

                MovablePrefab movable = whell.GetComponent<MovablePrefab>();

                movable.Unload();

                if (_currentUpload >= _neeedToFix)
                {
                    CarFixed?.Invoke();
                    _currentUpload = 0;
                    CountPartChanged?.Invoke(_currentUpload, _neeedToFix);
                    yield break;
                }
            }
            yield return new WaitForSeconds(_collectionDelay);
        }
    }
}

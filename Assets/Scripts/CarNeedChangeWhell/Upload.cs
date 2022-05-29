using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class Upload : MonoBehaviour
{
    [SerializeField] private MoneyPrefab _moneyPrefab;

    public int _neeedToFix = 3; // свойство

    private int _currentUpload = 0;
    private float _collectionDelay = 0.2f;
    private Coroutine CollectCoroutine;
    private BoxCollider _boxCollider;
    private bool _isCarArrivedToRepair = false;

    public event UnityAction CarFixed;
    public event UnityAction CarArrivedToDelivery;
    public event UnityAction<int, int> CountPartChanged;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(player.transform.DOScale(new Vector3(1, 1, 1), 0f));
            player.transform.SetParent(null);

            if (_isCarArrivedToRepair)
                CollectCoroutine = StartCoroutine(CollectFrom(player));
        }

        if (other.gameObject.TryGetComponent(out CarWhell carWhell))
        {
            CarArrivedToDelivery?.Invoke();
            _isCarArrivedToRepair = true;
        }

        if (other.gameObject.TryGetComponent(out CarRepair carRepair))
        {
            _isCarArrivedToRepair = true;
            CarArrivedToDelivery?.Invoke();
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
            _isCarArrivedToRepair = false;

        if (other.gameObject.TryGetComponent(out CarRepair carRepair))
            _isCarArrivedToRepair = false;
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
                    SpawnMoney(_currentUpload);

                    CarFixed?.Invoke();
                    _currentUpload = 0;
                    CountPartChanged?.Invoke(_currentUpload, _neeedToFix);
                    yield break;
                }
            }
            yield return new WaitForSeconds(_collectionDelay);
        }
    }

    private void SpawnMoney(int money)
    {
        for (int i = 0; i < money; i++)
        {
            Vector3 positionSpawnMoney = new Vector3(transform.position.x + i + 5, transform.position.y, transform.position.z);

            MoneyPrefab monye = Instantiate(_moneyPrefab, positionSpawnMoney, transform.rotation, null);
        }
    }
}

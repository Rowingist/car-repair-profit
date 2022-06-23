using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : ObjectPool
{
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private int _startSpawn;
    [SerializeField] private CellsSequence _cells;
    [SerializeField] private ParticleSystem _dollarsPoofEffect;

    private Transform _playerWallet;
    private Coroutine _transition;
    private Coroutine _transitionToTarget;
    private float _elapsedTime;
    private int _switchTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerWallet = player.WalletPoint;
            _transitionToTarget = StartCoroutine(TransitingToTarget());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Invoke(nameof(StopCollecting), 0.2f);
        }
    }

    private void StopCollecting()
    {
        if (_transitionToTarget != null)
            StopCoroutine(_transitionToTarget);
    }

    private void Start()
    {
        Initialize(_moneyPrefab);
        if (_startSpawn > 0)
            SpawnWithAnount(_startSpawn);
    }

    public void SpawnWithAnount(int value)
    {
       StartCoroutine(SpawningOnTable(value));
    }

    private IEnumerator SpawningOnTable(int amount)
    {
        yield return null;
        for (int i = 0; i < amount + 1; i++)
        {
            if (TryGetObject(out GameObject itemObject))
            {
                itemObject.gameObject.SetActive(true);
                itemObject.GetComponent<MoneyMover>().SetTargetPosition(false,
                    _cells.GetCellByNumber(i).transform.position);
                itemObject.GetComponent<MoneyMover>().enabled = true;                
            }
        }
    }

    private IEnumerator TransitingToTarget()
    {
        if (TryGetActiveObjects(out List<GameObject> activeItems))
        {
            for (int i = activeItems.Count-1; i >= 0 ; i--)
            {
                activeItems[i].GetComponent<MoneyMover>().
                    SetTargetPosition(true, _playerWallet.position);
                activeItems[i].GetComponent<MoneyMover>().enabled = true;
                yield return null;
            }
        }
    }
}

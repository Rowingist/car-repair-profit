using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : ObjectPool
{
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private int _startSpawn;
    [SerializeField] private CellsSequence _cells;
    [SerializeField] private ParticleSystem _dollarsPoofEffect;

    private Collider _collider;
    private Transform _target;
    private Coroutine _transitionToTarget;
    private Player _player;
    private MoneyDropArea _dropArea;

    private void Start()
    {
        Initialize(_moneyPrefab);
        _collider = GetComponent<Collider>();
        if (_startSpawn > 0)
            SpawnWithAnount(_startSpawn);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
            _target = player.WalletPoint;
            _transitionToTarget = StartCoroutine(TransitingToPlayer(true, _target));
        }

        if (other.TryGetComponent(out MoneyDropArea moneyDropArea))
        {
            _dropArea = moneyDropArea;
            _target = moneyDropArea.transform;
            _transitionToTarget = StartCoroutine(UpdatingWaletView(moneyDropArea.GetZonePrice(), _target));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() || other.GetComponent<MoneyDropArea>())
        {
            Invoke(nameof(StopTransmitting), 0.2f);
            _collider.enabled = true;
        }
    }

    private void StopTransmitting()
    {
        if (_transitionToTarget != null)
        {
            StopAllCoroutines();
        }
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        SpawnWithAnount(100);
    }

    public void SpawnWithAnount(int value)
    {
        StartCoroutine(SpawningOnTable(value));
    }

    private IEnumerator SpawningOnTable(int amount)
    {
        yield return null;
        _collider.enabled = false;
        for (int i = 0; i < amount; i++)
        {
            if (TryGetObject(out GameObject item))
            {
                item.gameObject.SetActive(true);
                MoneyMover mover = item.GetComponent<MoneyMover>();
                mover.SetTargetPosition(false, _cells.GetCellByNumber(i).transform);
                mover.enabled = true;
            }
        }
        _collider.enabled = true;
    }

    private IEnumerator TransitingToPlayer(bool disable, Transform target)
    {
        if (TryGetActiveObjects(out List<GameObject> activeItems))
        {
            for (int i = activeItems.Count - 1; i >= 0; --i)
            {
                MoneyMover mover = activeItems[i].GetComponent<MoneyMover>();
                mover.SetTargetPosition(disable, target);
                mover.enabled = true;
                _player.Replenish(activeItems[i].GetComponent<GameCash>().Value);
                yield return null;
            }
        }
    }

    private IEnumerator TransitingToOpeningArea(int amount, Transform target)
    {
        while (amount > 0)
        {
            if (TryGetObject(out GameObject item))
            {
                item.gameObject.SetActive(true);
                MoneyMover mover = item.GetComponent<MoneyMover>();
                mover.SetTargetPosition(true, target, transform);
                mover.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator UpdatingWaletView(int amount, Transform target)
    {
        yield return new WaitForSeconds(1f);
        Coroutine transition = StartCoroutine(TransitingToOpeningArea(amount, target));
        for (int i = 0; i < amount; i++)
        {
            if (TryGetObject(out GameObject item))
            {
                _dropArea.Player.PayForZone(item.GetComponent<GameCash>().Value);
                _dropArea.UpdateState(item.GetComponent<GameCash>().Value);
                if (amount <= 20)
                    yield return new WaitForSeconds(0.1f);
                else if(amount <= 60)
                    yield return new WaitForSeconds(0.01f);
                else
                    yield return null;
            }
        }
        StopCoroutine(transition);
        _dropArea.Deactivate();
    }
}

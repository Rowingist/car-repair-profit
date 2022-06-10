using System.Collections;
using UnityEngine;

public class MoneySpawner : ObjectPool
{
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private int _startSpawn;
    [SerializeField] private Stock _stock;
    [SerializeField] private ParticleSystem _dollarsPoofEffect;

    private void Start()
    {
        Initialize(_moneyPrefab);
        if (_startSpawn > 0)
            StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(0.1f);
        StartSpawn(_startSpawn);
    }

    public void StartSpawn(int amount)
    {
        for (int i = 0; i < amount - 1; i++)
        {
            TakeItem();
        }
        _dollarsPoofEffect.Play();
    }

    private void TakeItem()
    {
        if (TryGetObject(out GameObject itemObject))
        {
            if (_stock.Filled)
                return;

            SetItem(itemObject);
            _stock.PushToLastFreeCell(itemObject.GetComponent<Item>());
        }

    }

    private void SetItem(GameObject item)
    {
        item.transform.position = transform.position;
        item.SetActive(true);
    }
}

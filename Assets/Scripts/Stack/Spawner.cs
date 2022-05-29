using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private Item _item;
    [SerializeField] private Button _buy;
    [SerializeField] private int _startSpawn = 0;

    private Item _newItem;

    private void OnEnable()
    {
        Spawn();
        if (_startSpawn > 0)
            DepayedSpawn();

        _buy.onClick.AddListener(Push);
    }

    private void DepayedSpawn()
    {
        StartCoroutine(SpawnArray());
    }

    private IEnumerator SpawnArray()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _startSpawn; i++)
        {
            Push();
        }
    }

    private void OnDisable()
    {
        _buy.onClick.RemoveListener(Push);
    }

    private void Push()
    {
        if (_stock.Filled)
            return;

        _newItem.gameObject.SetActive(true);
        _stock.Push(_newItem);
        Spawn();
    }

    private void Spawn()
    {
        Item newItem = Instantiate(_item);
        _newItem = newItem;
        _newItem.gameObject.SetActive(false);
    }
}

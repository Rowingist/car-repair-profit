using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private Item _item;
    [SerializeField] private int _startSpawn = 0;

    private Item _newItem;

    private void OnEnable()
    {
        Spawn();
    }

    private void Start()
    {
        if (_startSpawn > 0)
            DelayedSpawn();
    }

    private void DelayedSpawn()
    {
        StartCoroutine(SpawnArray());
    }

    private IEnumerator SpawnArray()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _startSpawn; i++)
        {
            Push();
            yield return new WaitForSeconds(0.005f);
        }
    }

    public void Push()
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
        newItem.transform.position = transform.position;
        _newItem = newItem;
        _newItem.gameObject.SetActive(false);
    }
}

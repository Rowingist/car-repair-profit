using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private Item _item;
    [SerializeField] private int _startSpawn = 0;
    [SerializeField] private Player _player;
    [SerializeField] private float _firstInitDelay = 0.5f;
    [SerializeField] private float _secondsBetweenArraySpawn = 0.005f;

    private Item _newItem;

    private void OnEnable()
    {
        Spawn();
        _player.Payed += Push;
    }

    private void Start()
    {
        if (_startSpawn > 0)
            DelayedSpawn(_startSpawn, _firstInitDelay, _secondsBetweenArraySpawn);
    }

    private void OnDisable()
    {
        _player.Payed -= Push;
    }

    public void DelayedSpawn(int amount, float delay = 0.5f, float sesondsBetween = 0.005f)
    {
        StartCoroutine(SpawningArray(amount, delay, sesondsBetween));
    }

    private IEnumerator SpawningArray(int startSpawn, float delay, float sesondsBetween)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < startSpawn; i++)
        {
            Push();
            yield return new WaitForSeconds(sesondsBetween);
        }
    }

    private void Push()
    {
        if (_stock.Filled)
            return;

        if (_item.ItemType != ItemType.Money && _player.ByingItemType == _item.ItemType)
        {
            _newItem.gameObject.SetActive(true);
            _stock.Push(_newItem);
            Spawn();
        }
        else if(_item.ItemType == ItemType.Money)
        {
            _newItem.gameObject.SetActive(true);
            _stock.Push(_newItem);
            Spawn();
        }
        else
        {
            return;
        }
    }

    private void Spawn()
    {
        Item newItem = Instantiate(_item);
        newItem.transform.position = transform.position;
        _newItem = newItem;
        _newItem.gameObject.SetActive(false);
    }
}

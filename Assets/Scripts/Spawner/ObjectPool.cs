using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    public Transform Container => _container.transform;

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected void InitializeRandom(GameObject[] prefabs)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int randIndex = Random.Range(0, prefabs.Length);
            GameObject spawned = Instantiate(prefabs[randIndex], _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    protected bool TryGetActiveObjects(out List<GameObject> result)
    {
        var activeList = _pool.Where(p => p.activeSelf.Equals(true)).ToList();
        result = activeList;
        return result != null;
    }
}

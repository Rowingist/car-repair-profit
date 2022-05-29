using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnerPartOne : MonoBehaviour
{   
    [SerializeField] private Stack _shopStack;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PartOne _partOneTemplate;
    [SerializeField] private Shop _shop;

    private void OnEnable()
    {
        _shop.OrderPlaced += OnDeliverParts;
    }

    private void OnDisable()
    {
        _shop.OrderPlaced -= OnDeliverParts;
    }

    private void OnDeliverParts(int countParts)
    {
        for (int i = 0; i < countParts; i++)
            InstantiatePrefab();
    }

    private void InstantiatePrefab()
    {
        Place place = _shopStack.Places.FirstOrDefault(place => place.IsAvailible);
        if (place != null)
        {
            PartOne part = Instantiate(_partOneTemplate, _spawnPoint.position, _partOneTemplate.transform.rotation);

            part.GetComponent<MovablePrefab>().MoveOnShalve(place.transform.position);

            part.transform.SetParent(place.transform);

            place.ReservePart(part);

            _shopStack.Add();
        }
    }
}

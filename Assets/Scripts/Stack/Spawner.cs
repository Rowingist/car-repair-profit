using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Stack _shopStack;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Whell _whellTemplate;
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
        {
            InstantiatePrefab();
        }
    }

    private void InstantiatePrefab()
    {
        Place place = _shopStack.Places.FirstOrDefault(place => place.IsAvailible);
        if (place != null)
        {
            Whell whell = Instantiate(_whellTemplate, _spawnPoint.position, _whellTemplate.transform.rotation);

            whell.GetComponent<MovablePrefab>().MoveOnShalve(place.transform.position);

            place.Reserve(whell);

            _shopStack.Add();
            _shopStack.Add();
        }
    }
}

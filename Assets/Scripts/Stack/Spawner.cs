using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Stack _shopStack;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Whell _whellTemplate;
  //  [SerializeField] private BoxCollider _collectableArea;

    private float _elapsedTime = 0;
    private float _currentSpawnDelay = 0.5f;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _currentSpawnDelay)
        {
            InstantiatePrefab();

            _elapsedTime = 0;
        }
    }

    private void InstantiatePrefab()
    {
        Place place = _shopStack.Places.FirstOrDefault(place => place.IsAvailible);

        if (place != null)
        {
            Whell whell = Instantiate(_whellTemplate, _spawnPoint.position, _whellTemplate.transform.rotation);
            whell.GetComponent<MovablePrefab>().MoveOnShalve(place.transform.position);

           // whell.transform.SetParent(_collectableArea.transform);
            place.Reserve(whell);

            _shopStack.Add();
            _shopStack.Add();
        }
    }
}

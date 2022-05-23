using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Linq;


public class DeliveryArea : MonoBehaviour
{
    [SerializeField] private Stack _deliveryStack;
    [SerializeField] private BoxCollider _deliveryArea;
    [SerializeField] private float _collectionDelay = 0.1f;

    private Car _car;
    private Coroutine CollectCoroutine;

    public event UnityAction CarArrivaedToDelivery;
    public event UnityAction PlayerTakeTheCar;

    public event UnityAction CarFixed;
    public event UnityAction<Whell> Collected;

    private void OnEnable()
    {
        Collected += OnBrickCollected;
        _deliveryStack.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Collected -= OnBrickCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            //  player.transform.SetParent(_car.transform);

            // player.gameObject.SetActive(false);

            PlayerTakeTheCar?.Invoke();

            CollectCoroutine = StartCoroutine(CollectFrom(player));
        }

        if (other.gameObject.TryGetComponent(out Car car))
        {
            CarArrivaedToDelivery?.Invoke();

            _car = car;

            _deliveryStack.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (CollectCoroutine != null)
                StopCoroutine(CollectCoroutine);
        }

        if (other.gameObject.TryGetComponent(out Car car))
            _deliveryStack.gameObject.SetActive(false);
    }

    private void OnBrickCollected(Whell whell)
    {
        _deliveryStack.Add();
    }

    private IEnumerator CollectFrom(Player player)
    {
        Whell whell = null;

        while (Physics.CheckBox(_deliveryArea.center, _deliveryArea.size))
        {
            Place place = _deliveryStack.Places.FirstOrDefault(place => place.IsAvailible);

            if (place != default)
            {
                whell = player.Bag.Sell();

                if (whell != null)
                {
                    MovablePrefab movable = whell.GetComponent<MovablePrefab>();

                    movable.Unload();

                    place.Reserve(whell);

                    Collected?.Invoke(whell);
                }
            }

            if (place == default)
            {
                CarFixed?.Invoke();
                _deliveryStack.ClearPlaces();
                yield break;
            }
            yield return new WaitForSeconds(_collectionDelay);
        }
    }


}

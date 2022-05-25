using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Linq;


public class WhellArea : MonoBehaviour
{
    [SerializeField] private Stack _whellStack;
    [SerializeField] private BoxCollider _whellArea;
    [SerializeField] private float _collectionDelay = 0.1f;

    private CarWhell _car;
    private Coroutine CollectCoroutine;

    public event UnityAction CarArrivaedToWhell;

    public event UnityAction CarFixed;
    public event UnityAction<Wheel> Collected;

    private void OnEnable()
    {
        Collected += OnWhellCollected;
        _whellStack.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Collected -= OnWhellCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            CollectCoroutine = StartCoroutine(CollectFrom(player));
        }

        if (other.gameObject.TryGetComponent(out CarWhell car))
        {
            CarArrivaedToWhell?.Invoke();

            _car = car;

            _whellStack.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (CollectCoroutine != null)
                StopCoroutine(CollectCoroutine);
        }

        if (other.gameObject.TryGetComponent(out CarWhell car))
            _whellStack.gameObject.SetActive(false);
    }

<<<<<<< HEAD:Assets/Scripts/Stack/DeliveryArea.cs
    private void OnBrickCollected(Wheel whell)
=======
    private void OnWhellCollected(Whell whell)
>>>>>>> 7067b379ae380adeaa5c24a71056d34997ca305e:Assets/Scripts/Stack/WhellArea.cs
    {
        _whellStack.Add();
    }

    private IEnumerator CollectFrom(Player player)
    {
        Wheel whell = null;

        while (Physics.CheckBox(_whellArea.center, _whellArea.size))
        {
            Place place = _whellStack.Places.FirstOrDefault(place => place.IsAvailible);

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
                _whellStack.ClearPlaces();
                yield break;
            }
            yield return new WaitForSeconds(_collectionDelay);
        }
    }
}

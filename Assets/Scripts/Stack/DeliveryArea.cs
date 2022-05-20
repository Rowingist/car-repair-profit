using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class DeliveryArea : MonoBehaviour
{
    [SerializeField] private Stack _deliveryStack;
    [SerializeField] private BoxCollider _deliveryArea;
    [SerializeField] private float _collectionDelay;

    private Coroutine CollectCoroutine;

    public event UnityAction<Whell> Collected;
    public event UnityAction EnterArea; // пока не используется
    public event UnityAction ExitArea; // пока не используется
    public event UnityAction CarFixed;

    private void OnEnable()
    {
        Collected += OnBrickCollected;
        _deliveryStack.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            EnterArea?.Invoke();

            CollectCoroutine = StartCoroutine(CollectFrom(player));
        }

        if (other.gameObject.TryGetComponent(out Car car))
            _deliveryStack.gameObject.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
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
            CollectedAll();

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
                yield break;
            }
            yield return new WaitForSeconds(_collectionDelay);
        }
    }

    public bool CollectedAll()
    {
        Place place = _deliveryStack.Places.FirstOrDefault(place => place.IsAvailible);
        return (place == null);
    }
}

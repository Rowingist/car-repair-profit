using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class RepairArea : MonoBehaviour
{
    [SerializeField] private Stack _deliveryStack;
    [SerializeField] private BoxCollider _repairArea;
    [SerializeField] private float _collectionDelay;
    [SerializeField] private Player _player;

    private Coroutine CollectCoroutine;
    public event UnityAction<Whell> Collected;
    public event UnityAction CarFixed;

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
            CollectCoroutine = StartCoroutine(CollectFrom(player));
        }

        if (other.gameObject.TryGetComponent(out CarRepair car))
        {
            StartCoroutine(EnableOnTimer());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (CollectCoroutine != null)
                StopCoroutine(CollectCoroutine);
        }

        if (other.gameObject.TryGetComponent(out CarRepair car))
            _deliveryStack.gameObject.SetActive(false);
    }

    private IEnumerator EnableOnTimer()
    {
        float timeLeft = 1;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            yield return null;
        }

        _player.transform.SetParent(null);
        _player.gameObject.SetActive(true);
        _deliveryStack.gameObject.SetActive(true);
    }

    private void OnBrickCollected(Whell whell)
    {
        _deliveryStack.Add();
    }

    private IEnumerator CollectFrom(Player player)
    {
        Whell whell = null;

        while (Physics.CheckBox(_repairArea.center, _repairArea.size))
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

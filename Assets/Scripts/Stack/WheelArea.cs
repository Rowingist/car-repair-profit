using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


public class WheelArea : MonoBehaviour
{
    //[SerializeField] private StackPack _whellStack;
    [SerializeField] private BoxCollider _whellArea;
    [SerializeField] private float _collectionDelay = 0.1f;

    private CarWheel _car;
    private Coroutine CollectCoroutine;

    public event UnityAction CarArrivaedToWhell;

    public event UnityAction CarFixed;
    public event UnityAction<Item> Collected;

    private void OnEnable()
    {
        Collected += OnWhellCollected;
        //_whellStack.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Collected -= OnWhellCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            //CollectCoroutine = StartCoroutine(CollectFrom(player));
        }

        if (other.gameObject.TryGetComponent(out CarWheel car))
        {
            CarArrivaedToWhell?.Invoke();

            _car = car;

            //_whellStack.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (CollectCoroutine != null)
                StopCoroutine(CollectCoroutine);
        }

        //if (other.gameObject.TryGetComponent(out CarWheel car))
            //_whellStack.gameObject.SetActive(false);
    }


    private void OnWhellCollected(Item whell)
    {
        //_whellStack.Add();
    }

    //private IEnumerator CollectFrom(Player player)
    //{
    //    Item whell = null;

    //    while (Physics.CheckBox(_whellArea.center, _whellArea.size))
    //    {
    //        Tower place = _whellStack.Places.FirstOrDefault(place => place.IsAvailible);

    //        if (place != default)
    //        {
    //            //whell = player.Bag.Sell();

    //            if (whell != null)
    //            {
    //                MovablePrefab movable = whell.GetComponent<MovablePrefab>();

    //                movable.Unload();

    //                place.Reserve(whell);

    //                Collected?.Invoke(whell);
    //            }
    //        }

    //        if (place == default)
    //        {
    //            CarFixed?.Invoke();
    //            _whellStack.ClearPlaces();
    //            yield break;
    //        }
    //        yield return new WaitForSeconds(_collectionDelay);
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using DG.Tweening;

public class CollectableArea : MonoBehaviour
{
    //[SerializeField] private StackPack _collectableStack;
    [SerializeField] private float _collectionDelay;
    [SerializeField] private BoxCollider _collectableArea;

    public event UnityAction Taken;
    private Coroutine CollectCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out Bag bag))
            //CollectCoroutine = StartCoroutine(CollectFrom(bag));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Stock>())
        {
            if (CollectCoroutine != null)
                StopCoroutine(CollectCoroutine);
        }
    }

    private void CollectToBag(Stock bag, Item whell)
    {
        float flyingEffectValue = 1.5f;
        float flightTime = 0.1f;
        Vector3 rotateInStack = new Vector3(0, 0, -90);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(whell.transform.DOLocalMoveZ(flyingEffectValue * (-1), flightTime).SetRelative());
        sequence.Append(whell.transform.DOLocalMoveZ(flyingEffectValue, flightTime).SetRelative());
        sequence.Append(whell.transform.DOLocalRotate(rotateInStack, 0.3f));

        whell.transform.SetParent(bag.transform);
        //whell.transform.position = bag.Stack.Places[bag.Count].transform.position;
    }

    //private IEnumerator CollectFrom(Bag bag)
    //{
    //    Item whell = null;

    //    while (Physics.CheckBox(_collectableArea.center, _collectableArea.size))
    //    {
    //        //if (bag._isFull)
    //        //    yield break;

    //        //Tower place = _collectableStack.Places.FirstOrDefault(place => place.IsAvailible == false);
    //        if (place != null)
    //        {
    //            whell = place.GetComponentInChildren<Item>();
    //            whell.transform.DOLocalMoveX(-3, 0.5f).SetRelative();
    //            //place.ClearStack();

    //            CollectToBag(bag, whell);
    //            //bag.Put();
    //            Taken?.Invoke();
    //        }

    //        yield return new WaitForSeconds(_collectionDelay);
    //    }
    //}
}

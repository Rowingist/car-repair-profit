using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    public event UnityAction Taken;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Bag bag))
        {
            CollectToBag(bag);

            bag.Put();

            Taken?.Invoke();
        }
    }

    private void CollectToBag(Bag bag)
    {
        float FlyingEffectValue = 0.5f;
        float FlightTime = 0.05f;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveZ(FlyingEffectValue * (-1), FlightTime).SetRelative());
        sequence.Append(transform.DOLocalMoveZ(FlyingEffectValue, FlightTime).SetRelative());
        sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -90f), 0f));

        transform.SetParent(bag.transform);
        transform.position = bag.Stack.Places[bag.Count].transform.position;
    }
    //private void CollectToBag(Bag bag)
    //{
    //    float FlyingEffectValue = 2f;
    //    float FlightTime = 0.1f;

    //    Sequence sequence = DOTween.Sequence();
    //    sequence.Append(transform.DOLocalMoveZ(FlyingEffectValue, FlightTime).SetRelative());
    //    sequence.Append(transform.DOLocalMoveZ(FlyingEffectValue * (-1), FlightTime).SetRelative());

    //    transform.SetParent(bag.transform);
    //    transform.position = bag.Stack.Places[bag.Count].transform.position;
    //   // transform.rotation = bag.Stack.Places[bag.Count].transform.rotation;
    //}
}

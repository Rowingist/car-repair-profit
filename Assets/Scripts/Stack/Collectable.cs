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
        float flyingEffectValue = 1.5f;
        float flightTime = 0.1f;
        Vector3 rotateInStack = new Vector3(0, 0, -90);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveZ(flyingEffectValue * (-1), flightTime).SetRelative());
        sequence.Append(transform.DOLocalMoveZ(flyingEffectValue, flightTime).SetRelative());
        sequence.Append(transform.DOLocalRotate(rotateInStack, 0.3f));

        transform.SetParent(bag.transform);
        transform.position = bag.Stack.Places[bag.Count].transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovablePrefab : MonoBehaviour // можно вынести в класс Whell
{
    public void MoveOnShalve(Vector3 targetPosition)
    {
        float FlyingEffectValue = 1f;
        float FlightTime = 0.3f;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(FlyingEffectValue, FlightTime).SetRelative());
        sequence.Append(transform.DOMove(targetPosition, FlightTime));
    }

    public void Unload()
    {
        Destroy(gameObject);
    }
}

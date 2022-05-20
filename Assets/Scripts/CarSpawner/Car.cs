using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    private CarSpawner _carSpawner;

    private void Start()
    {
        MoveToGarage();
    }

    public void InitSpawner(CarSpawner carSpawner)
    {
        _carSpawner = carSpawner;
    }


    private void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawner._deliveryPoint.position, 2f));
    }
}

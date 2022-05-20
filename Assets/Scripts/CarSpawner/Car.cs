using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    private CarSpawner _carSpawner;

    public void InitSpawner(CarSpawner carSpawner)
    {
        _carSpawner = carSpawner;
    }

    private void Start()
    {
        MoveToGarage();
    }

    private void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawner._deliveryPoint.position, 2f));
    }

    public void MoveAfterRepair()
    {
        Sequence sequence = DOTween.Sequence();
        //sequence.Append(transform.DOMove(_carSpawner._spawnPoint .position, 2f));
        sequence.Append(transform.DOLocalMoveY(5, 1));
        //  gameObject.SetActive(false);
    }

}
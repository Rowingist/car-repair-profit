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

    public void MoveToGarage()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawner._deliveryPoint.position, 2f));
    }

    public void MoveAfterRepair()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_carSpawner._spawnPoint .position, 2f));
        Destroy(gameObject, 3);
    }
}

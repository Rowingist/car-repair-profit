using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Shop : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameCamera; // в отдельный класс
    [SerializeField] private CinemachineVirtualCamera _shopCamera; // в отдельный класс


    public event UnityAction<int> OrderPlaced;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _gameCamera.Priority = 0;
            _shopCamera.Priority = 1;

            OrderPlaced?.Invoke(CalculatePartsCount());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _gameCamera.Priority = 1;
            _shopCamera.Priority = 0;
        }
    }

    private int CalculatePartsCount()
    {
        return Random.Range(5, 10);
    }
}

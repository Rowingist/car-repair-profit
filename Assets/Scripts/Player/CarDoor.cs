using System;
using UnityEngine;

public class CarDoor : MonoBehaviour
{
    [SerializeField] private Transform _car;

    public event Action Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerToCarTransition player))
        {
            Entered?.Invoke();
            player.GetInto(_car);
        }
    }
}

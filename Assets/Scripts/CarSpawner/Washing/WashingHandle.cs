using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingHandle : MonoBehaviour
{
    [SerializeField] private CarHandle _carHandle;

    private ParticleSystem _washParticle;

    private void Start()
    {
        _washParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_carHandle.IsInBox)
            {
                _washParticle.Play();
                _carHandle.PushButtonStartWash();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {

        }
    }

}

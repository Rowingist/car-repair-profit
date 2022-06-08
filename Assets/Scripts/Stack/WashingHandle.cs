using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WashingHandle : MonoBehaviour
{
    [SerializeField] private CarHandle _carHandle;

    private int _carWashedCount;
    private ParticleSystem _washParticle;
    private bool _isWashingTutorialComplete = false;

    public event UnityAction ManyCarsWashed;

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

                _carWashedCount++;

                if (!_isWashingTutorialComplete && _carWashedCount >= 2)
                {
                    ManyCarsWashed?.Invoke();
                    _isWashingTutorialComplete = true;
                }
            }
        }
    }
}

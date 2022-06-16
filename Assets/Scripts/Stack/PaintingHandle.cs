using UnityEngine;
using UnityEngine.Events;

public class PaintingHandle : MonoBehaviour
{
    [SerializeField] private CarHandle _carHandle;
    [SerializeField] private ParticleSystem _washParticle;
    [SerializeField] private Stock _relatedStock;
    [SerializeField] private ParticleSystem _areaParticle;
    [SerializeField] private ParticleSystem _buttonPushParticle; 

    private int _carWashedCount;
    private bool _isWashingTutorialComplete = false;

    public event UnityAction ManyCarsWashed;

    private void OnEnable()
    {
        _carHandle.CarInzone += OnStartParticle;
        _carHandle.CarWashed += OnStopParticle;
    }

    private void OnDisable()
    {
        _carHandle.CarInzone -= OnStartParticle;
        _carHandle.CarWashed -= OnStopParticle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_carHandle.IsInBox && _relatedStock.GetDemandedCount() == 0)
            {
                _washParticle.Play();
                _buttonPushParticle.Play();
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

    private void OnStartParticle()
    {
        _areaParticle.Play();
    }

    private void OnStopParticle()
    {
        _areaParticle.Stop();
    }
}

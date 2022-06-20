using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WashingHandle : MonoBehaviour
{
    [SerializeField] private CarHandle _carHandle;
    [SerializeField] private ParticleSystem _washParticle;
    [SerializeField] private Stock _relatedStock;
    [SerializeField] private ParticleSystem _areaParticle;
    [SerializeField] private ParticleSystem _buttonPushParticle;

    private bool _isWashingTutorialComplete = false;
    private int _carWashedCount;
    private int _carWashedToOpenNewZone = 3;
    private bool _isButtonPressed = false;

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
            if (_carHandle.IsInBox && !_isButtonPressed)
            {

                _washParticle.Play();
                _buttonPushParticle.Play();
                _carHandle.PushButtonStartWash();
                _isButtonPressed = true;

                StartCoroutine(PushByttonOnTimer());

                _carWashedCount++;

                if (!_isWashingTutorialComplete && _carWashedCount >= _carWashedToOpenNewZone)
                {
                    ManyCarsWashed?.Invoke();
                    _isWashingTutorialComplete = true;
                }
            }
        }
    }

    private IEnumerator PushByttonOnTimer()
    {
        float timeLeft = 7f;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        _isButtonPressed = false;
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

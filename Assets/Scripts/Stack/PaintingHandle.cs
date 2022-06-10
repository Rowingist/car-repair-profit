using UnityEngine;
using UnityEngine.Events;

public class PaintingHandle : MonoBehaviour
{
    [SerializeField] private CarHandle _carHandle;
    [SerializeField] private ParticleSystem _washParticle;
    [SerializeField] private Stock _relatedStock;

    private int _carWashedCount;
    private bool _isWashingTutorialComplete = false;

    public event UnityAction ManyCarsWashed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_carHandle.IsInBox && _relatedStock.GetDemandedCount() == 0)
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

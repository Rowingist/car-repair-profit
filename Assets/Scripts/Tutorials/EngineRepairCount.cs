using UnityEngine;
using UnityEngine.Events;

public class EngineRepairCount : MonoBehaviour
{
    private int _carRepaered;
    private int _needCarToUnlock = 8;

    public event UnityAction CarExitFromEngine;

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CarCleaner car))
        {
            _carRepaered++;

            if (_carRepaered == _needCarToUnlock)
                CarExitFromEngine?.Invoke();
        }
    }
}

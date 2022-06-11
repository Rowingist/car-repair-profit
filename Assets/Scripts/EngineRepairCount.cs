using UnityEngine;
using UnityEngine.Events;

public class EngineRepairCount : MonoBehaviour
{
    private int _carRepaered;

    public event UnityAction CarExitFromEngine;

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CarCleaner car))
        {
            _carRepaered++;

            if (_carRepaered == 2)
                CarExitFromEngine?.Invoke();
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

public class OpenWahTutorial : MonoBehaviour
{
    private bool _isTutorialComlete = false;

    public event UnityAction FirstTutorialMoneyZoneLeft;

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.TryGetComponent(out Player player)) && !_isTutorialComlete)
        {
            FirstTutorialMoneyZoneLeft?.Invoke();

            _isTutorialComlete = true;
        }
    }
}

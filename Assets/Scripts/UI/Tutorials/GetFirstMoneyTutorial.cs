using UnityEngine;
using UnityEngine.Events;


public class GetFirstMoneyTutorial : MonoBehaviour
{
    private bool _isTutorialComlete = false;

    public event UnityAction PlayerExitFromMoneyArea;


    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.TryGetComponent(out Player player)) && !_isTutorialComlete)
        {
            PlayerExitFromMoneyArea?.Invoke();

            _isTutorialComlete = true;
        }
    }
}

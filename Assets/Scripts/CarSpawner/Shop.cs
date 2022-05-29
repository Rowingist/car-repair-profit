using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public UnityEvent GotInto;
    public UnityEvent WentOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            print("player");
            GotInto?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            WentOut?.Invoke();
        }
    }
}

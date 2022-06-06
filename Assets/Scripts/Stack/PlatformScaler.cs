using MoreMountains.Feedbacks;
using UnityEngine;

public class PlatformScaler : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _scaleFeedback;
    [SerializeField] private MMFeedbacks _unscaleFeedback;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>()) { _scaleFeedback?.PlayFeedbacks(); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>()) { _unscaleFeedback?.PlayFeedbacks(); }
    }
}

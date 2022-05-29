using MoreMountains.Feedbacks;
using UnityEngine;

public class PlatformScaler : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _scaleFeedback;
    [SerializeField] private MMFeedbacks _unscaleFeedback;
    [SerializeField] private Area _relatedArea;

    private void OnEnable()
    {
        _relatedArea.Entered += OnPlayScaleFeedback;
        _relatedArea.Left += OnPlayUnscaleFeedback;
    }

    private void OnDisable()
    {
        _relatedArea.Entered -= OnPlayScaleFeedback;
        _relatedArea.Left -= OnPlayUnscaleFeedback;
    }

    private void OnPlayScaleFeedback()
    {
        _scaleFeedback?.PlayFeedbacks();
    }

    private void OnPlayUnscaleFeedback()
    {
        _unscaleFeedback?.PlayFeedbacks();
    }
}

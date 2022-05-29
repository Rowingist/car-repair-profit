using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private Transform _item;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private MMFeedbacks _scaleFeedback;

    private Transform _target;

    public void SetDestination(Transform destination)
    {
        _target = destination;
    }

    public void Move()
    {
        StartCoroutine(UpdateInitialPosition(1f));
        _scaleFeedback.Initialization();
        _scaleFeedback?.PlayFeedbacks();
    }

    private IEnumerator UpdateInitialPosition(float updateTime)
    {
        float t = 0;
        while (t < 1)
        {
            _item.transform.position = Vector3.Lerp(_item.transform.position, _target.position, t);
            t += Time.deltaTime / updateTime;
            yield return null;
        }
    }

}

using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform;
    [SerializeField] private float _transitionTime;
    [SerializeField] private MMFeedbacks _scaleFeedback;

    private Transform _target;

    public void SetDestination(Transform destination)
    {
        _target = destination;
    }

    public void Move()
    {
        StartCoroutine(UpdateInitialPosition(_transitionTime));
        if (_scaleFeedback)
        {
            _scaleFeedback.Initialization();
            _scaleFeedback?.PlayFeedbacks();
        }
    }

    private IEnumerator UpdateInitialPosition(float updateTime)
    {
        float t = 0;
        while (t < 1)
        {
            _itemTransform.transform.position = Vector3.Lerp(_itemTransform.transform.position, _target.position, t);
            t += Time.deltaTime / updateTime;
            yield return null;
        }
    }

}

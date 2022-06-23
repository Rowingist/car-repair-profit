using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _scaleFeedback;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public void Scale()
    {
        if (_scaleFeedback)
        {
            _scaleFeedback.Initialization();
            _scaleFeedback?.PlayFeedbacks();
        }
    }

    public void Transmit(float durationInSeconds, Transform targetPosition)
    {
        StartCoroutine(Moving(durationInSeconds, targetPosition));
    }

    private IEnumerator Moving(float moveDuration, Transform target)
    {
        float t = 0;
        while (t < 1)
        {
            _transform.position = Vector3.Lerp(_transform.position, target.position, t);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, target.rotation, t);
            t += Time.deltaTime / moveDuration;
            yield return null;
        }
        _transform.position = target.position;
        _transform.parent = target;
    }
}

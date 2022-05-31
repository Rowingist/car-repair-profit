using System.Collections;
using UnityEngine;

public class PlayerToCarTransition : MonoBehaviour
{
    [SerializeField] private Transform _bodyForparenting;
    [SerializeField] private Transform _noParentPoint;
    [SerializeField] private Stock _playerStock;

    public void GetInto(Transform _carSeat)
    {
        _playerStock.gameObject.SetActive(false);
        _bodyForparenting.parent = _carSeat;
        StartCoroutine(Transition(0.15f, _carSeat));
        StartCoroutine(Scaling(0.5f, Vector3.zero));
    }

    public void Exit(Transform _carExit)
    {
        _playerStock.gameObject.SetActive(true);
        _bodyForparenting.parent = null;
        StartCoroutine(Transition(0.15f, _carExit));
        StartCoroutine(Scaling(0.5f, Vector3.one));
    }

    private IEnumerator Transition(float animationTime, Transform target)
    {
        _noParentPoint.position = target.position;
        float t = 0;
        while (t < 1)
        {
            _bodyForparenting.position = Vector3.Lerp(_bodyForparenting.position, _noParentPoint.position, t);
            t += Time.deltaTime / animationTime;
            yield return null;
        }
    }
    private IEnumerator Scaling(float animationTime, Vector3 scale)
    {
        float t = 0;
        while (t < 1)
        {
            _bodyForparenting.localScale = Vector3.Lerp(_bodyForparenting.localScale, scale, t);
            t += Time.deltaTime / animationTime;
            yield return null;
        }
    }

}

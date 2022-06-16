using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PaintingCount : MonoBehaviour
{
    [SerializeField] private Image _imageTutorial;

    private int _carPainting;
    private bool _isTutorialComleted = false;

    public event UnityAction CarArrivedToPainting;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CarCleaner car))
        {
            _carPainting++;

            if (_carPainting >= 1 && !_isTutorialComleted)
            {
                _imageTutorial.gameObject.SetActive(true);

                CarArrivedToPainting?.Invoke();

                StartCoroutine(ShowOnTimer());

                _isTutorialComleted = true;
            }
        }
    }

    private IEnumerator ShowOnTimer()
    {
        float timeLeft = 3.5f;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        _imageTutorial.gameObject.SetActive(false);
    }
}

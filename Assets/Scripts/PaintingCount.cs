using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PaintingCount : MonoBehaviour
{
    [SerializeField] private Image _imageTutorial;

    private int _carPainting;
    private bool _isTutorialComleted = false;

    public event UnityAction CarExitFromPainting;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CarCleaner car))
        {
            _carPainting++;

            if (_carPainting >= 2 && !_isTutorialComleted)
            {
                _imageTutorial.gameObject.SetActive(true);

                CarExitFromPainting?.Invoke();

                StartCoroutine(ShowOnTimer());

                _isTutorialComleted = true;
            }
        }
    }

    private IEnumerator ShowOnTimer()
    {
        float timeLeft = 4f;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        _imageTutorial.gameObject.SetActive(false);
    }
}

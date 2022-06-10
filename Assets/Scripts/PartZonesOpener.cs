using UnityEngine;

public class PartZonesOpener : MonoBehaviour
{
    [SerializeField] private GameObject[] _moneyDropAreas;
    [SerializeField] private int[] _stepsToOpenZone;
    [SerializeField] private Stock _relatedStack;

    private int _currentStep = 0;

    private void OnEnable()
    {
        _relatedStack.TakenItem += OnUnblockNextZone;
    }

    private void OnDisable()
    {
        _relatedStack.TakenItem += OnUnblockNextZone;
    }

    private void OnUnblockNextZone()
    {
        if(_stepsToOpenZone[_currentStep] == _relatedStack.Lifespan)
        {
            _moneyDropAreas[_currentStep].SetActive(true);
            _currentStep += 1;
        }
    }
}

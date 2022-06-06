using UnityEngine;

public class Resources : MonoBehaviour
{
    [SerializeField] private GameObject[] _sceneServiceZones;
    [SerializeField] private GameObject[] _moneyDropZones;

    public void ActivateServiceZones(int count)
    {
        for (int i = 0; i < count - 1; i++)
        {
            _sceneServiceZones[i].SetActive(true);
        }
    }

    public void DeactivateMoneyDropZones(int count)
    {
        for (int i = 0; i < count - 1; i++)
        {
            _moneyDropZones[i].SetActive(false);
        }
    }

    public int GetActiveServiceZones()
    {
        int activeZones = 0;
        for (int i = 0; i < _sceneServiceZones.Length; i++)
        {
            if (_sceneServiceZones[i].activeSelf)
            {
                activeZones++;
            }
        }

        return activeZones;
    }

    public int GetInactiveDropZones()
    {
        int inactiveZones = 0;
        for (int i = 0; i < _sceneServiceZones.Length; i++)
        {
            if (_sceneServiceZones[i].activeSelf == false)
            {
                inactiveZones++;
            }
        }

        return inactiveZones;
    }
}

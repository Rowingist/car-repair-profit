using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class StartLevel_Temp : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _promoCamera; // в отдельный класс
    [SerializeField] private Button _button;

    public void StartLevel()
    {
        _promoCamera.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}

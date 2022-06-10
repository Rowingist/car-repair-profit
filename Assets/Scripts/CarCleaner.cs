using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCleaner : MonoBehaviour
{
    [SerializeField] private MeshRenderer _cleanMesh;
    [SerializeField] private MeshRenderer _dryMesh;

    public void SetDryColor()
    {
        _dryMesh.gameObject.SetActive(true);
        _cleanMesh.gameObject.SetActive(false);

    }

    public void ChangeCleanMesh()
    {
        _dryMesh.gameObject.SetActive(false);
        _cleanMesh.gameObject.SetActive(true);
    }
}

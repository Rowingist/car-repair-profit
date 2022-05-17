using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Whell : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private float _elapsedTime = 0;
    private float _delayAfterSpawn = 1f;

    private void OnEnable()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _boxCollider.enabled = false;
    }
    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _delayAfterSpawn)
            _boxCollider.enabled = true;
    }
}

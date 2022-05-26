using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Driver : MonoBehaviour
{
    [SerializeField] private TMP_Text _textInWidget;
    [SerializeField] private GameObject _panelWidget;

    private Upload _upload;

    private void Start()
    {
        _upload.CountPartChanged += OnPlayerSalled;
        _textInWidget.text = _upload._neeedToFix.ToString();
    }

    private void OnDisable()
    {
        _upload.CountPartChanged -= OnPlayerSalled;
    }

    private void OnPlayerSalled(int currenSell, int needToFix)
    {
        int show = needToFix - currenSell;

        _textInWidget.text = show.ToString();
    }

    public void Init(Upload upload)
    {
        _upload = upload;
    }
}

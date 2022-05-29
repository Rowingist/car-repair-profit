using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Payed += OnChangeView;
        _player.GotCash += OnChangeView;
    }

    private void Start()
    {
        OnChangeView();
    }

    private void OnDisable()
    {
        _player.Payed -= OnChangeView;
        _player.GotCash -= OnChangeView;
    }

    private void OnChangeView()
    {
        _walletText.text = _player.GetWalletCash().ToString();
    }
}

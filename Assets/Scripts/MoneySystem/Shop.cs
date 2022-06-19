using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopView _shopView;
    [SerializeField] private MMFeedbacks _scaleFeedback;
    [SerializeField] private MMFeedbacks _unscaleFeedback;
    [SerializeField] private MovingTutorial _movingTutorial;

    private bool _isShopTutorialComlete = false;

    public event UnityAction PlayerExitFromShop;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _shopView.gameObject.SetActive(true);
            _scaleFeedback?.PlayFeedbacks();
            player.EnterShop();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _shopView.gameObject.SetActive(false);
            _unscaleFeedback?.PlayFeedbacks();
            player.ExitShop(); 

            if (!_isShopTutorialComlete && _movingTutorial._isWhellTutorialComplete)
            {
                PlayerExitFromShop?.Invoke();
                _isShopTutorialComlete = true;
            }
        }
    }
}

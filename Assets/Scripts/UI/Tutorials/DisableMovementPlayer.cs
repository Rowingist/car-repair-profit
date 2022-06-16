using System.Collections;
using UnityEngine;

public class DisableMovementPlayer : MonoBehaviour
{
    [SerializeField] private FirstMoneyTutorial _firstMoneyTutorial;
    [SerializeField] private MovingTutorial _movingTutorial;
    [SerializeField] private UnlockedArea _unlockedArea;
    [SerializeField] private GameObject _joistickHandler;
    [SerializeField] private FloatingJoystick _floatingJoystick;

    private void OnEnable()
    {
        _firstMoneyTutorial.FirstTutorialMoneyZoneLeft += OnDIsableMovementPlayer;
        _movingTutorial.FirstTutorialShoewed += OnDIsableMovementPlayer;
        _movingTutorial.WhellTutorialShowed += OnDIsableMovementPlayer;
        _movingTutorial.CarDoorTutorialShowed += OnDIsableMovementPlayer;
        _movingTutorial.ShopTutorialShowed += OnDIsableMovementPlayer;
        _movingTutorial.RackTutorialShowed += OnDIsableMovementPlayer;
        _movingTutorial.RepairTutorialShowed += OnDIsableMovementPlayer;
        _movingTutorial.PaintTutorialShowed += OnDIsableMovementPlayer;
        _movingTutorial.PaintBallonTutorialShowed += OnDIsableMovementPlayer;
    }

    private void OnDisable()
    {
        _firstMoneyTutorial.FirstTutorialMoneyZoneLeft -= OnDIsableMovementPlayer;
        _movingTutorial.FirstTutorialShoewed -= OnDIsableMovementPlayer;
        _movingTutorial.WhellTutorialShowed -= OnDIsableMovementPlayer;
        _movingTutorial.CarDoorTutorialShowed -= OnDIsableMovementPlayer;
        _movingTutorial.ShopTutorialShowed -= OnDIsableMovementPlayer;
        _movingTutorial.RackTutorialShowed -= OnDIsableMovementPlayer;
        _movingTutorial.RepairTutorialShowed -= OnDIsableMovementPlayer;
        _movingTutorial.PaintTutorialShowed -= OnDIsableMovementPlayer;
        _movingTutorial.PaintBallonTutorialShowed -= OnDIsableMovementPlayer;
    }

    private void OnDIsableMovementPlayer()
    {
        _floatingJoystick.ClearInputValue();

        _joistickHandler.gameObject.SetActive(false);

        StartCoroutine(EnableMovementOnTimer());
    }

    private IEnumerator EnableMovementOnTimer()
    {

        float timeToChangeCamera = 4f;

        float timeLeft = timeToChangeCamera;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        _joistickHandler.gameObject.SetActive(true);
    }
}

using MoreMountains.Feedbacks;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private MMFeedbackPosition _moveFeedback;

    public void SetTarget(Item item)
    {
        _moveFeedback.AnimatePositionTarget = item.gameObject;
    }

    public void SetInitialPosition(Vector3 initialPosistion)
    {
        _moveFeedback.InitialPosition = initialPosistion;
    }

    public void SetDestination(Vector3 destinationPosition)
    {
        _moveFeedback.DestinationPosition = destinationPosition;
    }
}

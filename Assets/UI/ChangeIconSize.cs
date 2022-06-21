using UnityEngine;
using DG.Tweening;

public class ChangeIconSize : MonoBehaviour
{
    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        transform.DOScale(2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}

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
        transform.DOScale(1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

}

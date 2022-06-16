using UnityEngine;
using DG.Tweening;

public class GateOpen : MonoBehaviour
{
    public void ShutterUp()
    {
        transform.DOScaleZ(0.1f, 15f);
    }
}

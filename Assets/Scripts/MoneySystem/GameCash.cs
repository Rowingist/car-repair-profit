using System.Collections;
using UnityEngine;

public class GameCash : Item
{
    [SerializeField] private int _value;
    [SerializeField] private float _deactivateAfrterCollectTime = 0.2f;
    
    public int Value => _value;

    [ContextMenu("Collect")]
    public void Collect()
    {
        StartCoroutine(Deactivate(_deactivateAfrterCollectTime));
    }

    private IEnumerator Deactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
            
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class GameCash : Item
{
    [SerializeField] private int _value;
    
    public int Value => _value;

    [ContextMenu("Collect")]
    public void Collect()
    {
        gameObject.SetActive(false);
    }
}

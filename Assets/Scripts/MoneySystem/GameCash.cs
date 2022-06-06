using UnityEngine;

public class GameCash : Item
{
    [SerializeField] private int _value;
    
    public int Value => _value;
}

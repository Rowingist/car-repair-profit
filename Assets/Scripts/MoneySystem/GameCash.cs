using UnityEngine;

public class GameCash : MonoBehaviour
{
    [SerializeField] private int _value;
    
    public int Value => _value;
}

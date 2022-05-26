using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;

    public ItemType ItemType => _itemType;
}

public enum ItemType
{
    Wheel,
    Engine,
    Oil,
    Paint
}

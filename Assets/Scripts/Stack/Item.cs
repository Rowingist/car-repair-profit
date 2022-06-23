using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private ItemMover _itemMover;
    
    public ItemType ItemType => _itemType;
    public ItemMover ItemMover => _itemMover;

}

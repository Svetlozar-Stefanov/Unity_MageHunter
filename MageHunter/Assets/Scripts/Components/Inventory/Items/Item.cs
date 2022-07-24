using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Items/Item")]
public class Item : BaseItem
{
    private void Awake()
    {
        type = ItemType.Item;
    }
}

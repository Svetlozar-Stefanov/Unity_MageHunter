using UnityEngine;

[CreateAssetMenu(fileName = "New MiscItem", menuName = "Game/Items/MiscItem")]
public class MiscItem : BaseItem
{
    private void Awake()
    {
        type = ItemType.MiscItem;
    }
}

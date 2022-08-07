using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventoryComponent : MonoBehaviour
{
    [SerializeField] private Inventory dropSet;

    [SerializeField] private ItemComponent toDropItemPrefab;

    private List<InventorySlot> dropItems = new List<InventorySlot>();

    private void Start()
    {
        for (int i = 0; i < dropSet.Capacity; i++)
        {
            if (dropSet.Items[i].Item != null)
            {
                InventorySlot toAdd = new InventorySlot(dropSet.Items[i].Item, dropSet.Items[i].Amount);
                dropItems.Add(toAdd);
            }
        }
    }

    public void DropRandomLoot()
    {
        int itemIndex = Random.Range(0, dropItems.Count);
        InventorySlot item = dropItems[itemIndex];
        int amount = Random.Range(1, item.Amount + 1);
        item.RemoveAmount(item.Amount - amount);

        DropLoot(item);
    }

    public void DropLoot(InventorySlot toDrop)
    {
        if (toDrop == null || toDropItemPrefab == null)
        {
            return;
        }

        toDropItemPrefab.Item = toDrop.Item;
        toDropItemPrefab.Amount = toDrop.Amount;

        Instantiate(toDropItemPrefab, transform.position, transform.rotation);
    }
}

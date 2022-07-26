using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Game/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();

    public List<InventorySlot> Items { get => items; } 
    
    public void AddItem(BaseItem item, int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        InventorySlot slot = GetItemSlot(item);
        if (slot == null)
        {
            items.Add(new InventorySlot(item, amount));
            return;
        }

        slot.AddAmount(amount);
    }

    public bool Contains(BaseItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Item == item)
            {
                return true;
            }
        }
        return false;
    }

    public InventorySlot GetItemSlot(BaseItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Item == item)
            {
                return items[i];
            }
        }
        return null;
    }

    public void Clear()
    {
        items.Clear();
    }
}

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private BaseItem item;
    [SerializeField] private int amount;

    public BaseItem Item { get => item; set => item = value; }
    public int Amount { get => amount; set => amount = value; }

    public InventorySlot(BaseItem item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        this.amount += value;
    }
}

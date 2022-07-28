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

    public void RemoveItem(int index, int amount)
    {
        if (index < 0 || index >= items.Count)
        {
            return;
        }

        InventorySlot slot = items[index];
        slot.RemoveAmount(amount);
        if (slot.Amount <= 0)
        {
            items.RemoveAt(index);
        }
    }

    public InventorySlot DropItem(int index, int amount)
    {
        if (index < 0 || index >= items.Count)
        {
            return null;
        }

        InventorySlot slot = items[index];

        if (amount > slot.Amount)
        {
            return null;
        }
        
        InventorySlot itemToDrop = new InventorySlot(slot.Item, slot.Amount);
        //RemoveItem(index, amount);

        return itemToDrop;
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

    public BaseItem Item { get => item; }
    public int Amount { get => amount; }

    public InventorySlot(BaseItem item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        this.amount += value;
    }

    public void RemoveAmount(int value)
    {
        if (value > amount)
        {
            return;
        }

        this.amount -= value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Game/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private int capacity;
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();

    private int index = 0;

    public List<InventorySlot> Items { get => items; }
    public int Capacity { get => capacity; }

    public int Length { get => index; }

    private void Awake()
    {
        for (int i = 0; i < capacity; i++)
        {
            items.Add(null);
        }
    }

    public void AddItem(BaseItem item, int amount)
    {
        if (amount <= 0 || index >= capacity)
        {
            return;
        }

        InventorySlot slot = GetItemSlot(item);
        if (slot == null)
        {
            items[index++] = new InventorySlot(item, amount);
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

    public void Swap(int i1, int i2)
    {
        if (i1 >= items.Count || i2 >= items.Count)
        {
            return;
        }

        var temp = items[i1];
        items[i1] = items[i2];
        items[i2] = temp;
    }

    //To be moved maybe
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

    //To be moved maybe
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

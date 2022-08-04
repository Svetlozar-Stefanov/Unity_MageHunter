using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Game/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();

    private int index = 0;

    public List<InventorySlot> Items { get => items; }
    public int Capacity { get => items.Count; }

    public int Size { get => index; }

    public void SetUp()
    {
        index = 0;
        for (int i = 0; i < Capacity; i++)
        {
            if (items[i].Item != null)
            {
                index++;
            }
        }
    }

    public void AddItem(BaseItem item, int amount)
    {
        if (amount <= 0 || index >= Capacity)
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

    public InventorySlot GetItemSlot(BaseItem item)
    {
        for (int i = 0; i < Capacity; i++)
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
        if (i1 >= Capacity || i2 >= Capacity)
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
        if (index < 0 || index >= Capacity)
        {
            return;
        }

        InventorySlot slot = items[index];
        slot.RemoveAmount(amount);
        if (slot.Amount <= 0)
        {
            items[index] = null;
        }
    }

    //To be moved maybe
    public InventorySlot DropItem(int index, int amount)
    {
        if (index < 0 || index >= Capacity)
        {
            return null;
        }

        InventorySlot slot = items[index];

        if (amount > slot.Amount)
        {
            return null;
        }

        InventorySlot itemToDrop = new InventorySlot(slot.Item, amount);

        return itemToDrop;
    }

    public void Clear()
    {
        for (int i = 0; i < Capacity; i++)
        {
            items[i] = null;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Game/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] protected List<InventorySlot> items = new List<InventorySlot>();
     public UnityAction<InventorySlot> onItemDrop;

    protected int index = 0;
    protected int size = 0;

    public List<InventorySlot> Items { get => items; }
    public int Capacity { get => items.Count; }

    public int Size { get => index; }

    public void SetUp()
    {
        index = 0;
        size = 0;
        for (int i = 0; i < Capacity; i++)
        {
            if (items[i].Item != null)
            {
                size++;
            }
        }
    }

    public virtual bool AddItem(BaseItem item, int amount)
    {
        if (amount <= 0 || index >= Capacity)
        {
            return false;
        }

        InventorySlot slot = GetItemSlot(item);
        if (slot == null)
        {
            while (index < Capacity && items[index].Item != null)
            {
                index++;
            }
            items[index] = new InventorySlot(item, amount);
            index = 0;
            return true;
        }

        slot.AddAmount(amount);
        return true;
    }

    public virtual bool AddItemAt(BaseItem item, int amount, int i)
    {
        if (amount <= 0 || i >= Capacity || (items[i].Item != null && items[i].Item != item))
        {
            return false;
        }

        InventorySlot slot = items[i];
        if (slot.Item == null)
        {
            items[i] = new InventorySlot(item, amount);
            return true;
        }
        slot.AddAmount(amount);
        return true;
    }

    public virtual InventorySlot GetItemSlot(BaseItem item)
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

    public virtual void Swap(int i1, int i2)
    {
        if (i1 >= Capacity || i2 >= Capacity)
        {
            return;
        }

        var temp = items[i1];
        items[i1] = items[i2];
        items[i2] = temp;
    }

    public virtual bool DropItem(int index, int amount)
    {
        if (index < 0 || index >= Capacity)
        {
            return false;
        }

        InventorySlot slot = items[index];

        if (amount > slot.Amount)
        {
            return false;
        }
        InventorySlot itemToDrop = new InventorySlot(slot.Item, amount);
        slot.RemoveAmount(amount);
        if (slot.Amount <= 0)
        {
            slot.Reset();
        }

        onItemDrop.Invoke(itemToDrop);
        return true;
    }

    public virtual bool RemoveItem(int index, int amount)
    {
        if (index < 0 || index >= Capacity)
        {
            return false;
        }

        InventorySlot slot = items[index];

        if (amount > slot.Amount)
        {
            return false;
        }

        slot.RemoveAmount(amount);
        if (slot.Amount <= 0)
        {
            slot.Reset();
        }

        return true;
    }

    public virtual void Clear()
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

    public void Reset()
    {
        item = null;
        amount = 0;
    }
}

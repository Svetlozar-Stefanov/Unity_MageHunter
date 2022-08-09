using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpellBook", menuName = "Game/SpellBook")]
public class SpellBook : Inventory
{
    public override bool AddItem(BaseItem item, int amount)
    {
        if (amount <= 0 || index >= Capacity || item.type != ItemType.SpellScroll)
        {
            return false;
        }

        InventorySlot slot = GetItemSlot(item);
        if (slot != null)
        {
            return false;
        }

        while (index < Capacity && items[index].Item != null)
        {
            index++;
        }
        items[index] = new InventorySlot(item, amount);

        return true;
    }

    public override bool AddItemAt(BaseItem item, int amount, int i)
    {
        if (amount <= 0 || i >= Capacity || (items[i].Item != null))
        {
            return false;
        }

        items[i] = new InventorySlot(item, amount);
        return true;
    }

    public SpellScroll GetSpell(int i)
    {
        if (i >= Capacity || items[i].Item == null)
        {
            return null;
        }

        return (SpellScroll)items[i].Item;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUIDisplay : MonoBehaviour
{
    [SerializeField] private GameObject itemUI;

    public void SetUp(InventorySlot item)
    {
        var obj = Instantiate(itemUI, transform);
        obj.GetComponent<ItemUIDisplay>().SetUp(item.Item.icon, item.Amount.ToString("n0"));
    }
}

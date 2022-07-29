using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    //[SerializeField] private int X_START;
    //[SerializeField] private int Y_START;
    //[SerializeField] private int X_ITEM_MARGIN;
    //[SerializeField] private int Y_ITEM_MARGIN;
    //[SerializeField] private int COLUMN_COUNT;

    [SerializeField] private GameObject backgroundPrefab;

    private Dictionary<InventorySlot, GameObject> inventoryDisplay = new Dictionary<InventorySlot, GameObject>();
    private List<GameObject> UISlots = new List<GameObject>();
     
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.Capacity; i++)
        {
            UISlots.Add(Instantiate(backgroundPrefab, transform));
        }

        for (int i = 0; i < inventory.Items.Count; i++)
        {
            SetupUIPrefab(inventory.Items[i], i);
        }
    }

    private void UpdateDisplay()
    {
        //for (int i = 0; i < inventory.Items.Count; i++)
        //{
        //    if (inventoryDisplay.ContainsKey(inventory.Items[i]))
        //    {
        //        int amount = inventory.Items[i].Amount;

        //        inventoryDisplay[inventory.Items[i]].GetComponent<ItemUIDisplay>().SetText(amount.ToString("n0"));
        //    }
        //    else
        //    {
        //        SetupUIPrefab(inventory.Items[i], i);
        //    }
        //}
        //if (inventoryDisplay.Keys.Count > inventory.Items.Count)
        //{
        //    List<InventorySlot> toDel = new List<InventorySlot>();
        //    foreach (var key in inventoryDisplay.Keys)
        //    {
        //        if (!inventory.Items.Contains(key))
        //        {
        //            toDel.Add(key);
        //        }
        //    }
        //    for (int i = 0; i < toDel.Count; i++)
        //    {
        //       Destroy(inventoryDisplay.GetValueOrDefault(toDel[i]).gameObject);
        //        inventoryDisplay.Remove(toDel[i]);
        //    }
        //}
    }

    private void SetupUIPrefab(InventorySlot item, int index)
    {
        UISlots[index].gameObject.GetComponent<SlotUIDisplay>().SetUp(item);

        inventoryDisplay.Add(item, UISlots[index]);
    }

    //private Vector3 GetPosition(int i)
    //{
    //    return new Vector3(X_START + X_ITEM_MARGIN * (i % COLUMN_COUNT), Y_START + (-Y_ITEM_MARGIN * (i/COLUMN_COUNT)), 0);
    //}
}

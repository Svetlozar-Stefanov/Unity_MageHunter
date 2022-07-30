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

    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private ItemUIDisplay itemUIPrefab;

    [SerializeField] private DescriptionUIDisplay descriptionUIDisplay;

    //private Dictionary<InventorySlot, GameObject> inventoryDisplay = new Dictionary<InventorySlot, GameObject>();
    private List<ItemUIDisplay> itemUIInstances = new List<ItemUIDisplay>();
     
    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.Capacity; i++)
        {
            ItemUIDisplay uiItem = Instantiate(itemUIPrefab, contentPanel.transform);
            uiItem.transform.SetParent(contentPanel); 
            itemUIInstances.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleItemBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMbtClicked += HandleShowItemActions;
        }

        for (int i = 0; i < inventory.Items.Count; i++)
        {
            SetupUIPrefab(inventory.Items[i], i);
        }
    }

    private void HandleShowItemActions(ItemUIDisplay obj)
    {
        
    }

    private void HandleEndDrag(ItemUIDisplay obj)
    {
        
    }

    private void HandleSwap(ItemUIDisplay obj)
    {
        
    }

    private void HandleItemBeginDrag(ItemUIDisplay obj)
    {
        
    }

    private void HandleItemSelection(ItemUIDisplay obj)
    {
        obj.Select();
        descriptionUIDisplay.Set(inventory.Items[0].Item);
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
        itemUIInstances[index].gameObject.GetComponent<ItemUIDisplay>().SetUp(item.Item.icon, item.Amount.ToString("n0"));

        //inventoryDisplay.Add(item, itemUIInstances[index]);
    }

    //private Vector3 GetPosition(int i)
    //{
    //    return new Vector3(X_START + X_ITEM_MARGIN * (i % COLUMN_COUNT), Y_START + (-Y_ITEM_MARGIN * (i/COLUMN_COUNT)), 0);
    //}
}

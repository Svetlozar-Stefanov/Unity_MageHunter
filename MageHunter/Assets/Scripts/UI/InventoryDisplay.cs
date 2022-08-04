using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventory;

    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private ItemUIDisplay itemUIPrefab;

    [SerializeField] private DescriptionUIDisplay descriptionUIDisplay;
    [SerializeField] private DragableUIDisplay dragableUI;

    private List<ItemUIDisplay> itemUIInstances = new List<ItemUIDisplay>();
    private int currentlyDraggedItemIndex = -1;

    void Start()
    {
        inputReader.openInventoryEvent += Show;
        inputReader.closeInventoryEvent += Hide;

        CreateDisplay();
        descriptionUIDisplay.ResetDescription();
        Hide();
        dragableUI.Toggle(false);
    }
    void Update()
    {
        UpdateDisplay();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        descriptionUIDisplay.ResetDescription();
        dragableUI.Toggle(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        dragableUI.Toggle(false);
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

        for (int i = 0; i < inventory.Capacity; i++)
        {
            SetupUIPrefab(inventory.Items[i], i);
        }
    }

    private void HandleShowItemActions(ItemUIDisplay obj)
    {

    }

    private void HandleEndDrag(ItemUIDisplay obj)
    {
        dragableUI.Toggle(false);
    }

    private void HandleSwap(ItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1)
        {
            dragableUI.Toggle(false);
            currentlyDraggedItemIndex = -1;
            return;
        }

        InventorySlot dragged = inventory.Items[currentlyDraggedItemIndex];
        InventorySlot toSwap = inventory.Items[index];

        SetupUIPrefab(dragged, index);
        SetupUIPrefab(toSwap, currentlyDraggedItemIndex);

        inventory.Swap(index, currentlyDraggedItemIndex);

        dragableUI.Toggle(false);
    }

    private void HandleItemBeginDrag(ItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;

        dragableUI.Toggle(true);
        dragableUI.SetData(obj.itemGraphic.sprite, obj.amountText.text);
    }

    private void HandleItemSelection(ItemUIDisplay obj)
    {
        obj.Select();
        descriptionUIDisplay.Set(inventory.Items[0].Item.icon, inventory.Items[0].Item.name, inventory.Items[0].Item.description);
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
        if (item.Item != null)
        {
            itemUIInstances[index].SetUp(item.Item.icon, item.Amount);
        }
        else
        {
            itemUIInstances[index].ResetData();
        }
    }
}

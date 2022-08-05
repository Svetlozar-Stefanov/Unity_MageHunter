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
        Hide();
    }
    void Update()
    {
        UpdateDisplay();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetDragableUI();
        ResetSelection();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDragableUI();
        ResetSelection();
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
            uiItem.OnRightMbtClicked += HandleRightClick;
        }

        for (int i = 0; i < inventory.Capacity; i++)
        {
            SetupUIPrefab(inventory.Items[i], i);
        }
    }

    private void HandleRightClick(ItemUIDisplay obj)
    {
        if (currentlyDraggedItemIndex != -1)
        {
            InventorySlot slot = inventory.Items[currentlyDraggedItemIndex];
            if (!inventory.AddItemAt(slot.Item, 1, itemUIInstances.IndexOf(obj)))
            {
                return;
            }
            slot.RemoveAmount(1);
            InventorySlot newSlot = inventory.Items[itemUIInstances.IndexOf(obj)];
            obj.SetUp(newSlot.Item.icon, newSlot.Amount);
            if (dragableUI.enabled)
            {
                dragableUI.SetData(slot.Item.icon, slot.Amount);
            }
            if (slot.Amount <= 0)
            {
                slot.Reset();
                itemUIInstances[currentlyDraggedItemIndex].ResetData();
                ResetDragableUI();
                return;
            }
        }
    }

    private void HandleItemBeginDrag(ItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;

        InventorySlot slot = inventory.Items[index];

        dragableUI.Toggle(true);
        dragableUI.SetData(slot.Item.icon, slot.Amount);
    }

    private void HandleEndDrag(ItemUIDisplay obj)
    {
        ResetDragableUI();
        ResetSelection();
    }

    private void HandleSwap(ItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1 || currentlyDraggedItemIndex == -1)
        {
            return;
        }

        InventorySlot dragged = inventory.Items[currentlyDraggedItemIndex];
        InventorySlot toSwap = inventory.Items[index];
        if (dragged == toSwap)
        {
            return;
        }

        if (dragged.Item == toSwap.Item)
        {
            toSwap.AddAmount(dragged.Amount);
            dragged.Reset();

            SetupUIPrefab(dragged, currentlyDraggedItemIndex);
            SetupUIPrefab(toSwap, index);
        }
        else
        {
            inventory.Swap(index, currentlyDraggedItemIndex);
            SetupUIPrefab(dragged, index);
            SetupUIPrefab(toSwap, currentlyDraggedItemIndex);
        }

        ResetDragableUI();
        ResetSelection();
    }

    private void HandleItemSelection(ItemUIDisplay obj)
    {
        ResetSelection();
        if (obj.Empty)
        {
            return;
        }

        obj.Select();
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        InventorySlot slot = inventory.Items[index];
        descriptionUIDisplay.Set(slot.Item.icon, slot.Item.name, slot.Item.description);
    }

    private void ResetSelection()
    {
        descriptionUIDisplay.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (var item in itemUIInstances)
        {
            item.Deselect();
        }
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Capacity; i++)
        {
            if (inventory.Items[i].Item != null)
            {
                if (itemUIInstances[i].Empty)
                {
                    SetupUIPrefab(inventory.Items[i], i);
                }
                else
                {
                    itemUIInstances[i].SetAmount(inventory.Items[i].Amount);
                }
            }
        }
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

    private void ResetDragableUI()
    {
        dragableUI.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }
}

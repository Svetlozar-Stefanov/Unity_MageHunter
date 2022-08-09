using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDisplay : BasePanelUIDisplay
{
    [SerializeField] private DescriptionUIDisplay descriptionUIDisplay;

    [SerializeField] private GameObject dropMenu;

    private bool isInDropMenu = false;
    private int itemToDropIndex = -1;

    void Start()
    {
        inputReader.openInventoryEvent += Show;
        inputReader.closeInventoryEvent += Hide;

        CreateDisplay();
        dropMenu.SetActive(false);
        Hide();
    }
    void Update()
    {
        UpdateDisplay();
    }

    protected override void CreateDisplay()
    {
        for (int i = 0; i < inventory.Capacity; i++)
        {
            ItemUIDisplay uiItem = Instantiate((ItemUIDisplay)itemUIPrefab, contentPanel.transform);
            uiItem.transform.SetParent(contentPanel);
            itemUIInstances.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleItemBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMbtClicked += HandleRightClick;
            uiItem.OnItemUsed += HandleItemUsed;
            uiItem.OnItemDroped += HandleItemDropped;
        }

        for (int i = 0; i < inventory.Capacity; i++)
        {
            SetupUIPrefab(inventory.Items[i], i);
        }
    }

    public void HandleRightClick(ItemUIDisplay obj)
    {
        if (isInDropMenu)
        {
            return;
        }
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
        else if(!obj.Empty)
        {
            if (!obj.IsInActionMenu)
            {
                obj.OpenActionMenu();
            }
            else if(obj.IsInActionMenu)
            {
                obj.CloseActionMenu();
            }
        }
    }

    protected override void HandleItemBeginDrag(BaseItemUIDisplay obj)
    {
        if (isInDropMenu)
        {
            return;
        }

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

    protected override void HandleSwap(BaseItemUIDisplay obj)
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

    protected override void HandleItemSelection(BaseItemUIDisplay obj)
    {
        if (isInDropMenu)
        {
            return;
        }

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

    public void HandleItemUsed(ItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        BaseItem item = inventory.Items[index].Item;
        if (item != null)
        {
            item.Use();
        }
    }

    public void HandleItemDropped(ItemUIDisplay obj)
    {
        obj.CloseActionMenu();
        dropMenu.SetActive(true);
        int index = itemUIInstances.IndexOf(obj);
        itemToDropIndex = index;
        InventorySlot slot = inventory.Items[index];
        dropMenu.GetComponent<DropMenuUIDisplay>().SetUp(slot.Amount);
        isInDropMenu = true;
        inputReader.DisableAllInput();
    }

    public void HandleOnDropCancel()
    {
        dropMenu.SetActive(false);
        isInDropMenu = false;
        itemToDropIndex = -1;
        inputReader.EnableInGameMenusInput();
    }

    public void HandleOnDropAccept()
    {
        int amount = dropMenu.GetComponent<DropMenuUIDisplay>().GetValue();
        if(inventory.DropItem(itemToDropIndex, amount))
        {
            if (inventory.Items[itemToDropIndex].Item == null)
            {
                itemUIInstances[itemToDropIndex].ResetData();
            }
            else
            {
                ((ItemUIDisplay)itemUIInstances[itemToDropIndex]).SetAmount(inventory.Items[itemToDropIndex].Amount);
            }
        }

        HandleOnDropCancel();
    }

    protected override void UpdateDisplay()
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
                    ((ItemUIDisplay)itemUIInstances[i]).SetAmount(inventory.Items[i].Amount);
                }
            }
        }
    }

    protected override void SetupUIPrefab(InventorySlot item, int index)
    {
        if (item.Item != null)
        {
            ((ItemUIDisplay)itemUIInstances[index]).SetUp(item.Item.icon, item.Amount);
        }
        else
        {
            itemUIInstances[index].ResetData();
        }
    }
}

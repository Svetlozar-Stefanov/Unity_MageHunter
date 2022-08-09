using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanelUIDisplay : MonoBehaviour
{
    [SerializeField] protected InputReader inputReader;
    [SerializeField] protected Inventory inventory;

    [SerializeField] protected RectTransform contentPanel;
    [SerializeField] protected BaseItemUIDisplay itemUIPrefab;

    [SerializeField] protected DragableUIDisplay dragableUI;

    protected List<BaseItemUIDisplay> itemUIInstances = new List<BaseItemUIDisplay>();
    protected int currentlyDraggedItemIndex = -1;

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

    public virtual void Show()
    {
        gameObject.SetActive(true);
        ResetDragableUI();
        ResetSelection();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        ResetDragableUI();
        ResetSelection();
    }

    protected virtual void CreateDisplay()
    {
        for (int i = 0; i < inventory.Capacity; i++)
        {
            BaseItemUIDisplay uiItem = Instantiate(itemUIPrefab, contentPanel.transform);
            uiItem.transform.SetParent(contentPanel);
            itemUIInstances.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleItemBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
        }

        for (int i = 0; i < inventory.Capacity; i++)
        {
            SetupUIPrefab(inventory.Items[i], i);
        }
    }

    protected virtual void HandleItemBeginDrag(BaseItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;

        InventorySlot slot = inventory.Items[index];

        dragableUI.Toggle(true);
        dragableUI.SetData(slot.Item.icon);
    }

    protected virtual void HandleEndDrag(BaseItemUIDisplay obj)
    {
        ResetDragableUI();
        ResetSelection();
    }

    protected virtual void HandleSwap(BaseItemUIDisplay obj)
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

        inventory.Swap(index, currentlyDraggedItemIndex);
        SetupUIPrefab(dragged, index);
        SetupUIPrefab(toSwap, currentlyDraggedItemIndex);

        ResetDragableUI();
        ResetSelection();
    }

    protected virtual void HandleItemSelection(BaseItemUIDisplay obj)
    {
        ResetSelection();
        if (obj.Empty)
        {
            return;
        }

        obj.Select();
    }

    protected virtual void ResetSelection()
    {
        DeselectAllItems();
    }

    protected void DeselectAllItems()
    {
        foreach (var item in itemUIInstances)
        {
            item.Deselect();
        }
    }

    protected virtual void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Capacity; i++)
        {
            if (inventory.Items[i].Item != null)
            {
                if (itemUIInstances[i].Empty)
                {
                    SetupUIPrefab(inventory.Items[i], i);
                }
            }
        }
    }

    protected virtual void SetupUIPrefab(InventorySlot item, int index)
    {
        if (item.Item != null)
        {
            itemUIInstances[index].SetUp(item.Item.icon);
        }
        else
        {
            itemUIInstances[index].ResetData();
        }
    }

    protected void ResetDragableUI()
    {
        dragableUI.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }
}

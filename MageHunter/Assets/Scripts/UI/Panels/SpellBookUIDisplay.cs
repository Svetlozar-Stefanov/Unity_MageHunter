using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookUIDisplay : BasePanelUIDisplay
{
    [SerializeField] protected FightingComponent fightingComponent;

    [SerializeField] protected RectTransform lightContent;
    [SerializeField] protected RectTransform heavyContent;

    void Start()
    {
        inputReader.openSpellBookEvent += Show;
        inputReader.closeSpellBookEvent += Hide;

        CreateDisplay();
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

        for (int i = 0; i < 3; i++)
        {
            BaseItemUIDisplay uiItem = Instantiate(itemUIPrefab, contentPanel.transform);
            ((SpellItemUIDisplay)uiItem).Type = SlotType.LightSpell;
            uiItem.transform.SetParent(lightContent);
            itemUIInstances.Add(uiItem);
        }
        for (int i = 0; i < 3; i++)
        {
            BaseItemUIDisplay uiItem = Instantiate(itemUIPrefab, contentPanel.transform);
            ((SpellItemUIDisplay)uiItem).Type = SlotType.HeavySpell;
            uiItem.transform.SetParent(heavyContent);
            itemUIInstances.Add(uiItem);
        }
    }

    protected override void HandleSwap(BaseItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1 || currentlyDraggedItemIndex == -1)
        {
            return;
        }

        SpellItemUIDisplay draggedUI = (SpellItemUIDisplay)itemUIInstances[currentlyDraggedItemIndex];
        SpellItemUIDisplay toSwapUI = (SpellItemUIDisplay)itemUIInstances[index];
        if (draggedUI == null || toSwapUI == null)
        {
            return;
        }

        InventorySlot dragged = inventory.Items[currentlyDraggedItemIndex];
        InventorySlot toSwap = inventory.Items[index];
        if (dragged == toSwap)
        {
            return;
        }

        if (draggedUI.Type == SlotType.Default && toSwapUI.Type == SlotType.Default)
        {
            inventory.Swap(index, currentlyDraggedItemIndex);
            SetupUIPrefab(dragged, index);
            SetupUIPrefab(toSwap, currentlyDraggedItemIndex);
        }
        else if(draggedUI.Type == SlotType.Default && toSwapUI.Type != SlotType.Default)
        {
            SpellScroll spell = (SpellScroll)dragged.Item;
            if (spell == null || (spell.SpellType == SpellType.Light && toSwapUI.Type != SlotType.LightSpell)
                || spell.SpellType == SpellType.Heavy && toSwapUI.Type != SlotType.HeavySpell)
            {
                return;
            }

            if (spell.SpellType == SpellType.Light)
            {
                fightingComponent.LightSpells[itemUIInstances.Count - inventory.Capacity + index].LoadSpell(spell);
                SetupUIPrefab(dragged, index);
                SetupUIPrefab(toSwap, currentlyDraggedItemIndex);
            }
            else if (spell.SpellType == SpellType.Heavy)
            {
                fightingComponent.HeavySpells[itemUIInstances.Count - inventory.Capacity + index - 3].LoadSpell(spell);
                SetupUIPrefab(dragged, index);
                SetupUIPrefab(toSwap, currentlyDraggedItemIndex);
            }
        }

        ResetDragableUI();
        ResetSelection();
    }
}

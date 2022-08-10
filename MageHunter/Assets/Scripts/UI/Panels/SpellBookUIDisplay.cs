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
            SpellItemUIDisplay uiItem = Instantiate((SpellItemUIDisplay)itemUIPrefab, contentPanel.transform);
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

        for (int i = 0; i < fightingComponent.LightSpells.Length; i++)
        {
            SpellItemUIDisplay uiItem = Instantiate((SpellItemUIDisplay)itemUIPrefab, contentPanel.transform);
            uiItem.Type = SlotType.LightSpell;
            uiItem.transform.SetParent(lightContent);
            itemUIInstances.Add(uiItem);

            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemBeginDrag += HandleItemBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;

            uiItem.ResetData();
        }
        for (int i = 0; i < fightingComponent.HeavySpells.Length; i++)
        {
            SpellItemUIDisplay uiItem = Instantiate((SpellItemUIDisplay)itemUIPrefab, contentPanel.transform);
            uiItem.Type = SlotType.HeavySpell;
            uiItem.transform.SetParent(heavyContent);
            itemUIInstances.Add(uiItem);

            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemBeginDrag += HandleItemBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;

            uiItem.ResetData();
        }
    }

    protected override void HandleSwap(BaseItemUIDisplay obj)
    {
        int index = itemUIInstances.IndexOf(obj);
        if (index == -1 || currentlyDraggedItemIndex == -1 || index == currentlyDraggedItemIndex)
        {
            return;
        }

        SpellItemUIDisplay draggedUI = (SpellItemUIDisplay)itemUIInstances[currentlyDraggedItemIndex];
        SpellItemUIDisplay toSwapUI = (SpellItemUIDisplay)itemUIInstances[index];
        if (draggedUI == null || toSwapUI == null)
        {
            return;
        }

        if (draggedUI.Type == SlotType.Default && toSwapUI.Type == SlotType.Default)
        {
            InventorySlot dragged = inventory.Items[currentlyDraggedItemIndex];
            InventorySlot toSwap = inventory.Items[index];
            if (dragged == toSwap)
            {
                return;
            }

            inventory.Swap(index, currentlyDraggedItemIndex);
            SetupUIPrefab(dragged, index);
            SetupUIPrefab(toSwap, currentlyDraggedItemIndex);
        }
        else if (draggedUI.Type == SlotType.Default && toSwapUI.Type != SlotType.Default)
        {
            InventorySlot dragged = inventory.Items[currentlyDraggedItemIndex];
            SpellScroll spell = (SpellScroll)dragged.Item;
            if (spell == null || (spell.SpellType == SpellType.Light && toSwapUI.Type != SlotType.LightSpell)
                || spell.SpellType == SpellType.Heavy && toSwapUI.Type != SlotType.HeavySpell)
            {
                return;
            }

            if (spell.SpellType == SpellType.Light)
            {
                if (fightingComponent.LoadSpellAtSlot(index - inventory.Capacity, spell))
                {
                    SetupUIPrefab(dragged, index);
                }
            }
            else if (spell.SpellType == SpellType.Heavy)
            {
                if (fightingComponent.LoadSpellAtSlot(index - inventory.Capacity - fightingComponent.LightSpells.Length, spell))
                {
                    SetupUIPrefab(dragged, index);
                }
            }
        }
        else if (draggedUI.Type == SlotType.LightSpell && toSwapUI.Type == SlotType.LightSpell)
        {
            if (fightingComponent.SwapSpells(SpellType.Light, currentlyDraggedItemIndex - inventory.Capacity, index - inventory.Capacity))
            {
                var temp = itemUIInstances[index].itemGraphic.sprite;
                itemUIInstances[index].SetUp(itemUIInstances[currentlyDraggedItemIndex].itemGraphic.sprite);
                itemUIInstances[currentlyDraggedItemIndex].SetUp(temp);
            }
        }
        else if (draggedUI.Type == SlotType.HeavySpell && toSwapUI.Type == SlotType.HeavySpell)
        {
            if(fightingComponent.SwapSpells(SpellType.Heavy
                , currentlyDraggedItemIndex - inventory.Capacity - fightingComponent.LightSpells.Length
                , index - inventory.Capacity - fightingComponent.LightSpells.Length))
            {
                var temp = itemUIInstances[index].itemGraphic.sprite;
                itemUIInstances[index].SetUp(itemUIInstances[currentlyDraggedItemIndex].itemGraphic.sprite);
                itemUIInstances[currentlyDraggedItemIndex].SetUp(temp);
            }
        }

        ResetDragableUI();
        ResetSelection();
    }
}

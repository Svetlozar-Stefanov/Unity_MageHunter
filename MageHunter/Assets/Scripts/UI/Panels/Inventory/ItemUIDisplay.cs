using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUIDisplay : BaseItemUIDisplay
{
    [SerializeField] public TextMeshProUGUI amountText;
    [SerializeField] private GameObject actionMenu;

    public override event Action<BaseItemUIDisplay> OnItemClicked, OnItemDroppedOn,
        OnItemBeginDrag, OnItemEndDrag;

    public event Action<ItemUIDisplay> OnRightMbtClicked, OnItemUsed, OnItemDroped;

    private bool isInActionMenu = false;
    public bool IsInActionMenu { get => isInActionMenu; set => isInActionMenu = value; }

    private void Awake()
    {
        ResetData();
        Deselect();
        CloseActionMenu();
    }

    public void SetUp(Sprite graphic, int amount)
    {
        itemGraphic.sprite = graphic;
        amountText.text = amount.ToString("n0");

        itemGraphic.gameObject.SetActive(true);
        empty = false;
    }

    public void SetAmount(int amm)
    {
        amountText.text = amm.ToString("n0");
    }

    public void OpenActionMenu()
    {
        actionMenu.SetActive(true);
        isInActionMenu = true;
    }

    public void CloseActionMenu()
    {
        actionMenu.SetActive(false);
        isInActionMenu = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        PointerEventData pointer = (PointerEventData)eventData;
        if (pointer.button == PointerEventData.InputButton.Right)
        {
            OnRightMbtClicked?.Invoke(this);
        }
        else if(!isInActionMenu)
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        PointerEventData pointer = (PointerEventData)eventData;
        if (empty || isInActionMenu || pointer.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        OnItemBeginDrag?.Invoke(this);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        this.OnItemEndDrag?.Invoke(this);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        this.OnItemDroppedOn?.Invoke(this);
    }

    public override void OnDrag(PointerEventData eventData)
    {
    }

    public void OnItemUse()
    {
        if (isInActionMenu)
        {
            OnItemUsed.Invoke(this);
        }
    }

    public void OnItemDrop()
    {
        if (isInActionMenu)
        {
            OnItemDroped.Invoke(this);
        }
    }
}

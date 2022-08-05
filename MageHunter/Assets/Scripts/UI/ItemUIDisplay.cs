using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUIDisplay : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] public Image borderImage;
    [SerializeField] public Image itemGraphic;
    [SerializeField] public TextMeshProUGUI amountText;

    [SerializeField] private GameObject actionMenu;

    public event Action<ItemUIDisplay> OnItemClicked, OnItemDroppedOn,
        OnItemBeginDrag, OnItemEndDrag, OnRightMbtClicked, OnItemUsed, OnItemDroped;

    private bool empty = true;
    private bool isInActionMenu = false;

    public bool Empty { get => empty; }
    public bool IsInActionMenu { get => isInActionMenu; set => isInActionMenu = value; }

    private void Awake()
    {
        ResetData();
        Deselect();
        CloseActionMenu();
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void ResetData()
    {
        itemGraphic.gameObject.SetActive(false);
        empty = true;
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

    public void OnPointerClick(PointerEventData eventData)
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        PointerEventData pointer = (PointerEventData)eventData;
        if (empty || isInActionMenu || pointer.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
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

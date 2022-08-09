using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseItemUIDisplay : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] public Image borderImage;
    [SerializeField] public Image itemGraphic;

    public virtual event Action<BaseItemUIDisplay> OnItemClicked, OnItemDroppedOn,
        OnItemBeginDrag, OnItemEndDrag;

    protected bool empty = true;

    public bool Empty { get => empty; }

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    public virtual void Deselect()
    {
        borderImage.enabled = false;
    }

    public virtual void Select()
    {
        borderImage.enabled = true;
    }

    public virtual void ResetData()
    {
        itemGraphic.gameObject.SetActive(false);
        empty = true;
    }

    public virtual void SetUp(Sprite graphic)
    {
        itemGraphic.sprite = graphic;

        itemGraphic.gameObject.SetActive(true);
        empty = false;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked?.Invoke(this);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        PointerEventData pointer = (PointerEventData)eventData;
        if (empty || pointer.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        OnItemBeginDrag?.Invoke(this);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
    }
}

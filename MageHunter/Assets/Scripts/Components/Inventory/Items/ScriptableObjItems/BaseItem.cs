using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    MiscItem,
    SpellScroll,
    Potion
}

public abstract class BaseItem : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [Header("UI")]
    public Sprite icon;
    public string itemName;
    [TextArea(15, 20)]
    public string description;

    [Header("Specs")]
    [SerializeField] protected float sellValue;

    public virtual void Use()
    {
        return;
    }

}

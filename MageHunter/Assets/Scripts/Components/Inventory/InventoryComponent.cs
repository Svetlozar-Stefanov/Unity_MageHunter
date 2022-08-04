using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private bool flushOnExit = true;

    [SerializeField] private ItemComponent toDropItemPrefab;

    public Inventory Inventory { get => inventory; }

    private void Start()
    {
        inventory.SetUp();
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        var itemComponent = other.GetComponent<ItemComponent>();
        if (itemComponent != null)
        {
            inventory.AddItem(itemComponent.Item, itemComponent.Amount);
            Destroy(other.gameObject);
        }
    }
    
    public void DropItem(int index, int amount)
    {
        InventorySlot data = inventory.DropItem(index, amount);

        if (data == null || toDropItemPrefab == null)
        {
            return;
        }

        toDropItemPrefab.Item = data.Item;
        toDropItemPrefab.Amount = data.Amount;

        Instantiate(toDropItemPrefab, transform.position, transform.rotation);
    }

    private void OnApplicationQuit()
    {
        if (flushOnExit)
        {
            inventory.Clear();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    private Vector3 DROP_OFFSET = new Vector3(0.8f,1,0);

    [SerializeField] private Inventory inventory;
    [SerializeField] private bool flushOnExit = true;

    [SerializeField] private ItemComponent toDropItemPrefab;

    public Inventory Inventory { get => inventory; }

    private void Start()
    {
        inventory.SetUp();
        inventory.onItemDrop += OnDropItem;
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

    public void OnDropItem(InventorySlot toDrop)
    { 
        if (toDrop == null || toDropItemPrefab == null)
        {
            return;
        }

        toDropItemPrefab.Item = toDrop.Item;
        toDropItemPrefab.Amount = toDrop.Amount;

        Instantiate(toDropItemPrefab, transform.position + DROP_OFFSET, transform.rotation).GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(0.0f, 3.0f), ForceMode2D.Impulse);
    }

    private void OnApplicationQuit()
    {
        if (flushOnExit)
        {
            inventory.Clear();
        }
    }
}

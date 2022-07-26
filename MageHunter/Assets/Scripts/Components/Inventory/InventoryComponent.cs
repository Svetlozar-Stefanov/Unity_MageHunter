using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var itemComponent = other.GetComponent<ItemComponent>();
        if (itemComponent != null)
        {
            inventory.AddItem(itemComponent.Item, itemComponent.Amount);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
    }
}

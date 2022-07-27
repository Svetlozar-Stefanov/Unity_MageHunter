using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    [SerializeField] private BaseItem item;
    [SerializeField] private int amount;

    public BaseItem Item { get => item; set => item = value; }
    public int Amount { get => amount; set => amount = value; }

    private void Awake()
    {
        

        GameObject prefabGameObj = Instantiate(item.prefab);
        gameObject.transform.localScale = prefabGameObj.transform.localScale;
        prefabGameObj.transform.SetParent(this.transform);
        prefabGameObj.transform.localPosition = Vector3.zero;
        prefabGameObj.transform.rotation = Quaternion.identity;

        CircleCollider2D collider2D = this.gameObject.AddComponent<CircleCollider2D>();
        CircleCollider2D prefabCollider2D = prefabGameObj.GetComponent<CircleCollider2D>();

        collider2D.isTrigger = true;
        collider2D.transform.position = prefabCollider2D.transform.position;
        collider2D.transform.rotation = prefabCollider2D.transform.rotation;

        collider2D.offset = prefabCollider2D.offset;
        collider2D.radius = prefabCollider2D.radius;

        Rigidbody2D rb2d = this.gameObject.AddComponent<Rigidbody2D>();
        Rigidbody2D prefabRb2d = prefabGameObj.GetComponent<Rigidbody2D>();

        rb2d.bodyType = prefabRb2d.bodyType;
    }
}

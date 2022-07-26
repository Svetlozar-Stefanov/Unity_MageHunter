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
        prefabGameObj.transform.SetParent(this.transform);
        prefabGameObj.transform.localPosition = Vector3.zero;
        prefabGameObj.transform.rotation = Quaternion.identity;

        this.gameObject.AddComponent<CircleCollider2D>().isTrigger 
            = prefabGameObj.GetComponent<CircleCollider2D>().isTrigger;

        this.gameObject.AddComponent<Rigidbody2D>().bodyType = prefabGameObj.GetComponent<Rigidbody2D>().bodyType;
    }
}

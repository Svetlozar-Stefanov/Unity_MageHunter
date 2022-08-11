using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    [SerializeField] private BaseItem item;
    [SerializeField] private int amount;
    [SerializeField] private float timeToWakeUp;

    private CircleCollider2D coll2d;
    private CircleCollider2D prefabCollider2D;
    private float timer = 0;

    public ItemComponent(BaseItem item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public BaseItem Item { get => item; set => item = value; }
    public int Amount { get => amount; set => amount = value; }

    private void Awake()
    {
        GameObject prefabGameObj = Instantiate(item.prefab);
        gameObject.transform.localScale = prefabGameObj.transform.localScale;
        prefabGameObj.transform.SetParent(this.transform);
        prefabGameObj.transform.localPosition = Vector3.zero;
        prefabGameObj.transform.rotation = Quaternion.identity;

        coll2d = this.gameObject.AddComponent<CircleCollider2D>();
        prefabCollider2D = prefabGameObj.GetComponent<CircleCollider2D>();

        coll2d.isTrigger = false;

        coll2d.offset = prefabCollider2D.offset;
        coll2d.radius = prefabCollider2D.radius;
    }

    private void Update()
    {
        if (timer > timeToWakeUp && coll2d.isTrigger == false)
        {
            coll2d.isTrigger = true;
        }
        else if(timer < timeToWakeUp)
        {
            timer += 0.05f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Game/Items/HealthPotion")]
public class HealthPotion : BaseItem
{
    [SerializeField] private float healAmount;

    public HealthPotion()
    {
        type = ItemType.Potion;
    }

    public override void Use()
    {
        HealthComponent player = GameObject.FindWithTag("Player").GetComponent<HealthComponent>();
        if (player != null)
        {
            player.Heal(healAmount);
        }
    }
}

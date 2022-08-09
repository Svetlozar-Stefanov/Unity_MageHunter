using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    Light,
    Heavy
}

[CreateAssetMenu(fileName = "Spell", menuName = "Game/Items/SpellScroll")]
public class SpellScroll : BaseItem
{
    public SpellScroll()
    {
        type = ItemType.SpellScroll;
    }

    [Header("Specs")]
    [SerializeField] private SpellType spellType = SpellType.Light;
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float cooldown = 10;
    [SerializeField] private float manaCost = 5.0f ;

    [SerializeField] private bool hasLifespan = true;
    [SerializeField] private float lifespan = 5.0f;

    public float Speed { get => speed; }
    public float Damage { get => damage; }
    public float Cooldown { get => cooldown; }
    public bool HasLifespan { get => hasLifespan; }
    public float Lifespan { get => lifespan; }
    public float ManaCost { get => manaCost; }
    public SpellType SpellType { get => spellType; }

    public override void Use()
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player != null)
        {
            player.FightingComponent.SpellBook.AddItem(this, 1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Game/Spells/Spell")]
public class SpellContainer : ScriptableObject
{
    [Header("UI Representation")]
    public Sprite image;

    [Header("Specs")]
    public float speed = 40.0f;
    public float damage = 10.0f;
    public float cooldown = 10;
    public float manaCost = 5.0f ;

    public bool hasLifespan = true;
    public float lifespan = 5.0f;
}

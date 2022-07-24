using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private Spell spell;

    public Spell Spell { get => spell; set => spell = value; }

    private float timer = 0.0f;
    private bool canUse = true;

    private void Update()
    {
        if (!canUse)
        {
            timer += 0.05f;
            if (timer >= spell.Data.Cooldown)
            {
                canUse = true;
                timer = 0.0f;
            }
        }
    }

    public bool Cast(Vector3 sTransform, Quaternion sRotation)
    {
        if (!canUse)
        {
            return false;
        }

        Instantiate(spell, sTransform, sRotation).Cast();

        canUse = false;

        return true;
    }
}

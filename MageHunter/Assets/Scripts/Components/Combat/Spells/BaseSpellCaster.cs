using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpellCaster : MonoBehaviour
{
    [SerializeField] private BaseSpell spell;

    public BaseSpell Spell { get => spell; }

    private float timer = 0.0f;
    private bool canUse = true;

    private void Update()
    {
        if (!canUse)
        {
            timer += 0.05f;
            if (timer >= spell.Cooldown)
            {
                canUse = true;
                timer = 0.0f;
            }
        }
    }

    public void Cast(Vector3 sTransform, Quaternion sRotation)
    {
        if (!canUse)
        {
            return;
        }

        Instantiate(spell, sTransform, sRotation).Use();

        canUse = false;
    }
}

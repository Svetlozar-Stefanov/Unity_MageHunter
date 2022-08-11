using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    private SpellScroll spellScroll;
    private Spell spellPrefab;

    public SpellScroll SpellScroll { get => spellScroll; }

    private float timer = 0.0f;
    private bool canUse = true;

    public void SetUp(Spell prefab)
    {
        spellPrefab = prefab;
    }

    public void ResetSpell()
    {
        spellScroll = null;
    }

    public void LoadSpell(SpellScroll spellScroll)
    {
        this.spellScroll = spellScroll;
    }

    private void Update()
    {
        if (!canUse)
        {
            timer += 0.05f;
            if (timer >= spellScroll.Cooldown)
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

        Spell castSpell = Instantiate(spellPrefab, sTransform, sRotation);
        castSpell.SetUp(spellScroll);
        castSpell.Cast();

        canUse = false;

        return true;
    }
}

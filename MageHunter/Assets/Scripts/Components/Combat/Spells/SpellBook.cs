using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    public List<Spell> spells;

    public bool LearnSpell(SpellScroll spellScroll)
    {
        if (!Contains(spellScroll))
        {
            Spell spell = new Spell();
            spell.Data = spellScroll;
            spells.Add(spell);
            return true;
        }

        return false;
    }

    public Spell GetSpell(int indx)
    {
        if (indx < 0 || indx >= spells.Count)
        {
            return null;
        }

        return spells[indx];
    }

    public bool Contains(SpellScroll spellScroll)
    {
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].Data == spellScroll)
            {
                return true;
            }
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingComponent : MonoBehaviour
{
    const int LIGHT_SPELL_COUNT = 3;
    const int HEAVY_SPELL_COUNT = 3;

    const float MANA_REGEN = 0.01f;

    [Header("Setup")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Transform aim;

    [Header("Specs")]
    [SerializeField] private SpellBook spellBook;

    [SerializeField] private float maxMana;

    private SpellCaster[] lightSpells;
    private SpellCaster[] heavySpells;

    private float mana;
    private int lightIdx = 0;
    private int heavyIdx = 0;
    private SpellCaster lightSpellCaster;
    private SpellCaster heavySpellCaster;

    private Vector3 mousePos;

    public float MaxMana { get => maxMana; }
    public float Mana { get => mana; }

    public SpellBook SpellBook { get => spellBook; }

    public SpellScroll CurrentLightSpell { get => lightSpellCaster.Spell.Data; }
    public SpellScroll CurrentHeavySpell { get => heavySpellCaster.Spell.Data; }

    private void Awake()
    {
        mana = maxMana;
        if (lightSpells == null)
        {
            lightSpells = new SpellCaster[LIGHT_SPELL_COUNT];
            for (int i = 0; i < LIGHT_SPELL_COUNT; i++)
            {
                lightSpells[i] = gameObject.AddComponent<SpellCaster>();
                lightSpells[i].Spell = spellBook.GetSpell(i);
            }
        }
        if (heavySpells == null)
        {
            heavySpells = new SpellCaster[HEAVY_SPELL_COUNT];
            for (int i = 0; i < HEAVY_SPELL_COUNT; i++)
            {
                heavySpells[i] = gameObject.AddComponent<SpellCaster>();
                heavySpells[i].Spell = spellBook.GetSpell(i + 3);
            }
        }

        lightSpellCaster = lightSpells[lightIdx];
        heavySpellCaster = heavySpells[heavyIdx];
    }

    private void Update()
    {
        OrientAim();

        ManaRegen();
    }

    public void SetMousePos(Vector2 mousePos)
    {
        this.mousePos = mousePos;
    }

    public void CastLightSpell()
    {
        CastSpell(lightSpellCaster);
    }

    public void CastHeavySpell()
    {
        CastSpell(heavySpellCaster);
    }

    public void LightSpellShift(int offset)
    {
        lightIdx = SpellIndexShift(lightIdx, offset, lightSpells.Length);
        lightSpellCaster = lightSpells[lightIdx];
    }

    public void HeavySpellShift(int offset)
    {
        heavyIdx = SpellIndexShift(heavyIdx, offset, heavySpells.Length);
        heavySpellCaster = heavySpells[heavyIdx];
    }

    private int SpellIndexShift(int old, int offset, int size)
    {
        old += offset;

        if (old < 0)
        {
            old = size - 1;
        }
        else if (old >= size)
        {
            old = 0;
        }

        return old;
    }

    private void OrientAim()
    {
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

        Vector3 rotation = worldMousePos - pivotPoint.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        pivotPoint.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void CastSpell(SpellCaster spellCaster)
    {
        if (mana >= spellCaster.Spell.Data.ManaCost)
        {
            if (spellCaster.Cast(aim.position, aim.rotation))
            {
                mana -= spellCaster.Spell.Data.ManaCost;
            }
            
        }
    }

    private void ManaRegen()
    {
        if (mana < maxMana)
        {
            mana += MANA_REGEN;
        }
        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }
}

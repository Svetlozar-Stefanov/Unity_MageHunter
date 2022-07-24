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
    [SerializeField] private float maxMana;

    [SerializeField] private BaseSpellCaster[] lightSpells;
    [SerializeField] private BaseSpellCaster[] heavySpells;

    private float mana;
    private int lightIdx = 0;
    private int heavyIdx = 0;
    private BaseSpellCaster lightSpellCaster;
    private BaseSpellCaster heavySpellCaster;

    private Vector3 mousePos;

    public float MaxMana { get => maxMana; }
    public float Mana { get => mana; }

    public SpellContainer CurrentLightSpell { get => lightSpellCaster.Spell.SpellData; }
    public SpellContainer CurrentHeavySpell { get => heavySpellCaster.Spell.SpellData; }

    private void Awake()
    {
        mana = maxMana;

        if (lightSpells == null)
        {
            lightSpells = new BaseSpellCaster[LIGHT_SPELL_COUNT];

        }
        if (heavySpells == null)
        {
            heavySpells = new BaseSpellCaster[HEAVY_SPELL_COUNT];

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

    private void CastSpell(BaseSpellCaster spellCaster)
    {
        if (mana >= spellCaster.Spell.ManaCost)
        {
            if (spellCaster.Cast(aim.position, aim.rotation))
            {
                mana -= spellCaster.Spell.ManaCost;
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

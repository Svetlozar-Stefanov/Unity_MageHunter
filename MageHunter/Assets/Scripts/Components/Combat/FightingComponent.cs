using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingComponent : MonoBehaviour
{
    const int LIGHT_SPELL_COUNT = 3;
    const int HEAVY_SPELL_COUNT = 3;

    const float MANA_REGEN = 0.05f;

    [Header("Setup")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Transform aim;

    [SerializeField] private Spell spellPrefab;

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

    public SpellScroll CurrentLightSpell { get => lightSpellCaster.SpellScroll; }
    public SpellScroll CurrentHeavySpell { get => heavySpellCaster.SpellScroll; }
    public SpellCaster[] LightSpells { get => lightSpells; set => lightSpells = value; }
    public SpellCaster[] HeavySpells { get => heavySpells; set => heavySpells = value; }

    private void Awake()
    {
        mana = maxMana;
        if (lightSpells == null)
        {
            lightSpells = new SpellCaster[LIGHT_SPELL_COUNT];
            for (int i = 0; i < LIGHT_SPELL_COUNT; i++)
            {
                lightSpells[i] = gameObject.AddComponent<SpellCaster>();
                lightSpells[i].SetUp(spellPrefab);
            }
        }
        if (heavySpells == null)
        {
            heavySpells = new SpellCaster[HEAVY_SPELL_COUNT];
            for (int i = 0; i < HEAVY_SPELL_COUNT; i++)
            {
                heavySpells[i] = gameObject.AddComponent<SpellCaster>();
                heavySpells[i].SetUp(spellPrefab);
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
        if (lightSpellCaster.SpellScroll == null)
        {
            return;
        }
        CastSpell(lightSpellCaster);
    }

    public void CastHeavySpell()
    {
        if (heavySpellCaster.SpellScroll == null)
        {
            return;
        }
        CastSpell(heavySpellCaster);
    }

    public void LightSpellShift(int offset)
    {
        lightIdx = SpellIndexShift(lightIdx, offset, LightSpells.Length);
        lightSpellCaster = LightSpells[lightIdx];
    }

    public void HeavySpellShift(int offset)
    {
        heavyIdx = SpellIndexShift(heavyIdx, offset, HeavySpells.Length);
        heavySpellCaster = HeavySpells[heavyIdx];
    }

    public bool LoadSpellAtSlot(int index, SpellScroll spellScroll)
    {
        SpellCaster[] spells = null;
        if (spellScroll.SpellType == SpellType.Light)
        {
            spells = lightSpells;
        }
        else if (spellScroll.SpellType == SpellType.Heavy)
        {
            spells = heavySpells;
        }

        if (index < 0 || index >= spells.Length || ContainsSpell(spells, spellScroll))
        {
            return false;
        }

        spells[index].LoadSpell(spellScroll);
        return true;
    }

    public bool SwapSpells(SpellType type ,int i1, int i2)
    {
        SpellCaster[] spells = null;
        if (type == SpellType.Light)
        {
            if (i1 >= lightSpells.Length || i2 >= lightSpells.Length)
            {
                return false;
            }

            spells = lightSpells;
        }
        else if (type == SpellType.Heavy)
        {
            if (i1 >= heavySpells.Length || i2 >= heavySpells.Length)
            {
                return false;
            }
            spells = heavySpells;
        }

        var temp = spells[i1];
        spells[i1] = spells[i2];
        spells[i2] = temp;

        if (type == SpellType.Light || (i1 == lightIdx || i2 == lightIdx))
        {
            lightSpellCaster = lightSpells[lightIdx];
        }
        if (type == SpellType.Heavy && (i1 == heavyIdx || i2 == heavyIdx))
        {
            heavySpellCaster = heavySpells[heavyIdx];
        }

        return true;
    }

    public bool FreeSlot(SpellType spellType, int index)
    {
        if (spellType == SpellType.Light)
        {
            if (index < 0 || index >= lightSpells.Length)
            {
                return false;
            }

            lightSpells[index].ResetSpell();
            lightSpellCaster = lightSpells[lightIdx];
            return true;
        }
        else if (spellType == SpellType.Heavy)
        {
            if (index < 0 || index >= heavySpells.Length)
            {
                return false;
            }

            heavySpells[index].ResetSpell();
            heavySpellCaster = heavySpells[heavyIdx];

            return true;
        }

        return false;
    }

    private bool ContainsSpell(SpellCaster[] spells, SpellScroll spellScroll)
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i].SpellScroll == spellScroll)
            {
                return true;
            }
        }
        return false;
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
        if (mana >= spellCaster.SpellScroll.ManaCost)
        {
            if (spellCaster.Cast(aim.position, aim.rotation))
            {
                mana -= spellCaster.SpellScroll.ManaCost;
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

    private void OnApplicationQuit()
    {
        spellBook.Clear();
    }
}

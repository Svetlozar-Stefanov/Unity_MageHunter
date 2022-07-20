using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingComponent : MonoBehaviour
{
    const int LIGHT_SPELL_COUNT = 3;
    const int HEAVY_SPELL_COUNT = 3;

    [Header("Setup")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Transform aim;

    [Header("Specs")]
    [SerializeField] private BaseSpellCaster[] lightSpells;
    [SerializeField] private BaseSpellCaster[] heavySpells;

    private int lightIdx = 0;
    private int heavyIdx = 0;
    private BaseSpellCaster lightSpellCaster;
    private BaseSpellCaster heavySpellCaster;

    private Vector3 mousePos;

    private void Awake()
    {
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
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

        Vector3 rotation = worldMousePos - pivotPoint.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        pivotPoint.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    public void SetMousePos(Vector2 mousePos)
    {
        this.mousePos = mousePos;
    }

    public void CastLightSpell()
    {
        lightSpellCaster.Cast(aim.position, aim.rotation);
    }

    public void CastHeavySpell()
    {
        heavySpellCaster.Cast(aim.position, aim.rotation);
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
}

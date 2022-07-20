using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingComponent : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Transform aim;

    [Header("Specs")]
    [SerializeField] private BaseSpellCaster lightSpellCaster;
    [SerializeField] private BaseSpellCaster heavySpellCaster;

    private Vector3 mousePos;

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
}

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
    [SerializeField] private BaseProjectile projectile;
    [SerializeField] private float cooldown = 10;

    private Vector3 mousePos;
    private float timer = 0.0f;
    private bool canFire = true;

    private void Update()
    {
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);
         
        Vector3 rotation = worldMousePos - pivotPoint.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        pivotPoint.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += 0.05f;
            if (timer >= cooldown)
            {
                canFire = true;
                timer = 0.0f;
            }
        }
    }

    public void SetMousePos(Vector2 mousePos)
    {
        this.mousePos = mousePos;
    }

    public void SpawnProjectile()
    {
        if (canFire)
        {
            canFire = false;

            Instantiate(projectile, aim.position, aim.rotation);
        }
    }
}

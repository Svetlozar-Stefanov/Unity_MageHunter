using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingComponent : MonoBehaviour
{
    [SerializeField] private Transform aim;
    [SerializeField] private BaseProjectile projectile;

    public void SpawnProjectile()
    {
        Instantiate(projectile, aim.position, aim.rotation);
    }
}

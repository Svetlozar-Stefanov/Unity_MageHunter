using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject ammoType;


    public void Shoot()
    {
        Instantiate(ammoType, firePoint.position, firePoint.rotation);
    }
}

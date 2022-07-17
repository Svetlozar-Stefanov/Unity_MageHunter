using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public HealthComponent healthComponent;

    public void Die()
    {
        Destroy(gameObject);
    }
}

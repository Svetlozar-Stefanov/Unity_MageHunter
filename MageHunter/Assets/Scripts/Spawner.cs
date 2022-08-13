using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private float interval = 10.0f;

    private Transform spawnTransform;


    // Start is called before the first frame update
    void Start()
    {
        spawnTransform = transform;
        InvokeRepeating("Spawn", interval, interval);
    }

    public void Spawn()
    {
        Instantiate(toSpawn, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPerSec : MonoBehaviour
{
    private float timespawn = 0;
    public GameObject GameobjectToSpawn;
    void Update()
    {
        if(timespawn > 0)
        {
            timespawn -= Time.deltaTime;
        }
        else
        {
            Instantiate(GameobjectToSpawn,transform.position,transform.rotation);
            timespawn = Random.Range(3, 7);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectByTag : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Breaker")
            Destroy(gameObject);
        
    }
}

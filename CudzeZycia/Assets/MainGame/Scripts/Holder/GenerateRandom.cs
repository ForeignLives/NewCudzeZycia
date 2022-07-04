using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandom : MonoBehaviour
{
    public ObjectHolder _OB;
    void Start()
    {
        GameObject _GM = _OB.HoldObjects[Random.Range(0, _OB.HoldObjects.Length)];
        Instantiate(_GM, transform.position, transform.rotation, transform);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    float scale;
    void Start()
    {
        scale = Random.Range(0.185f, 0.22f);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}

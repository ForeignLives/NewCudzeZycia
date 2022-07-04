using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector3 initialValue;
    public bool trans = false;
    public float playerRotation;
    public bool playMapExitAnimation = false;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public Transform Target;
    [SerializeField] private Vector3 Offset;
    [SerializeField] public float Distance;
    [SerializeField] public float Height;
    [SerializeField] public float speedCamera;

   

    private void FixedUpdate()
    {
        Offset = new Vector3(-Distance, Height, -Distance);
        Vector3 DistancePosition = Target.position + Offset;
        Vector3 SmoothPosition = Vector3.Lerp(transform.position, DistancePosition, speedCamera);

        transform.LookAt(Target);
        transform.position = SmoothPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMapAndScale : MonoBehaviour
{
    public Camera Cam;
    public float speed;
    public float MaxScale, MinScale;
    private float dragSpeed = 15;

    private void OnEnable()
    {
        if(Cam == null)
        {
            Debug.LogError("MoveMapAndScale nie ma ustawione Cam (kamery), Ten skrypt nie bêdzie dzia³a³. GameObject.name = "+ gameObject.name+ ".");
            enabled = false;
            return;
        }
        Cam.orthographicSize = 200f;
        Cam.transform.position = new Vector3(46.4f, 0, 132.9f);
        dragSpeed = 15;
    }

    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && Cam.orthographicSize > MaxScale)
        {
            Cam.orthographicSize -= speed;
            dragSpeed -= 0.70f;

        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f && Cam.orthographicSize < MinScale)
        {
            Cam.orthographicSize += speed;
            dragSpeed += 0.70f;
        }


        if (Input.GetMouseButton(0))
        {

            Cam.transform.Translate( new Vector3(Input.GetAxis("Mouse X") * -dragSpeed, Input.GetAxis("Mouse Y") * -dragSpeed , 0f) );
        }

        

        

    }
}

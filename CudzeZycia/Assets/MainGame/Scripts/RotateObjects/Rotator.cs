using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotator : MonoBehaviour
{
    [SerializeField] float RotSpeed = 3f;
    //private bool dragging = false;
    //Rigidbody _rb;
    private Transform CameraPos;
    public float offset;
    //float mouseX, mouseY;
    
    Vector3 mPrevPos, mPosDelta = Vector3.zero;


    void Start()
    {
        //_rb = GetComponent<Rigidbody>();
        CameraPos = GameObject.Find("CameraRotator").GetComponent<Transform>();
        transform.position = CameraPos.position - new Vector3(0, 0, -offset);
    }

    /*private void OnMouseDrag()
    {
        dragging = true;
    }*/

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //dragging = false;
            // mouseX = Input.GetAxis("Mouse X");
            // mouseY = Input.GetAxis("Mouse Y");
            mPosDelta = Input.mousePosition - mPrevPos;
            if(Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, CameraPos.transform.right) * Time.deltaTime * RotSpeed, Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, CameraPos.transform.right) * Time.deltaTime * RotSpeed, Space.World);
            }

            transform.Rotate(CameraPos.transform.right,Vector3.Dot(mPosDelta, CameraPos.transform.up) * Time.deltaTime * RotSpeed, Space.World);

        }

        mPrevPos = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
           //mouseX = 0;
           // mouseY = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        /*if(dragging == true)
        {
            float x = Input.GetAxis("Mouse X") * RotSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * RotSpeed * Time.fixedDeltaTime;

            _rb.AddTorque(Vector3.down * x);
            _rb.AddTorque(Vector3.right * y);
        }*/

        //transform.eulerAngles += new Vector3(-mouseY*RotSpeed, -mouseX*RotSpeed);
    }



}

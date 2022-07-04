using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] public float _speed = 2;
    [SerializeField] public float _RunSpeed = 4;
    
    

    private float mainSpeed = 2;
    //[SerializeField] public float _TurnSpeed = 360;
    //[SerializeField] public float _SpeedRotate = 5;
    private float _inputHorizontal;
    public Transform CameraRotation;
    private float _inputVertical;
    private bool ShiftCheck;
    
    private Vector3 _input;
    public Animator anim;
    public VectorValue StartPosition;
    Vector3 moveDirection;

    void Start()
    {
        transform.position = StartPosition.initialValue;
        transform.Rotate(new Vector3(0, StartPosition.playerRotation, 0));
        
    }


    void Update()
    {
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");
        ShiftCheck = Input.GetKey(KeyCode.LeftShift);

        if(Input.GetKeyDown(KeyCode.E))
        {
            StartPosition.playerRotation = transform.eulerAngles.y;
        }
        Rotate();

    }

    private void FixedUpdate()
    {
        Move();
        _rigidbody.AddForce(-transform.up, ForceMode.VelocityChange);
    }

    

    /*void Look()
    {
        if(_input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var skewedInput = matrix.MultiplyPoint3x4(_input);

            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _TurnSpeed * Time.deltaTime);
            
        }
    }*/


    void Move()
    {
        moveDirection = transform.forward * _inputVertical + transform.right * _inputHorizontal;
        
        if (_inputVertical != 0 || _inputHorizontal != 0)
        {
            
            if (ShiftCheck == true)
            {
                mainSpeed = _RunSpeed;
                anim.SetBool("Run", true);
                anim.SetInteger("_Speed", 2);
            }
            else if (ShiftCheck == false)
            {
                mainSpeed = _speed;
                anim.SetBool("Run", false);
                anim.SetInteger("_Speed", 1);
            }
            //_rigidbody.MovePosition(transform.position + (transform.forward * _inputVertical) * mainSpeed * Time.deltaTime, transform.position + (transform.right * _inputHorizontal) * mainSpeed * Time.deltaTime);
            //_rigidbody.AddForce(moveDirection * mainSpeed * Time.deltaTime * 500, ForceMode.Force) ;
            _rigidbody.velocity = moveDirection * mainSpeed;
            
            
            anim.SetBool("Walk", true);
        }
        else if(_inputVertical == 0 && _inputHorizontal == 0)
        {
            anim.SetInteger("_Speed", 0);
            anim.SetBool("Walk", false);
        }


    }

    /*void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }*/

    void Rotate()
        {
        
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, CameraRotation.eulerAngles.y, transform.eulerAngles.z);
            

        }

    }


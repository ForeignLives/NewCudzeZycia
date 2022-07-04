using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;

    [Header("EditedByMax")]
    public Animator anim;
    public VectorValue StartPosition;
    // private GameObject _pause;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();




    void Start()
    {
        // przechodzenie między mapami
        if (StartPosition)
        {
            if (StartPosition.trans)
            {
                transform.position = StartPosition.initialValue;
                transform.eulerAngles = new Vector3(0, StartPosition.playerRotation, 0);
                StartPosition.trans = false;
            }

        }
        else
        {
            Debug.LogWarning("Nie ustawiono pozycji startowej (StartPosition), gracz rozpoczyna grę w miejscu gdzie został pozostawiony GameObject Character");
        }
        // _pause = GameObject.Find("Pause");
    }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        // odpalanie animacji podczas poruszania
        
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (IsRunning == true)
            {
                anim.SetBool("Run", true);
                anim.SetInteger("_Speed", 2);
            }
            else if (IsRunning == false)
            {
                anim.SetBool("Run", false);
                anim.SetInteger("_Speed", 1);
            }
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetInteger("_Speed", 0);
            anim.SetBool("Walk", false);
        }

        //Zapisuje Rotacje gracza po kliknięciu na E Sluzy to do przejscia miedzy pomieszczeniami 
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            StartPosition.playerRotation = transform.eulerAngles.y;
        }*/
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}
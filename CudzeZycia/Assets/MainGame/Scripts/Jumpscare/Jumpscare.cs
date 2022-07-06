using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jumpscare : MonoBehaviour
{
    private GameObject Character;
    private NavMeshAgent agent;
    private Transform JumpscarePosition;
    private FirstPersonLook CameraMove;
    private FirstPersonMovement Movement;
    private Rigidbody RG;
    private pausemenu Pause;
    public GameObject lookat;
    private Vector3 _StartPosition;
    private CyclesMove CyclesMove;
    private Animator _LevelLoader;
    private GameObject STAJ;
    private ScreenJumpscare SJ;
    private AudioSource _Audio;

    private void Start()
    {
        _LevelLoader = GameObject.Find("LevelLoader").GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        JumpscarePosition = GameObject.FindWithTag("JumpscarePosition").transform;
        CameraMove = GameObject.Find("Camera").GetComponent<FirstPersonLook>();
        Movement = GameObject.Find("Character").GetComponent<FirstPersonMovement>();
        RG = GameObject.Find("Character").GetComponent<Rigidbody>();
        Pause = FindObjectOfType<pausemenu>();
        _StartPosition = transform.position;
        CyclesMove = GetComponent<CyclesMove>();
        Character = GameObject.Find("Character");
        STAJ = GameObject.Find("StartPositionAfterJumpscare");
        SJ = GameObject.Find("JumpscareScreenBlack").GetComponent<ScreenJumpscare>();
        _Audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(agent.isStopped == true)
        {
            transform.position = JumpscarePosition.position;
            transform.rotation = JumpscarePosition.rotation;
            Camera.main.transform.LookAt(lookat.transform);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCharacter")
        {
            agent.isStopped = true;
            Movement.enabled = false;
            CameraMove.enabled = false;
            RG.velocity = Vector3.zero;
            Pause.openIsBlocked = true;
            CyclesMove.StartJumpscare = true;
            SJ.IfJumpscare = true;
            StartCoroutine(StartReset());
            _Audio.Play();
        }
    }




    IEnumerator StartReset()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        _LevelLoader.SetBool("Jumpscare", true);


        

        yield return new WaitForSecondsRealtime(2);
        _LevelLoader.SetBool("Jumpscare", false);
        agent.isStopped = false;
        SJ.IfJumpscare = false;
        Character.transform.position = STAJ.transform.position;
        Character.transform.rotation = STAJ.transform.rotation;
        transform.position = _StartPosition;
        Movement.enabled = true;
        CameraMove.enabled = true;
        Pause.openIsBlocked = false;

    }
}

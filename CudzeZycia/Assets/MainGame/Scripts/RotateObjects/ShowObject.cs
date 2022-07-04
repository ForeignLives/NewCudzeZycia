using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : MonoBehaviour
{
    private FirstPersonLook lockcamera;
    private FirstPersonMovement lockmovement;
    private GameObject _outline;
    public GameObject _ShowObject;
    private bool isCollide = false;
    private int Many = 0;
    private pausemenu _pausemenuScript;
    private AudioSource _audio;


    private void Start()
    {
        _audio = GameObject.Find("Character").GetComponent<AudioSource>();
        lockmovement = GameObject.Find("Character").GetComponent<FirstPersonMovement>();
        lockcamera = GameObject.Find("Camera").GetComponent<FirstPersonLook>();
        _outline = transform.Find("outline").gameObject;
        _pausemenuScript = GameObject.Find("PauseMenu").GetComponent<pausemenu>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Collider")
        {
            isCollide = true;
            _outline.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Collider")
        {
            isCollide = false;
            _outline.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCollide == true && Many == 0 && !getIsGamePaused())
        {
            PrzyblizObiekt();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isCollide == true && !getIsGamePaused())
        {
            ZamknijObiekt();
        }
    }


    void PrzyblizObiekt()
    {
        lockcamera.enabled = false;
        lockmovement.enabled = false;
        Instantiate(_ShowObject);
        Many = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _pausemenuScript.openIsBlocked = true;
        _audio.enabled = false;
    }

    void ZamknijObiekt()
    {
        lockcamera.enabled = true;
        lockmovement.enabled = true;
        Many = 0;
        Cursor.lockState = CursorLockMode.Locked;
        _pausemenuScript.openIsBlocked = false;
        Cursor.visible = false;
        _audio.enabled = true;
    }

    private bool getIsGamePaused()
    {
        return _pausemenuScript.isGamePaused;
    }

}

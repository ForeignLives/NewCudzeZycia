using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectAndPlay : MonoBehaviour
{
    private GameObject _outline;
    private bool isCollide = false;


    private void Start()
    {
        _outline = transform.Find("outline").gameObject;
    }

    private void OnTriggerEnter(Collider other)
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
        if (Input.GetKeyDown(KeyCode.E) && isCollide == true)
        {
        }
        
    }



}

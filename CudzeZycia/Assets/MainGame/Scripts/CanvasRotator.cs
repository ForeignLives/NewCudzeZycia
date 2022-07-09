using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    public GameObject _image;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Rotator") == true)
        {
            _image.SetActive(true);
        }
        else if(GameObject.FindGameObjectWithTag("Rotator") == false)
        {
            _image.SetActive(false);
        }
    }
}

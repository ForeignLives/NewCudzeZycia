using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    public GameObject _image;
    private pausemenu Pause;

    private void Start()
    {
        Pause = FindObjectOfType<pausemenu>();
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Rotator") == true)
        {
            _image.SetActive(true);
            Pause.isGamePaused = true;
        }
        else if(GameObject.FindGameObjectWithTag("Rotator") == false)
        {
            _image.SetActive(false);
            Pause.isGamePaused = false;
        }
    }
}

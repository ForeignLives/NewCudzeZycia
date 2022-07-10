using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionDoors : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 playerPosition;
    public VectorValue playerStorage;

    int EClick = 0;
    private DoorOpenCanvasController uiController;
    private bool isInTrigger = false;

    private void Start()
    {
        uiController = FindObjectOfType<DoorOpenCanvasController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            EClick = 1;
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            EClick = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collider"))
        {
            uiController.ShowDoorOpenUI();
            isInTrigger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collider"))
        {
            uiController.HideDoorOpenUI();
            isInTrigger = false;
        }
    }

    public void OnDisable()
    {
        if (isInTrigger)
        {
            isInTrigger = false;
            uiController.HideDoorOpenUI();
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Collider") && EClick == 1)
        {
            StartCoroutine(sceneLoader());
        }
    }

    IEnumerator sceneLoader()
    {
        playerStorage.trans = true;
        playerStorage.playMapExitAnimation = true;
        playerStorage.initialValue = playerPosition;
        yield return new WaitForSeconds(2);
        uiController.HideDoorOpenUI();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void RunSceneLoader()
    {
        // used in Minigame in button Continue
        StartCoroutine(sceneLoader());
    }

}

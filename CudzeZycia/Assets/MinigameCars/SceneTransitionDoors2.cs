using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionDoors2 : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 playerPosition;
    public VectorValue playerStorage;

    int EClick = 0;
    public GameObject uiController;

   

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
            uiController.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collider"))
        {
            uiController.SetActive(false);
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
        uiController.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void RunSceneLoader()
    {
        // used in Minigame in button Continue
        StartCoroutine(sceneLoader());
    }

}

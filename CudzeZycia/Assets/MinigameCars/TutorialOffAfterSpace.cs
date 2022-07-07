using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOffAfterSpace : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOffAfterSpace : MonoBehaviour
{
    public ScoreCounter SC;
    void Update()
    {
        if (SC.Tut == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            {
                SC.Tut = false;
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

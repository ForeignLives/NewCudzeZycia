
using UnityEngine;

public class ScreenJumpscare : MonoBehaviour
{
    public GameObject Screen;
    public bool IfJumpscare = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (IfJumpscare == false)
        {
            Screen.SetActive(false);
        }
        else
            Screen.SetActive(true);
    }
}

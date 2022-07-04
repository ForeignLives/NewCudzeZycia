using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public VectorValue Storage;
    public Animator anim;
    void Start()
    {
        // Nie ustawiaj na false bo wtedy gracza nie mo¿na przesuwaæ
        // Storage.trans = false       
    }

    // Update is called once per frame
    void Update()
    {
        if(Storage.playMapExitAnimation == true)
        {
            Storage.playMapExitAnimation = false;
            anim.SetBool("Start", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMoveLeft : MonoBehaviour
{
    private float LeftSpeed;
    public ScoreCounter SC;
    public bool isCar, isProp;

    private void Start()
    {
        if(isCar)
        {
            LeftSpeed = Random.Range(11, 15);
        }
        else if(isProp)
        {
            LeftSpeed = 10.5f;
        }
        
    }

    private void Update()
    {
        if(SC.Dead == true)
        {
            LeftSpeed = 0;
        }
        transform.position += new Vector3(-LeftSpeed*Time.deltaTime, 0, 0);
        
    }

    

}

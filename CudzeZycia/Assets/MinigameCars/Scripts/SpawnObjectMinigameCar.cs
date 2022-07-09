using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectMinigameCar : MonoBehaviour
{
    public ObjectHolder Top, Mid, Bot;
    public Transform TopTrans, MidTrans, BotTrans;
    public Animation _anim;
    private int randomnumber1, randomnumber2, randomnumber3, SpawnType;
    private int PropType;
    private float _time;
    private bool couritneExe = false;
    public ScoreCounter Poziom;

   /* [Range(0, 12)]
    public float MaxTimer;

    [Range(0, 12)]
    public float MinTimer;*/

    void Start()
    {
        
    }

    
    void Update()
    {
        if(_anim["LoopRoad"].speed >= 1 && Poziom.Dead == false)
        {
            StartCoroutine(_SpawnObject());
        }
    }

   

    IEnumerator _SpawnObject()
    {
        if (couritneExe)
        {
            yield break;
        }

        couritneExe = true;

        //_time = Random.Range(MinTimer, MaxTimer);
        PoziomTrudnosci(Poziom.poziom);
        yield return new WaitForSeconds(_time);
        randomnumber1 = Random.Range(0, 10);
        randomnumber2 = Random.Range(0, 10);
        randomnumber3 = Random.Range(0, 10);
        SpawnType = Random.Range(1, 6);

        SpawnObjects(SpawnType, randomnumber1, randomnumber2, randomnumber3);
        

        couritneExe = false;
    }



    void PoziomTrudnosci(int Poziom)
    {
        if (Poziom == 1)
        {
            _time = Random.Range(1.5f, 2.5f);
        }
        else if(Poziom == 2)
        {
            _time = Random.Range(1.3f, 2f);
        }
        else if (Poziom == 3)
        {
            _time = Random.Range(0.8f, 1.3f);
        }
        else if (Poziom == 4)
        {
            _time = Random.Range(0.6f, 1f);
        }
        else
        {
            _time = Random.Range(0.4f, 0.5f);
        }
    }

    void SpawnObjects(int randomtype, int random1, int random2, int random3)
    {
        if(randomtype == 1)
        {
                Instantiate(Top.HoldObjects[random1], TopTrans.position, TopTrans.rotation);
        }

        if (randomtype == 2)
        {
                Instantiate(Mid.HoldObjects[random2], MidTrans.position, MidTrans.rotation);
        }

        if (randomtype == 3)
        {
                Instantiate(Bot.HoldObjects[random3], BotTrans.position, BotTrans.rotation);
        }

        if (randomtype == 4)
        {
            Instantiate(Top.HoldObjects[random1], TopTrans.position, TopTrans.rotation);
            Instantiate(Bot.HoldObjects[random3], BotTrans.position, BotTrans.rotation);
        }

        if (randomtype == 5)
        {
            Instantiate(Bot.HoldObjects[random3], BotTrans.position, BotTrans.rotation);
            Instantiate(Mid.HoldObjects[random2], MidTrans.position, MidTrans.rotation);
        }

        if (randomtype == 6)
        {
            Instantiate(Top.HoldObjects[random1], TopTrans.position, TopTrans.rotation);
            Instantiate(Mid.HoldObjects[random2], MidTrans.position, MidTrans.rotation);
        }


    }






    /*if (randomnumber1 >= 9)
        {
            Instantiate(Top.HoldObjects[9], transform.position, transform.rotation);
        }
        else if (randomnumber1 < 9 && randomnumber1 > 5)
        {
            Instantiate(Mid.HoldObjects[8], transform.position, transform.rotation);
        }
        else
        {
            PropType = Random.Range(0, 7);
            Instantiate(Bot.HoldObjects[PropType], transform.position, transform.rotation);
        }*/
}

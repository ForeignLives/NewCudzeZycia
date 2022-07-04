using UnityEngine;

public class MovmentCharacterMinigameCar : MonoBehaviour
{
    public Transform TopRoad, MidRoad, BottomRoad;
    public Animator _animation;
    public Animation _Roadspeed;
    public SpriteRenderer SR;
    public ScoreCounter SO;

    private float t;
    private int position;

    void Start()
    {
        position = 0;
        transform.position = MidRoad.position;
        SR.sortingOrder = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SO.start == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && position != 1)
            {
                position += 1;
                SR.sortingOrder += 1;

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && position != -1)
            {
                position -= 1;
                SR.sortingOrder -= 1;
            }

          
                _animation.SetBool("Run", true);
                if (_Roadspeed["LoopRoad"].speed >= 1)
                {
                    if (SO.Dead == true)
                    {
                        _Roadspeed["LoopRoad"].speed = 0;
                        _animation.SetBool("Damage", true);
                    }
                }
            
        }
        
    }


    private void FixedUpdate()
    {
        if (SO.start == true)
        {
            if (position == 0)
            {
                transform.position = MidRoad.position;
            }
            else if (position == 1)
            {
                transform.position = BottomRoad.position;
            }
            if (position == -1)
            {
                transform.position = TopRoad.position;
            }

            t += 0.5f * Time.deltaTime;
            startRoad();
        }
    }




    private void startRoad()
    {
        if(SO.start == true)
        {
            _Roadspeed["LoopRoad"].speed = Mathf.Lerp(0,1,t);
        }
    }


    
}

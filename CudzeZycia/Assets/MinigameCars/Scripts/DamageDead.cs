
using UnityEngine;

public class DamageDead : MonoBehaviour
{
    public ScoreCounter Dead;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Dead.Dead = true;
            Dead.RecordPoints = Dead.points;
        }
        else if(collision.gameObject.tag == "Breaker")
        {
            Destroy(gameObject);
        }
    }
}

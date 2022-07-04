using UnityEngine;

public class AddPoints : MonoBehaviour
{
    public ScoreCounter AddPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AddPoint.PointCollect = true;
            Destroy(gameObject);
        }
    }
}

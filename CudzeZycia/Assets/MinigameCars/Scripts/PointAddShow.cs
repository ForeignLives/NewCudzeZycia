using UnityEngine;
using TMPro;

public class PointAddShow : MonoBehaviour
{
    public ScoreCounter SO;
    private float Fpoints = 0;
    public TMP_Text TextPoints;
    public float speed;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(SO.start == true && SO.Dead == false)
        {
            Fpoints += 1 * Time.deltaTime * speed;
            if(SO.PointCollect == true)
            {
                Fpoints += 100;
                SO.PointCollect = false;
            }
            SO.points = (int)Fpoints;
            TextPoints.text = SO.points.ToString();
        }
    }
}

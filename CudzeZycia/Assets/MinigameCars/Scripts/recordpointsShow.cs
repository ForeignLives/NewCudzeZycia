
using UnityEngine;
using TMPro;

public class recordpointsShow : MonoBehaviour
{
    public ScoreCounter SC;
    public TMP_Text _tmp;

    private void Start()
    {
        
        _tmp.text = SC.RecordPoints.ToString();
    }
}
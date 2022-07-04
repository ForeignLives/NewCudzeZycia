using UnityEngine;
using TMPro;

public class RecordPointsChecker : MonoBehaviour
{ 
    public ScoreCounter SC;
    public TMP_Text _tmp;

    private void Start()
    {
        if (SC.OldRecordPoints < SC.RecordPoints)
        {
            _tmp.text = "Pobiłes rekord!";
        }
        else
        {
            _tmp.text = "Twoj rekord:";
        }
        
    }
}

    


using UnityEngine;
using TMPro;

public class RecordPoints : MonoBehaviour
{
    public ScoreCounter SC;
    public TMP_Text _tmp;

    private void Start()
    {
        if(SC.OldRecordPoints < SC.RecordPoints)
        {
            SC.OldRecordPoints = SC.RecordPoints;
        }
        _tmp.text = SC.OldRecordPoints.ToString();
    }
}

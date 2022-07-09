using UnityEngine;

[CreateAssetMenu]
public class ScoreCounter : ScriptableObject
{
    public int points = 0;
    public int RecordPoints = 0;
    public int OldRecordPoints = 0;
    public bool start = false;
    public int poziom = 1;
    public bool PointCollect = false;
    public bool Dead = false;
    public bool menuGlowne = true;
    public bool PunktyCanvas = false;
    public bool Statystyki = false;
    public bool Tut = true;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlUpgrader : MonoBehaviour
{
    public Animation _anim;
    public ScoreCounter Poziom;
    private bool courtineExe2 = false;

    // Update is called once per frame
    void Update()
    {
        if (_anim["LoopRoad"].speed >= 1)
        {
            StartCoroutine(_LevelUper());
        }
    }

    IEnumerator _LevelUper()
    {
        if (courtineExe2)
        {
            yield break;
        }

        courtineExe2 = true;
        if (Poziom.poziom != 5)
        {
            yield return new WaitForSeconds(15);
            Poziom.poziom += 1;
        }
        courtineExe2 = false;
    }
}

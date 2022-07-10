using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Zarzadzanie1 : MonoBehaviour
{
    public ScoreCounter SC;
    public Animation _anim;

    public GameObject PunktyCanvas;
    public GameObject MenuGlowne;
    public GameObject Statystyki;


    private bool courtineExe2 = false;

    void Start()
    {
        SC.points = 0;
        SC.poziom = 1;
        SC.start = false;
        SC.Dead = false;
        _anim["LoopRoad"].speed = 0;
        SC.menuGlowne = true;
        SC.PunktyCanvas = false;
        SC.Statystyki = false;

        // show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if(SC.PunktyCanvas)
        {
            MenuGlowne.SetActive(false);
            PunktyCanvas.SetActive(true);
        }

        if(SC.Statystyki)
        {
            PunktyCanvas.SetActive(false);
            Statystyki.SetActive(true);
        }

        if(SC.menuGlowne)
        {
            MenuGlowne.SetActive(true);

        }

        if(SC.Dead)
        {
            StartCoroutine(_LevelUper());
        }
        
    }

    public void StartGame()
    {
        SC.start = true;
        SC.menuGlowne = false;
        SC.PunktyCanvas = true;
    }

    public void StatisticsShow()
    {
        SC.start = false;
        SC.PunktyCanvas = false;
        SC.Statystyki = true;
        SC.RecordPoints = SC.points;
    }

    IEnumerator _LevelUper()
    {
        if (courtineExe2)
        {
            yield break;
        }

        courtineExe2 = true;

            yield return new WaitForSeconds(1f);
        StatisticsShow();

        courtineExe2 = false;
    }

    public void Reset()
    {
        SceneManager.LoadScene("MiniGameArcade");
    }

    public void menuexit()
    {
        SceneManager.LoadScene("arcade_interior");
        // Application.Quit();
    }

}

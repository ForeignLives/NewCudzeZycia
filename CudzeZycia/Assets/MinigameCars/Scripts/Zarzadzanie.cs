using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Zarzadzanie : MonoBehaviour
{
    public ScoreCounter SC;
    public Animation _anim;

    public GameObject PunktyCanvas;
    public GameObject MenuGlowne;
    public GameObject Statystyki;

    public GameObject RequirementsGameObject;
    public GameObject ContinueButton;
    private TMP_Text ContinueButtonText;
    private Button ContinueButtonButton;

    public int RequiredPointsToContinue = 2000;

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

        ContinueButtonText = ContinueButton.GetComponentInChildren<TMP_Text>();
        ContinueButtonButton = ContinueButton.GetComponentInChildren<Button>();

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

            if(SC.OldRecordPoints < RequiredPointsToContinue)
            {
                ContinueButtonText.color = Color.gray;
                ContinueButtonButton.interactable = false;
                RequirementsGameObject.SetActive(true);
            }
            else
            {
                ContinueButtonText.color = Color.white;
                ContinueButtonButton.interactable = true;
                RequirementsGameObject.SetActive(false);
            }
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
        SceneManager.LoadScene("MiniGame1");
    }

    public void menuexit()
    {
        SceneManager.LoadScene("MainMenu");
        // Application.Quit();
    }

}

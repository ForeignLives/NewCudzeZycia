using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    private FirstPersonLook lockcamera;
    private FirstPersonMovement lockmovement;
    public GameObject pauseObject; // canvas with pause menu (resume btn, close game, etc)
    public GameObject showAfterSave; // informacja ¿e zapisano gre
    
    public bool isGamePaused = false; // used by exteranal scripts (for example ShowObjects.cs)
    public bool openIsBlocked = false; // used to block open pause menu (for example ShowObjects.cs block pause when preview object)

    private void Start()
    {
        pauseObject.SetActive(false);
        lockmovement = GameObject.Find("Character").GetComponent<FirstPersonMovement>();
        lockcamera = GameObject.Find("Camera").GetComponent<FirstPersonLook>();
    }

    void Update()
    {
        if (!openIsBlocked)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused == true)
            {
                ResumeGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused == false)
            {
                PauseGame(false);
            }
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log("DEBUG f5 save game");
            SaveGame();
        }
#endif
    }


    public void PauseGame(bool onlyFreeze)
    {
        isGamePaused = true;
        Time.timeScale = 0;
        if (!onlyFreeze)
        {
            pauseObject.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        lockcamera.enabled = false;
        lockmovement.enabled = false;
        showAfterSave.SetActive(false);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pauseObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lockcamera.enabled = true;
        lockmovement.enabled = true;
    }

    public void GoToMainMenu()
    {
        // copy from MainMenu.cs
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        // copy from MainMenu.cs
        Application.Quit();
    }

    public void SaveGame()
    {

        FindObjectOfType<SaveGameSingleton>().SaveGame();
        showAfterSave.SetActive(true);
    }
}

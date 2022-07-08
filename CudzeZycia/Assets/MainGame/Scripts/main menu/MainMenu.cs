using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    public MenuButton NewGameBtn;
    public MenuButton LoadGameBtn;
    public MenuButton SettingsBtn;
    public MenuButton CreditsBtn;
    public MenuButton ExitBtn;


    [Header("Containers")]
    public GameObject SettingsContainer;
    public GameObject CreditsContainer;

    private MenuButton selectedButton = null;
    private AudioManager audioManager;

    public void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        NewGameBtn.OnSelectToggle.AddListener((newState) => { OnMenuClick(newState, NewGameBtn); });
        LoadGameBtn.OnSelectToggle.AddListener((newState) => { OnMenuClick(newState, LoadGameBtn); });
        SettingsBtn.OnSelectToggle.AddListener((newState) => { OnMenuClick(newState, SettingsBtn); });
        CreditsBtn.OnSelectToggle.AddListener((newState) => { OnMenuClick(newState, CreditsBtn); });
        ExitBtn.OnSelectToggle.AddListener((newState) => { OnMenuClick(newState, ExitBtn); });

        // ukrywanie przycisku wczytaj je¿eli nie ma sejwa
        LoadGameBtn.gameObject.SetActive(IsSaveGameExist());
    }

    private void OnMenuClick(bool newState, MenuButton thisBtn)
    {
        audioManager.Play("MenuClick");

        if (newState == false)
        {
            selectedButton = null;
            ChangeContainerDisplay();
            return;
        }
        else
        {
            selectedButton = thisBtn;
            // deselect all but not selectedButton
            var _t = new List<MenuButton>() { NewGameBtn, LoadGameBtn, SettingsBtn, CreditsBtn, ExitBtn };
            _t.ForEach((e) => {
                if (e != selectedButton) { e.isSelected = false; }
            });
            ChangeContainerDisplay();
            HandleBtnClick(thisBtn);
        }
    }

    private void ChangeContainerDisplay()
    {
        SettingsContainer.SetActive((selectedButton == SettingsBtn));
        CreditsContainer.SetActive((selectedButton == CreditsBtn));
    }

    private void HandleBtnClick(MenuButton thisBtn)
    {
        if (thisBtn == NewGameBtn) PlayGame();
        if (thisBtn == LoadGameBtn) LoadGameFromSave();
        if (thisBtn == ExitBtn) QuitGame();
    }


    public void PlayGame() {
        FindObjectOfType<SaveGameSingleton>().LoadNewGame();
    }
    public void LoadGameFromSave() {
        FindObjectOfType<SaveGameSingleton>().LoadGame();
    }
    public void GoToSettingsMenu() {
        // OptionsContainer.SetActive(true);

        // SceneManager.LoadScene("SettingsMenu");
    }
    public void GoToCreditsMenu() {
        SceneManager.LoadScene("CreditsMenu");
    }
    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame() {
        Application.Quit();
    }

    private bool IsSaveGameExist()
    {
        return FindObjectOfType<SaveGameSingleton>().IsSaveGameExist();
    }
}

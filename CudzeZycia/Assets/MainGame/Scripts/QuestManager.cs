using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject container;
    public TMP_Text currentQuestTMP_Text;
    public string currentQuest;

    private SaveGameSingleton saveGameInstance;

    private void Awake()
    {
        saveGameInstance = FindObjectOfType<SaveGameSingleton>();
        if (saveGameInstance == null)
        {
            Debug.LogError("Nie znaleziono SaveGameSingleton (ale to pewnie dla tego ?e Debugujesz)");
        }
    }

    public void UpdateCurrentQuestText(string text)
    {
        currentQuestTMP_Text.SetText(text);
        currentQuest = text;
        saveGameInstance.gameState.currentQuestText = text;
        container.SetActive(text != "");
    }
}

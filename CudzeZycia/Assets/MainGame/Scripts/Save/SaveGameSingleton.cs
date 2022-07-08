using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.IO;


[Serializable]
public class SaveContentTrigger
{
    public string id;
    public bool isComplete;
}

[Serializable]
public class ContentState
{
    public string id;
    public bool active;
}

[Serializable]
public class GameSaveData
{
    public string mapName = "";
    public Vector3 characterTransform;
    public Quaternion characterRotation;
    public List<SaveContentTrigger> contentTriggerList = new List<SaveContentTrigger>() { };
    public List<ContentState> contentStateList = new List<ContentState>() { };
    public string currentQuestText = "";
    public DateTime saveCreateDateTime = DateTime.Now;
}



public class SaveGameSingleton : MonoBehaviour
{
    public GameSaveData gameState;
    string saveGamePath = "";

    private bool isSaveLoading = false;

    void OnDestroy()
    {
        Debug.Log("USUÑ OnSceneLoaded");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        saveGamePath = Application.persistentDataPath + "/" + "save.dat";

        gameState = new GameSaveData();
        gameState.mapName = SceneManager.GetActiveScene().name;
        gameState.currentQuestText = "";

        Debug.Log("DODAJ OnSceneLoaded");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isSaveLoading)
        {
            isSaveLoading = false;
            // only after load save (setup player pos etc)

            // setup character
            var characterObj = GameObject.Find("Character");
            characterObj.transform.position = gameState.characterTransform;
            characterObj.transform.rotation = gameState.characterRotation;
        }

        // after every map change (update current quest, etc, map setup)

        // update current map name
        gameState.mapName = SceneManager.GetActiveScene().name;

        // Load current quest
        var questManagerObj = GameObject.FindObjectOfType<QuestManager>();
        if (questManagerObj) questManagerObj.UpdateCurrentQuestText(gameState.currentQuestText);
        else Debug.LogError("QuestManager not found, cant update currentQuestText");

        // Load ContentTriggers
        foreach (var ct in Resources.FindObjectsOfTypeAll<ContentTrigger>())
        {
            var ContentTriggerUniqueId = ct.GetUniqueId();
            var saveContentTrigger = gameState.contentTriggerList.Find(sct => sct.id == ContentTriggerUniqueId);
            if(saveContentTrigger != null)
            {
                ct.isComplete = saveContentTrigger.isComplete;
                ct.gameObject.SetActive(!ct.isComplete);
            }

            // load ContentTriggers states (switches on Map A to make change on Map B)
            var thisContentState = gameState.contentStateList.Find(cs => cs.id == ContentTriggerUniqueId);
            if (thisContentState != null)
            {
                ct.gameObject.SetActive(thisContentState.active);
            }
        }

    }

    public void ContentTriggerUpdate(ContentTrigger ct)
    {
        var thisCtId = ct.GetUniqueId();
        var index = gameState.contentTriggerList.FindIndex(a => a.id == thisCtId);
        if (index != -1)
        {
            // edytuj istniejacy
            gameState.contentTriggerList[index].isComplete = ct.isComplete;
        }
        else
        {
            // dodaj nowy
            gameState.contentTriggerList.Add(new SaveContentTrigger
            {
                id = thisCtId,
                isComplete = ct.isComplete,
            });
        }
    }

    public void ChangeContentStateById(string uniqueId, bool newState)
    {
        var index = gameState.contentStateList.FindIndex(a => a.id == uniqueId);
        if (index != -1)
        {
            // edytuj istniejacy
            gameState.contentStateList[index].active = newState;
        }
        else
        {
            // dodaj nowy
            gameState.contentStateList.Add(new ContentState
            {
                id = uniqueId,
                active = newState,
            });
        }
    }


    public void SaveGame()
    {
        // get data
        gameState.mapName = SceneManager.GetActiveScene().name;
        gameState.characterTransform = GameObject.Find("Character").transform.position;
        gameState.characterRotation = GameObject.Find("Character").transform.rotation;
        gameState.currentQuestText = GameObject.FindObjectOfType<QuestManager>().currentQuest;


        // save to file
        string jsondata = JsonUtility.ToJson(gameState, true);
        File.WriteAllText(saveGamePath, jsondata);
        Debug.Log("Game saved to " + saveGamePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveGamePath))
        {
            string jsondata = File.ReadAllText(saveGamePath);
            gameState = JsonUtility.FromJson<GameSaveData>(jsondata);
        }
        else
        {
            throw new Exception("File with save dont exist (" + saveGamePath + ")");
        }

        // start loading target map
        isSaveLoading = true;
        SceneManager.LoadScene(gameState.mapName);
    }

    public void LoadNewGame()
    {
        gameState = new GameSaveData();
        SceneManager.LoadScene("Hospital");
    }

    public bool IsSaveGameExist()
    {
        try
        {
            // try parse save game
            if (File.Exists(saveGamePath))
            {
                string jsondata = File.ReadAllText(saveGamePath);
                JsonUtility.FromJson<GameSaveData>(jsondata);
                return true;
            }
        } catch (Exception _) { }
        return false;
    }
}

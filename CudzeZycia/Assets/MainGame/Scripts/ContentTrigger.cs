using UnityEngine;
using MyBox;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Scripts.DialogSystem;
using System.Threading.Tasks;
using CleverCrow.Fluid.UniqueIds;
using System.Collections;
using System.Collections.Generic;

public enum ContentTriggerType
{
    Video,
    Dialogs,
    ChangeObjective,
    ChangeGameObjectState,
    TeleportToMap,
}

[System.Serializable]
public class ContentTriggerItem
{
    public ContentTriggerType ContentType = ContentTriggerType.Video;

    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.Video)]
    public VideoClip videoClip;

    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.Dialogs)]
    public DialogGraph dialogGraph;

    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.ChangeObjective)]
    public string newObjective;

    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.ChangeGameObjectState)]
    public GameObject targetGameObject;
    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.ChangeGameObjectState)]
    public bool newActiveState;

    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.TeleportToMap)]
    public string sceneToLoad;
    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.TeleportToMap)]
    public Vector3 playerPosition;
    [ConditionalField(new[] { nameof(ContentType) }, new[] { false }, ContentTriggerType.TeleportToMap)]
    public VectorValue playerStorage;


    public double getVideoDurationSek()
    {
        return videoClip.length;
    }
}



[RequireComponent(typeof(UniqueId))]
public class ContentTrigger : MonoBehaviour
{
    public ContentTriggerItem[] scenarioList;

    private MyVideoPlayer myVideoPlayer;
    private DialogWindow dialogWindow;
    private QuestManager qm;
    private SaveGameSingleton saveGameInstance;
    private pausemenu _pausemenuScript;
    private bool wasExecuted = false;
    public bool isComplete = false;


    void Start()
    {
        if(scenarioList.FirstIndex((e) => e.ContentType == ContentTriggerType.Video) != -1)
        {
            if (!myVideoPlayer) { myVideoPlayer = FindObjectOfType<MyVideoPlayer>(); }
            if (!myVideoPlayer) { Debug.LogError("Nie znaleziono MyVideoPlayer, ContentTrigger nie bêd¹ dzia³aæ poprawnie"); }
        }

        if (scenarioList.FirstIndex((e) => e.ContentType == ContentTriggerType.Dialogs) != -1)
        {
            if (!dialogWindow) { dialogWindow = FindObjectOfType<DialogWindow>(); }
            if (!dialogWindow) { Debug.LogError("Nie znaleziono DialogWindow, ContentTrigger nie bêd¹ dzia³aæ poprawnie"); }
        }

        if (scenarioList.FirstIndex((e) => e.ContentType == ContentTriggerType.ChangeObjective) != -1)
        {
            if (!qm) { qm = FindObjectOfType<QuestManager>(); }
            if (!qm) { Debug.LogError("Nie znaleziono QuestManager, ContentTrigger nie bêd¹ dzia³aæ poprawnie"); }
        }      

        _pausemenuScript = FindObjectOfType<pausemenu>();
        if (!_pausemenuScript) { Debug.LogError("Nie znaleziono PauseMenu, ContentTrigger nie bêd¹ dzia³aæ poprawnie"); }

        saveGameInstance = FindObjectOfType<SaveGameSingleton>();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Debug.Log("WYKRYTO NACISNIECIE F11, SKIP CONTENT");
            DebugSkip().ContinueWith(t => Debug.LogError(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
        }
#endif
    }

    public string GetUniqueId()
    {
        var uniqueId = this.GetComponent<UniqueId>();
        return uniqueId.Id;
    }

    public async Task DebugSkip()
    {
        if(myVideoPlayer) myVideoPlayer.Debug_Skip_On();
        if(dialogWindow) dialogWindow.Debug_Skip_On();
        await Task.Delay(1000); // wait for skip all
        if (myVideoPlayer) myVideoPlayer.Debug_Skip_Off();
        if (dialogWindow) dialogWindow.Debug_Skip_Off();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (wasExecuted) return; // dont run if was executed (run only one time)
        if (other.tag != "Collider") return; // detect only player colisions

        wasExecuted = true;
        Exec();
    }

    private async void Exec()
    {
        // Debug.Log("ContentTrigger ("+gameObject.name + ") Uruchomiono");
        _pausemenuScript.openIsBlocked = true; // zablokuj otwierania menu pauzy
        _pausemenuScript.PauseGame(true);
        foreach (var e in scenarioList)
        {
            if(e.ContentType == ContentTriggerType.Video)
            {
                if (!e.videoClip) { Debug.LogError("videoClip not set! Dont play video."); continue; }
                // Debug.Log("Uruchomiono Video (duration = " + e.videoClip.length + "sek)");
                await myVideoPlayer.PlayVideo(e.videoClip);
            }
            else if (e.ContentType == ContentTriggerType.Dialogs)
            {
                if (!e.dialogGraph) { Debug.LogError("dialogGraph not set! Dont start dialog."); continue; }
                // Debug.Log("Uruchomiono Dialog (name = " + e.dialogGraph.name + "; length = "+ e.dialogGraph.nodes.Count+ ")");
                await dialogWindow.OpenDialogAndWaitForClose(e.dialogGraph);
            }
            else if (e.ContentType == ContentTriggerType.ChangeObjective)
            {
                // Debug.Log("Nowy cel ("+ e.newObjective + ")");
                qm.UpdateCurrentQuestText(e.newObjective);
            }
            else if (e.ContentType == ContentTriggerType.ChangeGameObjectState)
            {
                if(e.targetGameObject.activeSelf == e.newActiveState)
                {
                    Debug.LogWarning("ChangeGameObjectState wybrany GameObject ma ju¿ docelowy stan (" + e.targetGameObject + ".active ju¿ jest " +(e.newActiveState? "on": "off") + ")");
                }
                e.targetGameObject.SetActive(e.newActiveState);
            }
            else if (e.ContentType == ContentTriggerType.TeleportToMap)
            {
                sceneLoader(e);
            }
            else
            {
                Debug.LogError("ContentTriggerType" + e.ContentType + " not implemented");
            }
        }
        _pausemenuScript.openIsBlocked = false; // odblokuj otwieranie menu pauzy
        _pausemenuScript.ResumeGame();

        // Debug.Log("ContentTrigger (" + gameObject.name + ") Done!");
        isComplete = true;
        saveGameInstance.ContentTriggerUpdate(this);
        gameObject.SetActive(false);
    }

    private async void sceneLoader(ContentTriggerItem ct)
    {
        ct.playerStorage.trans = true;
        ct.playerStorage.playMapExitAnimation = true;
        ct.playerStorage.initialValue = ct.playerPosition;
        await Task.Delay(2000);
        SceneManager.LoadScene(ct.sceneToLoad);
    }
}

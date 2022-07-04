using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

/// <summary>
/// Kontroler okna z dialogiem w grze
/// </summary>

namespace Scripts.DialogSystem
{
    public class DialogWindow : MonoBehaviour
    {
        public GameObject DialogUiGameObject;

        public TMP_Text authorText;
        public TMP_Text dialogText;

        public DialogGraph activeDialog;

        public GameObject buttonPrefab;

        public Transform buttonParent;

        public Image LeftAvatarImage;
        public Image RightAvatarImage;

        public Sprite btnBgMain;
        public Sprite btnBgSecondary;
        public Sprite btnBgGray;

        private XNode.Node activeSegment;
        private DialogAvatarManager dialogAvatarManager;
        private bool dialogIsActive = false;
        private bool skipAll = false;


        void Start()
        {
            // on start hide dialog ui
            DialogUiGameObject.SetActive(false);
            dialogIsActive = false;
        }

        private void Awake()
        {
            dialogAvatarManager = DialogAvatarManager.GetResourceDialogAvatarManager(); // informacje o avatarach (tekstury imiona itd)
        }

        public void OpenDialog(DialogGraph dialogGraph)
        {
            activeDialog = dialogGraph;

            // freeze game
            Time.timeScale = 0;

            // show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            DialogUiGameObject.SetActive(true); // show UI
            dialogIsActive = true;
            FindDialogStart(); // build UI
        }

        public async Task OpenDialogAndWaitForClose(DialogGraph dialogGraph)
        {
            OpenDialog(dialogGraph);

            // wait for dialog close
            while (dialogIsActive)
            {
                await Task.Delay(500);
                if (skipAll)
                {
                    CloseDialog();
                }
            }
            return;
        }

        public void CloseDialog()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            DialogUiGameObject.SetActive(false);
            dialogIsActive = false;
        }


        private void FindDialogStart()
        {
            // Znajdz pocz¹tek dialogu (DialogStart)
            foreach (var node in activeDialog.nodes)
            {
                if (node.GetType() == typeof(DialogStart))
                {
                    var port = node.GetPort("Output");
                    if (port.IsConnected)
                    {
                        var firstNode = port.Connection.node as DialogNode;
                        UpdateDialog(firstNode);
                        return;
                    }
                    else
                    {
                        Debug.LogWarning("DialogStart nie jest do niczego podlaczony");
                    }
                }
            }
            // Awaryjnie znajdz pocz¹tek dialogu (pierwszy bez inputa)
            foreach (DialogNode node in activeDialog.nodes)
            {
                if (!node.GetInputPort("input").IsConnected)
                {
                    UpdateDialog(node);
                    Debug.LogWarning("DialogStart wyjatkowo wybieram pierwszy node bez inputa");
                    return;
                }
            }
            Debug.LogWarning("Dialog startowy nie zostal znaleziony!");
        }

        private Sprite GetDialogAnswerBackground(DialogAnswer.AnswerType answerType) => answerType switch
        {
            DialogAnswer.AnswerType.MainQuest => btnBgMain,
            DialogAnswer.AnswerType.Secondary => btnBgSecondary,
            DialogAnswer.AnswerType.Gray => btnBgGray,
            _ => throw new ArgumentOutOfRangeException(nameof(answerType), $"Not expected direction value: {answerType}"),
        };

        private void UpdateDialog(DialogNode newSegment)
        {
            activeSegment = newSegment;
            var avatarName = dialogAvatarManager.GetAvatarName(newSegment.AvatarName);
            dialogText.text = newSegment.DialogText;
            authorText.text = avatarName;

            LeftAvatarImage.enabled = (newSegment.AvatarPosition == AvatarPosition.Left);
            LeftAvatarImage.sprite = dialogAvatarManager.GetAvatarSprite(newSegment.AvatarName);
            RightAvatarImage.sprite = dialogAvatarManager.GetAvatarSprite(newSegment.AvatarName);
            RightAvatarImage.enabled = (newSegment.AvatarPosition == AvatarPosition.Right);


            // usun przyciski wyboru
            int answerIndex = 0;
            foreach (Transform child in buttonParent){
                Destroy(child.gameObject);
            }

            // dodaj nowe przyciski
            if(newSegment.GetType() == typeof(DialogQuestion))
            {
                // wyrenderuj odpowiedzi bo to dialog z wieloma odpowiedziami
                var dialogSegment = newSegment as DialogQuestion;
                foreach (var answer in dialogSegment.Answers)
                {
                    var btn = Instantiate(buttonPrefab, buttonParent);
                    // change button background
                    var imageComponent = btn.GetComponent<Image>();
                    imageComponent.sprite = GetDialogAnswerBackground(answer.Type);
        
                    var textComponent = btn.GetComponentInChildren<Text>();
                    textComponent.text = answer.Message;
                    // we dont change colors now (should be white) but maybe some day we want this again kekw
                    // textComponent.color = answer.GetTextColor(); 

                    var index = answerIndex;
                    btn.GetComponentInChildren<Button>().onClick.AddListener((() => {
                        var nextDialog = dialogSegment.GetNextDialog(index);
                        if (nextDialog != null)
                            UpdateDialog(nextDialog);
                        else
                            CloseDialog();
                    }));

                    answerIndex++;
                }
            }
            else
            {
                // zwyk³e pojedyncze wyjscie
                var btn = Instantiate(buttonPrefab, buttonParent);
                var imageComponent = btn.GetComponent<Image>();

                // domyœlnie niebieskie t³o
                imageComponent.sprite = btnBgSecondary;

                btn.GetComponentInChildren<Text>().text = "Dalej...";
                btn.GetComponentInChildren<Button>().onClick.AddListener((() => {
                    var nextDialog = newSegment.GetNextDialog(0);
                    if (nextDialog != null)
                        UpdateDialog(nextDialog);
                    else
                        CloseDialog();
                }));
            }
            
        }



        public void Debug_Skip_On()
        {
            skipAll = true;
        }

        public void Debug_Skip_Off()
        {
            skipAll = false;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.DialogSystem;

public class ExampleDialogTrigger : MonoBehaviour
{
    public SphereCollider triggerSphereCollider; // z tego gameobject do ktorego jest przypisany
    public Canvas DialogCanvas; // gameobject (canvas) ktory ma Dialog Window
    public DialogGraph dialogGraph; // dialog ktory ma sie pokazac

    private DialogWindow dialogWindowScript;

    void Start()
    {
        triggerSphereCollider.isTrigger = true; // upewnienie siê ¿e jest to trigger
        dialogWindowScript = DialogCanvas.GetComponent<DialogWindow>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        dialogWindowScript.activeDialog = dialogGraph;
        DialogCanvas.gameObject.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        DialogCanvas.gameObject.SetActive(false);
    }
}

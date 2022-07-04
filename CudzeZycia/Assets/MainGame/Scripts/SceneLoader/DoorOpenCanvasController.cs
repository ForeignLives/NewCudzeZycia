using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenCanvasController : MonoBehaviour
{
	public static DoorOpenCanvasController instance;

	public GameObject container;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
    {
        container.SetActive(false);
    }

    public void ShowDoorOpenUI()
    {
        container.SetActive(true);
    }

    public void HideDoorOpenUI()
    {
        container.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class OnSelectToggleEvent : UnityEvent<bool>
{
}

public class MenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image targetImage;
    public Sprite sprite;
    public Sprite pressedSprite;
    public bool isSelected {
        get
        {
            return _select;
        }
        set
        {
            _select = value;
            UpdateSprite();
        }
    }


    bool _select = false;
    public OnSelectToggleEvent OnSelectToggle;

    void Start()
    {
        if (OnSelectToggle == null)
            OnSelectToggle = new OnSelectToggleEvent();
        // OnSelectToggle.AddListener(func);
    }



    public void OnPointerClick(PointerEventData pointerEventData)
    {
        isSelected = !isSelected;
        OnSelectToggle.Invoke(isSelected);
        UpdateSprite();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        targetImage.sprite = pressedSprite;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (isSelected)
        {
            targetImage.sprite = pressedSprite;
        }
        else
        {
            targetImage.sprite = sprite;
        }
    }

}

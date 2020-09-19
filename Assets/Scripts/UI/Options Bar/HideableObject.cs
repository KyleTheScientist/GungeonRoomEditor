using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MouseListener))]
public class HideableObject : MonoBehaviour
{
    public bool hideOnClickElsewhere = true;
    public bool disableShortcutsOnShow = false;
    public float showTime;
    public Action OnShow, OnHide;

    // Start is called before the first frame update
    public void Hide()
    {
        if (disableShortcutsOnShow)
            InputHandler.Instance.shortcutsDisabled = false; //Need to make a stack for disabling keyboard shortcuts
        this.gameObject.SetActive(false);
        OnHide?.Invoke();
    }

    public void Show()
    {
        InputHandler.Instance.shortcutsDisabled = disableShortcutsOnShow;
        showTime = Time.time;
        this.gameObject.SetActive(true);
        OnShow?.Invoke();
    }
}

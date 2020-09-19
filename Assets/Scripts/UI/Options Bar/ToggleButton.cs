using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    private bool m_toggled = false;
    private Transform checkMark;

    public bool Toggled
    {
        get
        {
            return m_toggled;
        }
        set
        {
            m_toggled = value;
            UpdateAppearance();
        }
    }

    void Awake()
    {
        this.checkMark = transform.Find("Check mark");
    }

    public void OnClick()
    {
        this.Toggled = !Toggled;
    }

    private void UpdateAppearance()
    {
        if (!checkMark)
            this.checkMark = transform.Find("Check mark");
        checkMark.gameObject.SetActive(Toggled);
    }
}

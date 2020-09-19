using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLayerButton : MonoBehaviour
{
    private float m_lastClick;
    private bool m_selected;
    private GameObject selectedPanel;
    public TilemapHandler map;
    private Text textComponent;


    public bool Selected
    {
        get { return m_selected; }
        set
        {
            this.m_selected = value;
            this.selectedPanel.SetActive(value);
        }
    }

    void Awake()
    {
        this.selectedPanel = transform.Find("Selected Panel").gameObject;
        this.textComponent = transform.Find("Text").GetComponent<Text>();
    }

    public void OnClick()
    {
        this.Selected = true;
        this.selectedPanel.SetActive(true);
        EnemyLayerHandler.Instance.SetSelectedLayer(this);
    }

    public void SetText(string text)
    {
        this.textComponent.text = text;
    }
}

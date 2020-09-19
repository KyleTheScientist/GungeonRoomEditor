using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NotificationHandler : MonoBehaviour
{
    public static NotificationHandler Instance;
    public float alphaDecay = .01f;

    private Text m_text;
    private float m_alpha = 0f;

    private void Awake()
    {
        NotificationHandler.Instance = this;
        m_text = GetComponent<Text>();
    }

    public void Notify(string text)
    {
        m_text.text = text;
        m_alpha = 1.5f;
    }

    void FixedUpdate()
    {
        if (m_alpha > 0) {
            m_text.color = new Color(1, 1, 1, m_alpha);
            m_alpha -= alphaDecay;
        }

        if (m_alpha <= 0 && m_text.color != Color.clear)
        {
            m_text.color = Color.clear;
            m_alpha = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewRoomMenu : MonoBehaviour
{
    public InputField dimX, dimY;
    public ToggleButton borderButton;
    public static NewRoomMenu Instance;
    private HideableObject m_hideable;

    void Start()
    {
        m_hideable = GetComponent<HideableObject>();
        NewRoomMenu.Instance = this;
        this.gameObject.SetActive(false);
    }

    public void OnCloseClicked()
    {
        m_hideable.Hide();
    }

    public void OnCreateClicked()
    {
        int
            x = int.Parse(dimX.text),
            y = int.Parse(dimY.text);
        if (x < 2 || y < 2)
        {
            Debug.LogError("Dimensions must be greater than 0!");
            return;
        }

        Manager.FilePath = null;
        Manager.drawBorder = borderButton.Toggled;
        Manager.roomSize = new Vector2Int(x, y);
        Manager.Reload();
    }
}
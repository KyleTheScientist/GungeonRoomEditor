using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushButton : MonoBehaviour
{
    public enum BrushType { PENCIL, BRUSH, ERASER, BUCKET }

    public BrushType type;

    public void Awake()
    {
        this.GetComponent<Image>().material = new Material(this.GetComponent<Image>().material);
    }

    public void OnClick()
    {
        InputHandler.Instance.BrushType = type;
        UpdateAppearances();
    }

    public static void UpdateAppearances()
    {
        foreach(var button in FindObjectsOfType<BrushButton>())
        {
            button.GetComponent<Image>().material.SetFloat("_InvertColors", button.type == InputHandler.Instance.BrushType ? 1 : 0);
        }
    }
}

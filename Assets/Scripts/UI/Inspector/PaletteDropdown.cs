using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapType = TilemapHandler.MapType;

public class PaletteDropdown : MonoBehaviour
{
    public TilePalette[] palettes;
    public static PaletteDropdown Instance;
    private Dropdown dropdown;

    private void Awake()
    {
        PaletteDropdown.Instance = this;
        dropdown = GetComponent<Dropdown>();
        ActivePalette = palettes[0];
    }

    public void SetValue(MapType type)
    {
        for(int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text.Equals(type.ToString()))
            {
                dropdown.value = i;
                if (dropdown.value == i)
                    OnValueChanged();
                return;
            }
        }
    }

    public TilePalette ActivePalette { get; private set; }

    public void OnValueChanged()
    {
        MapType type = (MapType)Enum.Parse(typeof(MapType), dropdown.options[dropdown.value].text);
        for(int i = 0; i < palettes.Length; i++)
        {
            if (palettes[i].mapType == type)
            {
                palettes[i].Show();
                ActivePalette = palettes[i];
            }
            else
                palettes[i].Hide();
        }
    }

}

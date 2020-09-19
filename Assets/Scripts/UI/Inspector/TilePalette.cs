using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using MapType = TilemapHandler.MapType;
public class TilePalette : MonoBehaviour
{
    public int columns = 6;
    public GameObject tileButtonPrefab;
    public RectTransform content;
    public List<TileButton> buttons;
    public bool Populated { get; set; }
    public MapType mapType;
    private float margin = 5;

    private Scrollbar m_scrollbar;

    public void Show()
    {
        if (!Manager.Instance.GetTilemap(this.mapType))
            return;
        this.gameObject.SetActive(true);
        InputHandler.Instance.SetActiveTilemap(this.mapType);
        Populate();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void Awake()
    {
        this.content = transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        buttons = new List<TileButton>();
        m_scrollbar = GetComponentInChildren<Scrollbar>();
    }

    private void Start()
    {
        Populate();
    }

    public void Populate()
    {
        if (Populated) return;
        var map = Manager.Instance.GetTilemap(mapType);
        if (!map) return;
        foreach (var tile in map.tiles)
        {
            AddTileButton(tile);
        }
        content.sizeDelta = new Vector2(content.sizeDelta.x, -GetCellPosition(buttons.Count-1).y + CellSize);
        Populated = true;
    }

    public void AddTileButton(Tile tile)
    {
        var button = Instantiate(tileButtonPrefab, content.transform).GetComponent<TileButton>();
        button.tile = tile;
        button.SetTile(tile, mapType);
        buttons.Add(button);
        PositionButton(buttons.Count - 1);
    }

    public void PositionButton(int index)
    {
        var button = buttons[index];
        float width = button.GetComponent<RectTransform>().rect.width;
        //button.GetComponent<RectTransform>().sizeDelta = new Vector2(CellSize, CellSize);
        if (width > CellSize)
        {
            Debug.LogError("Tile buttons are too big! Reduce size to be less than " + CellSize);
            return;
        }
        float offset = (CellSize - width) / 2f + margin;
        button.transform.localPosition = GetCellPosition(index) + new Vector2(offset, -offset);
    }

    public Vector2 GetCellPosition(int index)
    {
        return new Vector2((index % columns) * CellSize, -(index / columns) * CellSize);
    }

    public float CellSize
    {
        get { return (content.rect.width - m_scrollbar.GetComponent<RectTransform>().rect.width - (margin * 2f)) / columns; }
    }

}

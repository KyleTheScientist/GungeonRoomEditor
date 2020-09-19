using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoomData = ImportExport.RoomData;

public class EnemyLayerHandler : MonoBehaviour
{
    public static EnemyLayerHandler Instance;

    public Transform enemyTilemapContainer;
    public RectTransform buttonContainer;
    public GameObject enemyTilemapPrefab;
    public EnemyLayerButton layerButtonPrefab;

    private List<EnemyMap> enemyMaps;
    private List<EnemyLayerButton> buttons;
    private EnemyLayerButton selectedButton;
    private int m_enemyLayer;
    public int verticalMargin = 5;
    public Color outOfFocusColor = new Color(1, 1, 1, .5f);
    public int scheduledLayers = 0;

    public int EnemyMapIndex
    {
        get { return m_enemyLayer; }
        set { this.m_enemyLayer = Mathf.Clamp(value, 0, enemyMaps.Count - 1); }
    }

    public void MoveLayerUp()
    {

    }

    public void MoveLayerDown()
    {

    }

    public EnemyMap GetMap(int index)
    {
        return enemyMaps[index];
    }

    public int LayerCount { get { return enemyMaps.Count; } }

    public void CollectDataForExport(ref RoomData data)
    {
        int count = enemyMaps.Count;
        for (int i = 0; i < count; i++)
        {
            enemyMaps[i].CollectDataForExport(ref data, i);
        }
    }

    void Awake()
    {
        buttons = new List<EnemyLayerButton>();
        EnemyLayerHandler.Instance = this;
        enemyMaps = new List<EnemyMap>();
    }

    void Start()
    {
        AddLayer();
        for (int i = 1; i < scheduledLayers; i++)
        {
            AddLayer();
        }
    }

    public TilemapHandler GetActiveTilemap()
    {
        if (enemyMaps.Count == 0) return null;
        return enemyMaps[EnemyMapIndex];
    }

    public void SetSelectedLayer(EnemyLayerButton button)
    {
        int index = buttons.IndexOf(button);
        this.selectedButton = button;
        this.EnemyMapIndex = index;
        foreach (var b in buttons)
        {
            if (b != button)
                b.Selected = false;
            else
                b.Selected = true;
        }

        for (int i = 0; i < enemyMaps.Count; i++)
        {
            int dist = Math.Min(Math.Abs(index - i), 10);
            enemyMaps[i].GetComponent<Tilemap>().color = i == index ? Color.white : new Color(1, 1, 1, .5f - dist / 3f);
        }
    }

    public void OnClickAddLayer()
    {
        AddLayer();
        PaletteDropdown.Instance.SetValue(TilemapHandler.MapType.Enemies);
    }

    public EnemyMap AddLayer()
    {
        var button = Instantiate(layerButtonPrefab.gameObject, buttonContainer).GetComponent<EnemyLayerButton>();
        button.SetText("Enemy Wave " + buttons.Count);
        buttons.Add(button);
        RepositionButtons();

        var tilemap = Instantiate(enemyTilemapPrefab, enemyTilemapContainer).GetComponent<EnemyMap>();
        button.map = tilemap.GetComponent<EnemyMap>();
        enemyMaps.Add(tilemap);
        SetSelectedLayer(button);
        return tilemap;
    }

    public void RepositionButtons()
    {
        int count = buttons.Count;
        for (int i = 0; i < count; i++)
        {
            if (buttons[i])
                buttons[i].transform.localPosition = new Vector2(0, -i * ButtonHeight - verticalMargin);
        }
        buttonContainer.sizeDelta = new Vector2(buttonContainer.sizeDelta.x, -buttons[count - 1].transform.localPosition.y + ButtonHeight);
    }

    public float ButtonHeight
    {
        get
        {
            return layerButtonPrefab.GetComponent<RectTransform>().rect.height;
        }
    }

    public void RemoveLayer()
    {
        if (!selectedButton) return;
        int index = buttons.IndexOf(selectedButton);
        Destroy(enemyMaps[index].gameObject);
        enemyMaps.RemoveAt(index);
        DestroyImmediate(selectedButton.gameObject);
        buttons.RemoveAt(index);
        if (enemyMaps.Count == 0)
        {
            PaletteDropdown.Instance.SetValue(TilemapHandler.MapType.Environment);
        }
        else
        {
            EnemyMapIndex = Mathf.Clamp(index, 0, enemyMaps.Count - 1);
            SetSelectedLayer(buttons[EnemyMapIndex]);
            RepositionButtons();
        }
    }

}

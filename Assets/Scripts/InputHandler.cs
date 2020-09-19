using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using BrushType = BrushButton.BrushType;
using MapType = TilemapHandler.MapType;
public class InputHandler : MonoBehaviour
{

    public static InputHandler Instance;
    public MouseListener
        EditorPanel,
        OptionsBarPanel,
        InspectorPanel;
    public UIMap uiMap;
    public GridLayout grid;
    public MapType activeTilemap;
    private BrushType m_brushType;
    public bool isPanning, isDrawing;
    public bool shortcutsDisabled;
    private Vector2 m_mousePosition, m_mouseLastPosition;
    public Action<Vector3> OnMousePressed;
    private UndoRedo undoRedo;

    public BrushType BrushType
    {
        get { return m_brushType; }
        set
        {
            this.m_brushType = value;
            BrushButton.UpdateAppearances();
        }
    }
    public Vector2 MousePosition { get { return m_mousePosition; } }
    public Vector2 MouseLastPosition { get { return m_mouseLastPosition; } }


    private Dictionary<MapType, Tile> lastTiles;
    private Tile m_selectedTile;
    public Tile selectedTileType
    {
        get { return m_selectedTile; }
    }

    public void SetSelectedTile(Tile tile, MapType type)
    {
        if (BrushType == BrushType.ERASER)
            BrushType = BrushType.BRUSH;
        this.m_selectedTile = tile;
        lastTiles[type] = tile;
    }

    private void Awake()
    {
        InputHandler.Instance = this;
        activeTilemap = MapType.Environment;
        m_selectedTile = null;
        lastTiles = new Dictionary<MapType, Tile>();
        BrushType = BrushType.BRUSH;
        foreach (var type in Enum.GetValues(typeof(MapType)))
        {
            lastTiles.Add((MapType)type, null);
        }
    }

    private void Start()
    {
        uiMap = FindObjectOfType<UIMap>();
        BrushButton.UpdateAppearances();
        undoRedo = new UndoRedo();
    }

    public void SetActiveTilemap(MapType mapType)
    {
        this.activeTilemap = mapType;
        m_selectedTile = lastTiles[mapType];
    }

    private void Update()
    {
        m_mouseLastPosition = m_mousePosition;
        m_mousePosition = Input.mousePosition;
        HandleShortcuts();
        HandleCursor();
        HandleMouseInput();
    }

    private void HandleShortcuts()
    {

        if (shortcutsDisabled) return;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z))
                undoRedo.Redo();
            else if (Input.GetKeyDown(KeyCode.Z))
                undoRedo.Undo();

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
                FileButton.SaveAs();
            else if (Input.GetKeyDown(KeyCode.S))
                FileButton.Save();

            if (Input.GetKeyDown(KeyCode.N)) FileButton.New();
            if (Input.GetKeyDown(KeyCode.O)) FileButton.Open();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.G)) GridMap.Instance.Toggle();

            if (Input.GetKeyDown(KeyCode.E)) BrushType = BrushType.ERASER;
            if (Input.GetKeyDown(KeyCode.B)) BrushType = BrushType.BRUSH;
            if (Input.GetKeyDown(KeyCode.P)) BrushType = BrushType.PENCIL;
            if (Input.GetKeyDown(KeyCode.F)) BrushType = BrushType.BUCKET;

            var palette = PaletteDropdown.Instance.ActivePalette;
            if (Input.GetKeyDown(KeyCode.Alpha1) && palette.buttons.Count > 0) palette.buttons[0].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha2) && palette.buttons.Count > 1) palette.buttons[1].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha3) && palette.buttons.Count > 2) palette.buttons[2].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha4) && palette.buttons.Count > 3) palette.buttons[3].OnClick();
        }
    }

    private Vector3 panStartPos;
    private void HandleMouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EditorPanel.Hovered)
            {
                isDrawing = true;
                undoRedo.RegisterState(Manager.Instance.GetTilemap(activeTilemap), BrushType.ToString());
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            HideDropdowns();
            OnMousePressed?.Invoke(MousePosition);
            if (EditorPanel.Hovered && isDrawing)
            {
                Manager.Instance.GetTilemap(activeTilemap).HandleMouseDown(MouseToGridPosition(), m_brushType);
            }
        }
        else
        {
            isDrawing = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (EditorPanel.Hovered)
            {
                isPanning = true;
                panStartPos = Input.mousePosition;
            }
        }

        if (Input.GetKey(KeyCode.Mouse2))
            if (isPanning)
                Pan();

        if (EditorPanel.Hovered)
            HandleScroll(Input.mouseScrollDelta.y);

        if (!Input.GetKey(KeyCode.Mouse2) && isPanning)
        {
            isPanning = false;
            if (Vector3.Distance(Input.mousePosition, panStartPos) < .1f)
            {
                grid.transform.position = new Vector3(3, 0, grid.transform.position.z);
            }
        }
    }

    public void HideDropdowns()
    {
        foreach (var obj in FindObjectsOfType<HideableObject>())
        {
            if (obj.gameObject.activeSelf)
                if (obj.gameObject.activeSelf && Time.time - obj.showTime > .1f && obj.hideOnClickElsewhere && !obj.GetComponent<MouseListener>().Hovered)
                    obj.Hide();
        }
    }

    private void HandleScroll(float amount)
    {
        if (amount != 0)
        {
            Vector3 s = grid.transform.localScale;
            s.x = Mathf.Clamp(s.x + amount / 10f, .15f, 2f);
            s.y = s.x;
            grid.transform.localScale = s;
        }
    }

    private void Pan()
    {
        Vector3 d = m_mousePosition - m_mouseLastPosition;
        grid.transform.position += d / 100f / grid.transform.localScale.x;
    }

    private void HandleCursor()
    {
        if (!EditorPanel.Hovered)
        { uiMap.ClearCursor(); return; }

        Vector3Int hoveredCell = MouseToGridPosition();
        if (!TilemapHandler.InBounds(hoveredCell))
        { uiMap.ClearCursor(); return; }

        uiMap.SetCursor(hoveredCell);
    }

    private Vector3Int MouseToGridPosition()
    {
        return grid.WorldToCell(Camera.main.ScreenToWorldPoint(m_mousePosition));
    }

    public void Undo()
    {
        undoRedo.Undo();
    }

    public void Redo()
    {
        undoRedo.Redo();
    }

    public void ClearUndoRedo()
    {
        undoRedo = new UndoRedo();
    }
}

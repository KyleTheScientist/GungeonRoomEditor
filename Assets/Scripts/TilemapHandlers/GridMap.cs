using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridMap : TilemapHandler
{

    public static bool toggled;
    public static GridMap Instance;
    public TileBase gridTile;

    protected override void Awake()
    {
        base.Awake();
        GridMap.Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.gameObject.SetActive(toggled);
        this.ResetAppearance();
    }

    public void Toggle()
    {
        bool active = !this.gameObject.activeSelf;
        toggled = active;
        this.gameObject.SetActive(active);
        if (active)
            ResetAppearance();
    }

    public void ResetAppearance()
    {
        this.map.ClearAllTiles();
        ResizeBounds();
        this.map.FloodFill(map.origin, gridTile);
    }

    public void BoxFill()
    {
        Vector3Int pos = Vector3Int.zero;
        int
            xmin = Bounds.xMin,
            ymin = Bounds.yMin,
            xmax = Bounds.xMax,
            ymax = Bounds.yMax;
        for (int x = xmin; x < xmax; x++)
        {
            for (int y = ymin; y < ymax; y++)
            {
                pos.x = x;
                pos.y = y;
                this.map.SetTile(pos, gridTile);
            }
        }
    }

    public override TileDatabase InitializeDatabase(){ return null; }
}

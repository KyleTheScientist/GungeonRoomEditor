using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CanvasHandler : MonoBehaviour
{
    public static CanvasHandler Instance;

    void Start()
    {
        CanvasHandler.Instance = this;
    }

    public void Trim()
    {
        var envMap = Manager.Instance.GetTilemap(TilemapHandler.MapType.Environment).map;
        var bounds = GetTrimmedBounds(envMap);
        Debug.Log(bounds);

        var newBounds = new BoundsInt(-bounds.size.x / 2, -bounds.size.y / 2, 0, bounds.size.x, bounds.size.y, 1);
        Manager.roomSize.x = newBounds.size.x;
        Manager.roomSize.y = newBounds.size.y;
        TilemapHandler.Bounds = newBounds;

        foreach (var map in FindObjectsOfType<TilemapHandler>())
        {
            var tiles = map.map.GetTilesBlock(bounds);
            map.map.ClearAllTiles();
            map.ResizeBounds();
            map.map.SetTilesBlock(newBounds, tiles);
        }

        FindObjectOfType<GridMap>()?.ResetAppearance();
        InputHandler.Instance.ClearUndoRedo();
    }

    public static BoundsInt GetTrimmedBounds(Tilemap map)
    {
        map.CompressBounds();
        var bounds = map.cellBounds;
        map.ResizeBounds();
        return bounds;
    }
}

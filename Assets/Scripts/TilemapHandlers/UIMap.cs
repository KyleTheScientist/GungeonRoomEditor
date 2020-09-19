using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class UIMap : TilemapHandler
{
    public TileBase cursor;
    private Vector3Int cursorPos = new Vector3Int();

    public void SetCursor(Vector3Int pos)
    {
        if (pos.Equals(cursorPos)) return; 

        map.SetTile(cursorPos, null);
        map.SetTile(pos, InputHandler.Instance.selectedTileType);
        cursorPos = pos;
    }

    public void ClearCursor()
    {
        map.SetTile(cursorPos, null);
        cursorPos = new Vector3Int(-1, -1, -1);
    }

    public override TileDatabase InitializeDatabase() { return null; }
}

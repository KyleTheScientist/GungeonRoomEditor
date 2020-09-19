using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RoomData = ImportExport.RoomData;
public class PlaceableMap : TilemapHandler
{
    protected override void Awake()
    {
        base.Awake();
    }

    //TODO
    public void CollectDataForExport(ref RoomData data)
    {
        BoundsInt boundsInt = new BoundsInt((Vector3Int)Bounds.position, (Vector3Int)Bounds.size);
        var tiles = AllTiles();
        List<string> guids = new List<string>();
        List<Vector2> positions = new List<Vector2>();
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (!tiles[x, y]) continue;
                var tile = tiles[x, y];
                guids.Add(tileDatabase.Entries[tile.name]);
                positions.Add(new Vector2(x, y));
            }
        }
        data.placeableGUIDs = guids.ToArray();
        data.placeablePositions = positions.ToArray();
    }

    public override TileDatabase InitializeDatabase()
    {
        tileDatabase = new PlaceableDatabase();
        tileDatabase.spriteDirectory = "sprites/placeables/";
        return tileDatabase;
    }
}

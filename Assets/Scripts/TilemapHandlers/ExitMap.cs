using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoomData = ImportExport.RoomData;

public class ExitMap : TilemapHandler
{
    public string[] Directions = new string[] { "north", "south", "east", "west" };

    public void CollectDataForExport(ref RoomData data)
    {
        BoundsInt boundsInt = new BoundsInt((Vector3Int)Bounds.position, (Vector3Int)Bounds.size);
        var tiles = AllTiles();
        List<string> directions = new List<string>();
        List<Vector2> positions = new List<Vector2>();
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (!tiles[x, y]) continue;
                var tile = tiles[x, y];
                string name = tile.name.ToLower();
                var position = new Vector2(x + 1, y + 1);
                foreach (var dir in Directions)
                {
                    if (name.Contains(dir))
                    {
                        directions.Add(dir.ToUpper());
                        positions.Add(position);
                        //positions.Add(position + (dir.Equals("north") || dir.Equals("south") ? Vector2.right : Vector2.up));
                    }
                }
            }
        }
        data.exitDirections = data.exitDirections.Concat(directions.ToArray()).ToArray();
        data.exitPositions = data.exitPositions.Concat(positions.ToArray()).ToArray();
    }

    public Tile GetExit(string direction)
    {
        foreach( var tile in tiles)
        {
            if (tile.name.ToLower().Contains(direction.ToLower()))
                return tile;
        }
        return null;
    }

    public override TileDatabase InitializeDatabase()
    {
        tileDatabase = new TileDatabase();
        tileDatabase.Entries = new Dictionary<string, string>()
        {
            { "door_west", null },
            { "door_north", null },
            { "door_south", null },
            { "door_east", null },
        };
        tileDatabase.spriteDirectory = "sprites/doors";
        return tileDatabase;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using RoomData = ImportExport.RoomData;
public class EnemyMap : TilemapHandler
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void CollectDataForExport(ref RoomData data, int index)
    {
        BoundsInt boundsInt = new BoundsInt((Vector3Int)Bounds.position, (Vector3Int)Bounds.size);
        var tiles = AllTiles();
        List<string> guids = new List<string>();
        List<Vector2> positions = new List<Vector2>();
        List<int> layers = new List<int>();
        for (int x = 0; x < tiles.GetLength(0); x++)    
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (!tiles[x, y]) continue;
                var tile = tiles[x, y];
                guids.Add(tileDatabase.Entries[tile.name]);
                positions.Add(new Vector2(x, y));
                layers.Add(index);
            }
        }
        data.enemyGUIDs = data.enemyGUIDs.Concat(guids.ToArray()).ToArray();
        data.enemyPositions = data.enemyPositions.Concat(positions.ToArray()).ToArray();
        data.enemyReinforcementLayers = data.enemyReinforcementLayers.Concat(layers.ToArray()).ToArray();
    }

    public override TileDatabase InitializeDatabase()
    {
        tileDatabase = new EnemyDatabase();
        tileDatabase.spriteDirectory = "sprites/enemies/";
        return tileDatabase;
    }
}

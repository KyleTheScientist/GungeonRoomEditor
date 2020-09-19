using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BrushType = BrushButton.BrushType;
using RoomData = ImportExport.RoomData;
public abstract class TilemapHandler : MonoBehaviour
{
    public TileDatabase tileDatabase;
    public enum MapType { Environment, Exits, Enemies, Placeables, UI }

    [HideInInspector]
    public Tilemap map;
    [HideInInspector]
    public GridLayout grid;
    public List<Tile> tiles;
    public Dictionary<string, Tile> palette;
    public MapType type;
    public bool hasStarted;
    protected bool m_initializedTiles;

    public static BoundsInt Bounds { get; set; }

    public void HandleMouseDown(Vector3Int gridPos, BrushType type)
    {
        if (!InBounds(gridPos)) return;
        Tile tile = InputHandler.Instance.selectedTileType;
        if (!tile && type != BrushType.ERASER) return;

        if (type == BrushType.PENCIL || type == BrushType.ERASER || type == BrushType.BRUSH)
        {
            this.map.SetTile(gridPos, type == BrushType.ERASER ? null : tile);
        }
        else if (type == BrushType.BUCKET)
        {
            if (InputHandler.Instance.activeTilemap == MapType.Environment)
            {
                this.map.FloodFill(gridPos, tile);
                this.map.SetTile(gridPos, tile);
            }
            else
            {
                InputHandler.Instance.BrushType = BrushType.PENCIL;
                this.map.SetTile(gridPos, tile);
            }

        }
    }

    public void Flood(Vector3Int gridPos, Tile tile, Tile startingTile)
    {

        var neighbors = GetGridNeighbors(gridPos);
        foreach (var n in neighbors)
        {
            if (InBounds(n))
            {
                if (startingTile == null)
                {
                    if (map.GetTile(n) == null)
                    {
                        map.SetTile(n, tile);
                        Flood(n, tile, startingTile);
                    }
                }
                else
                {
                    if (map.GetTile(n) != null && !map.GetTile(n).Equals(tile) && map.GetTile(n).Equals(startingTile))
                    {
                        map.SetTile(n, tile);
                        Flood(n, tile, startingTile);
                    }
                }
            }
        }
    }

    public Tile[,] AllTiles()
    {
        int
            xmin = Bounds.xMin,
            ymin = Bounds.yMin,
            width = Bounds.size.x,
            height = Bounds.size.y;
        Tile[,] tiles = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = (Tile)this.map.GetTile(new Vector3Int(xmin + x, ymin + y, 0));
            }
        }
        return tiles;
    }



    public Vector3Int[] GetGridNeighbors(Vector3Int gridPos)
    {
        return new Vector3Int[] {
            gridPos + Vector3Int.right,
            gridPos + Vector3Int.up,
            gridPos + Vector3Int.left,
            gridPos + Vector3Int.down,
        };
    }

    public static bool InBounds(Vector3Int cellPosition)
    {
        int
            xmin = Bounds.xMin,
            ymin = Bounds.yMin,
            xmax = Bounds.xMax,
            ymax = Bounds.yMax;
        return cellPosition.x >= xmin && cellPosition.x < xmax
            && cellPosition.y >= ymin && cellPosition.y < ymax;
    }

    protected virtual void Awake()
    {
        this.map = GetComponent<Tilemap>();
        this.grid = FindObjectOfType<GridLayout>();
    }

    protected virtual void Start()
    {
        GeneratePalette();
        ResizeBounds();
        this.hasStarted = true;
    }

    public void ResizeBounds()
    {
        this.map.origin = new Vector3Int(Bounds.x, Bounds.y, 0);
        this.map.size = new Vector3Int(Bounds.size.x, Bounds.size.y, 0);
        this.map.ResizeBounds();
    }

    public void BuildFromTileArray(Tile[,] tiles)
    {
        //this should be replaced with map.SetTilesBlock but I'm too lazy to do the math
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                this.map.SetTile(GameToLocalPosition(i, j), tiles[i, j]);
            }
        }
    }

    protected virtual void GeneratePalette()
    {
        if (!m_initializedTiles) InitializeTiles();
        palette = new Dictionary<string, Tile>();
        foreach (var tile in tiles)
        {
            palette.Add(tile.name, tile);
        }
    }

    public abstract TileDatabase InitializeDatabase();

    protected virtual void InitializeTiles()
    {
        if (m_initializedTiles) return;
        this.tiles = new List<Tile>();
        var emptyTile = Manager.Instance.emptyTile;

        var tdb = InitializeDatabase();
        if (tdb ==  null)
        {
            m_initializedTiles = true;
            return;
        }

        foreach (var entry in tdb.Entries.Keys)
        {
            var sprite = Resources.Load<Sprite>(System.IO.Path.Combine(tdb.spriteDirectory, entry));

            GameObject tileText = null;

            var tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = sprite ? sprite.texture.CropWhiteSpace().ToSprite() : Manager.Instance.missingImageTileSprite;
            tile.name = entry;
            tile.gameObject = tileText;
            tile.colliderType = emptyTile.colliderType;
            tile.color = emptyTile.color;
            tile.flags = emptyTile.flags;
            tile.hideFlags = emptyTile.hideFlags;
            tile.transform = emptyTile.transform;
            this.tiles.Add(tile);
        }
        m_initializedTiles = true;
    }



    public static Vector3Int GameToLocalPosition(int x, int y)
    {
        return new Vector3Int(x - Manager.roomSize.x / 2, y - Manager.roomSize.y / 2, 0);
    }

    public static Vector3Int GameToLocalPosition(Vector2 vector)
    {
        return GameToLocalPosition((int)vector.x, (int)vector.y);
    }

}

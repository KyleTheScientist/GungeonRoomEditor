using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using MapType = TilemapHandler.MapType;
public static class ImportExport
{
    public static readonly string dataHeader = "***DATA***";
    public static Action postStart, onLoad;

    public static void Export(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            path = Manager.FilePath;
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("No file path to export to.");
                return;
            }
        }
        RoomData data = new RoomData()
        {
            enemyGUIDs = new string[0],
            enemyPositions = new Vector2[0],
            enemyReinforcementLayers = new int[0],
            exitDirections = new string[0],
            exitPositions = new Vector2[0],
            floors = new string[0],
            category = "",
            placeablePositions = new Vector2[0],
            placeableGUIDs = new string[0],
            weight = 1f,
        };

        CanvasHandler.Instance.Trim();

        Manager m = Manager.Instance;
        Texture2D texture = m.GetTilemap(MapType.Environment).GetComponent<EnvironmentMap>().CollectDataForExport();
        DumpTexture(texture);
        EnemyLayerHandler.Instance.CollectDataForExport(ref data);
        m.GetTilemap(MapType.Exits).GetComponent<ExitMap>().CollectDataForExport(ref data);
        m.GetTilemap(MapType.Placeables).GetComponent<PlaceableMap>().CollectDataForExport(ref data);

        Manager.Instance.roomProperties.CollectRoomProperties(ref data);
        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine("\n");
            sw.WriteLine(dataHeader);
            sw.WriteLine(JsonUtility.ToJson(data));
        }
        NotificationHandler.Instance.Notify($"Saved to {path}");
    }

    public static void DumpTexture(Texture2D texture)
    {
        string path = Manager.FilePath;
        File.WriteAllBytes(path, ImageConversion.EncodeToPNG(texture));
    }

    public static void Import(string path)
    {
        Texture2D texture = GetTextureFromFile(path);
        if (texture.width < 2 || texture.height < 2 || texture.width > 999 || texture.height > 999)
        {
            Debug.LogError("Invalid room size");
            return;
        }
        Manager.roomSize = new Vector2Int(texture.width, texture.height);
        Manager.Reload();
        RoomData data = ExtractRoomData(path);
        onLoad = () => PrepareEnemyMaps(data);
        postStart = () => { BuildMapsFromData(texture, data); Manager.Instance.roomProperties.ImportRoomProperties(data); };
        Manager.OnSceneLoaded.Enqueue(onLoad);
        Manager.OnPostStart.Enqueue(postStart);
    }

    public static void BuildMapsFromData(Texture2D texture, RoomData data)
    {
        BuildEnvironmentMapFromData(texture);
        BuildExitMapFromData(data);
        BuildEnemyMapsFromData(data);
        BuildPlaceableMapFromData(data);
    }

    public static void BuildExitMapFromData(RoomData data)
    {
        if (data.exitDirections == null || data.exitPositions == null)
        {
            Debug.Log($"Incomplete or no exit data found");
            return;
        }
        if (data.exitDirections.Length != data.exitPositions.Length)
        {
            Debug.LogError($"Uneven exit data array length: {data.exitPositions.Length} != {data.exitDirections.Length}");
            return;
        }

        ExitMap tilemapHandler = (ExitMap)Manager.Instance.GetTilemap(MapType.Exits);
        string direction;
        Vector2 position;
        var tiles = new Tile[Manager.roomSize.x, Manager.roomSize.y];
        for (int i = 0; i < data.exitDirections.Length; i++)
        {
            position = data.exitPositions[i];
            direction = data.exitDirections[i];
            tiles[(int)position.x - 1, (int)position.y - 1] = tilemapHandler.GetExit(direction);
        }
        tilemapHandler.BuildFromTileArray(tiles);
    }

    private static void BuildEnvironmentMapFromData(Texture2D texture)
    {
        try
        {
            int width = Manager.roomSize.x;
            int height = Manager.roomSize.y;
            var tilemapHandler = Manager.Instance.GetTilemap(MapType.Environment);
            var tiles = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = TileFromColor(texture.GetPixel(x, y), tilemapHandler);
                }
            }
            tilemapHandler.BuildFromTileArray(tiles);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
        }
    }

    private static Tile TileFromColor(Color color, TilemapHandler tilemapHandler)
    {
        if (color == Color.black)
            return tilemapHandler.palette["pit"];
        else if (color == Color.white)
            return tilemapHandler.palette["floor"];
        else if (color == Color.magenta)
            return null;
        else
            return tilemapHandler.palette["wall"];
    }

    private static void BuildPlaceableMapFromData(RoomData data)
    {
        try
        {
            var mapHandler = Manager.Instance.GetTilemap(MapType.Placeables);
            mapHandler.InitializeDatabase();
            string id, guid;
            Vector2 position;
            for (int i = 0; i < data.placeableGUIDs.Length; i++)
            {
                guid = data.placeableGUIDs[i];
                position = data.placeablePositions[i];
                id = mapHandler.tileDatabase.GetID(guid);
                if (id == null || !mapHandler.palette.ContainsKey(id))
                {
                    Debug.LogError($"Tile ID not found: {id}");
                    continue;
                }
                mapHandler.map.SetTile(TilemapHandler.GameToLocalPosition((int)position.x, (int)position.y), mapHandler.palette[id]);
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
        }
    }

    public static void BuildEnemyMapsFromData(RoomData data)
    {
        if (EnemyLayerHandler.Instance.LayerCount == 0) return;
        string guid, id;
        EnemyMap mapHandler;
        Vector2 position;
        List<Tile[,]> tileArrays = new List<Tile[,]>();
        for (int i = 0; i < EnemyLayerHandler.Instance.LayerCount; i++)
        {
            tileArrays.Add(new Tile[Manager.roomSize.x, Manager.roomSize.y]);
        }

        for (int i = 0; i < data.enemyGUIDs.Length; i++)
        {
            int layer = data.enemyReinforcementLayers[i];
            mapHandler = EnemyLayerHandler.Instance.GetMap(layer);
            guid = data.enemyGUIDs[i];
            position = data.enemyPositions[i];
            id = mapHandler.tileDatabase.GetID(guid);
            if (!mapHandler.palette.ContainsKey(id))
            {
                Debug.Log(id);
                continue;
            }
            tileArrays[layer][(int)position.x, (int)position.y] = mapHandler.palette[id];
        }

        for (int i = 0; i < EnemyLayerHandler.Instance.LayerCount; i++)
            EnemyLayerHandler.Instance.GetMap(i).BuildFromTileArray(tileArrays[i]);
    }

    public static void PrepareEnemyMaps(RoomData data)
    {
        if (data.enemyGUIDs == null || data.enemyReinforcementLayers == null || data.enemyPositions == null)
            return;
        if (data.enemyGUIDs.Length != data.enemyReinforcementLayers.Length || data.enemyGUIDs.Length != data.enemyPositions.Length)
        {
            Debug.LogError($"Uneven enemy data array length: {data.enemyGUIDs.Length} != {data.enemyPositions.Length} != {data.enemyReinforcementLayers.Length}");
            return;
        }

        int numLayers = 0;
        foreach (var layer in data.enemyReinforcementLayers)
            numLayers = Mathf.Max(numLayers, layer);
        EnemyLayerHandler.Instance.scheduledLayers = numLayers + 1;
    }

    public static RoomData ExtractRoomData(string path)
    {
        string data = File.ReadAllText(path);
        int end = data.Length - dataHeader.Length - 1;
        for(int i = end; i > 0; i--)
        {
            string sub = data.Substring(i, dataHeader.Length);
            if (sub.Equals(dataHeader))
                return JsonUtility.FromJson<RoomData> (data.Substring(i + dataHeader.Length));
        }

        Debug.Log("Failed to load data");
        return new RoomData();
    }

    public static Texture2D GetTextureFromFile(string path)
    {
        Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        ImageConversion.LoadImage(texture, File.ReadAllBytes(path));
        texture.filterMode = FilterMode.Point;
        texture.name = Path.GetFileName(path);
        return texture;
    }

    public struct RoomData
    {
        public string
            category,
            normalSubCategory,
            specialSubCategory,
            bossSubCategory;
        public Vector2[] enemyPositions;
        public string[] enemyGUIDs;
        public Vector2[] placeablePositions;
        public string[] placeableGUIDs;
        public int[] enemyReinforcementLayers;
        public Vector2[] exitPositions;
        public string[] exitDirections;
        public string[] floors;
        public float weight;
        public bool isSpecialRoom;
        public bool shuffleReinforcementPositions, doFloorDecoration, doWallDecoration, doLighting;
    }
}

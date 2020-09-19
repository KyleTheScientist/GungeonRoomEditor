using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Vector2Int roomSize = new Vector2Int(30, 30);
    public static bool drawBorder = true;
    public static Queue<Action> OnSceneLoaded = new Queue<Action>(), OnPostStart = new Queue<Action>();
    public static bool OpeningFile = false;
    public static Manager Instance;
    public static string FilePath;

    public bool LateStartCompleted = false;
    public InputHandler inputHandler;
    public TilemapHandler environment, exits, placeables;
    public GameObject editorPanel;
    public Tile emptyTile;
    public GameObject tileText;
    public Sprite missingImageTileSprite;
    public Action postStart;
    public RoomProperties roomProperties;



    void Awake()
    {
        Manager.Instance = this;
        SceneManager.sceneLoaded += SceneLoaded;
        this.roomProperties = new RoomProperties();
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        while (OnSceneLoaded.Count > 0)
            OnSceneLoaded.Dequeue()?.Invoke();
    }

    public TilemapHandler GetTilemap(TilemapHandler.MapType type)
    {
        switch (type)
        {
            case TilemapHandler.MapType.Enemies:
                return EnemyLayerHandler.Instance.GetActiveTilemap();
            case TilemapHandler.MapType.Environment:
                return environment;
            case TilemapHandler.MapType.Exits:
                return exits;
            case TilemapHandler.MapType.Placeables:
                return placeables;
        }
        return null;
    }

    void Start()
    {
        TilemapHandler.Bounds = new BoundsInt(-roomSize.x / 2, -roomSize.y / 2, 0, roomSize.x, roomSize.y, 0);
        StartCoroutine("WaitForStart");
        postStart = () =>
        {
            if (string.IsNullOrEmpty(FilePath) && drawBorder)
                ((EnvironmentMap)environment).DrawBorder();
        };
        OnPostStart.Enqueue(postStart);
    }

    public static void Reload()
    {
        SceneManager.LoadScene("RoomEditor");
    }

    public IEnumerator WaitForStart()
    {
        while (true)
        {
            foreach (var map in FindObjectsOfType<TilemapHandler>())
            {
                if (!map.hasStarted)
                    yield return new WaitForSeconds(.05f);
            }
            break;
        }
        while (OnPostStart.Count > 0)
            OnPostStart.Dequeue()?.Invoke();
        LateStartCompleted = true;
    }

}

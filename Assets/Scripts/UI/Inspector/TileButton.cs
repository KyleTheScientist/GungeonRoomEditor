using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using MapType = TilemapHandler.MapType;
public class TileButton : MonoBehaviour
{
    public Tile tile;
    public Text text;
    public Image image;
    private GameObject selectedOutline;
    private MapType mapType;

    private void Awake()
    {
        this.image = this.GetComponent<Image>();
        this.text = this.GetComponentInChildren<Text>();
        this.selectedOutline = transform.Find("Selected Outline").gameObject;
    }

    public void SetTile(Tile tile, MapType mapType)
    {
        this.tile = tile;
        this.mapType = mapType;
        UpdateAppearance();
    }

    public void UpdateAppearance()
    {
        if (!tile)
        {
            Debug.LogError("Attempting to initialize tile button with no assigned tile!");
            return;
        }

        if (tile.sprite != Manager.Instance.missingImageTileSprite)
        {
            Texture2D squaredTexture = tile.sprite.texture.Square();
            this.image.sprite = squaredTexture.ToSprite();
            this.text.enabled = false;
        }
        else
        {
            this.text.text = tile.name.Replace("_", " ");
            this.image.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (InputHandler.Instance?.selectedTileType != tile)
        {
            selectedOutline.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {

        selectedOutline.SetActive(true);
        InputHandler.Instance.SetSelectedTile(tile, mapType);
    }
}

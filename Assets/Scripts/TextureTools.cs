using UnityEngine;
using System;
using System.IO;
public static class TextureTools
{   
    public static Texture2D CropWhiteSpace(this Texture2D orig)
    {
        Rect bounds = orig.GetTrimmedBounds();
        Texture2D result = new Texture2D((int)bounds.width, (int)bounds.height);
        result.name = orig.name;

        for (int x = (int)bounds.x; x < bounds.x + bounds.width; x++)
        {
            for (int y = (int)bounds.y; y < bounds.y + bounds.height; y++)
            {
                result.SetPixel(x - (int)bounds.x, y - (int)bounds.y, orig.GetPixel(x, y));
            }
        }
        result.filterMode = orig.filterMode;
        result.Apply(false, false);

        return result;
    }

    public static Rect GetTrimmedBounds(this Texture2D t)
    {

        int xMin = t.width;
        int yMin = t.height;
        int xMax = 0;
        int yMax = 0;
        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
                if (t.GetPixel(x, y).a != 0)
                {
                    if (x < xMin) xMin = x;
                    if (y < yMin) yMin = y;
                    if (x > xMax) xMax = x;
                    if (y > yMax) yMax = y;
                }
            }
        }
        return new Rect(xMin, yMin, xMax - xMin + 1, yMax - yMin + 1);
    }

    public static Texture2D Square(this Texture2D texture)
    {
        int w = texture.width;
        int h = texture.height;
        int square = Mathf.Max(w, h);
        Texture2D result = new Texture2D(square, square, TextureFormat.RGBAFloat, false);
        int adjX = 0;
        int adjY = 0;
        if (h == square)
            adjX = (square - w) / 2;
        if (w == square)
            adjY = (square - h) / 2;

        for (int i = adjX; i < square; i++)
        {
            for (int j = adjY; j < square; j++)
            {
                result.SetPixel(i, j, Color.clear);
            }
        }

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                result.SetPixel(i + adjX, j + adjY, texture.GetPixel(i, j));
            }
        }
        result.filterMode = texture.filterMode;
        result.Apply(false, false);
        return result;
    }

    public static Sprite ToSprite(this Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 16);
    }
}

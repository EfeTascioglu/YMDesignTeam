using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapTextureGenerator
{
    //color map index must be [y*x_size + x] for it to work
    public static Texture2D TextureFromColorMap(Color[] color_map, int x_size, int y_size)
    {
        Texture2D new_texture = new Texture2D(x_size, y_size);
        new_texture.SetPixels(color_map);
        new_texture.Apply();

        return new_texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] perlinNoise)
    {
        int width = perlinNoise.GetLength(0);
        int height = perlinNoise.GetLength(1);

        Color[] pixel_colors = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pixel_colors[y * width + x] = Color.Lerp(Color.black, Color.white, perlinNoise[x, y]);
            }
        }

        return TextureFromColorMap(pixel_colors, width, height);

    }
    
}

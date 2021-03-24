using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColorMap}
    public DrawMode drawMode;
    public int x_size;
    public int y_size;
    public float scale;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public int seed;

    public TerrainType[] regions;
    // Start is called before the first frame update

    //if some parts are not showing up the Height of the regions must be from lowest to greatest
    public void GenerateNewMap()
    {
        float[,] new_map = Noise.GenerateNoiseMap(x_size, y_size, seed, scale,  octaves, persistance, lacunarity);
        Color[] new_colors = new Color[x_size * y_size];
        for (int x = 0; x < x_size; x++)
        {
            for (int y = 0; y < y_size; y++)
            {
                float current_height = new_map[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (current_height <= regions[i].height)
                    {
                        new_colors[y * x_size + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        DisplayMap displayMap = FindObjectOfType<DisplayMap>();
        if (drawMode == DrawMode.NoiseMap)
        {
            displayMap.DrawTexture(MapTextureGenerator.TextureFromHeightMap(new_map));
        }
        else if(drawMode == DrawMode.ColorMap)
        {
            Debug.Log((new_colors.Length, x_size, y_size).ToString());
            displayMap.DrawTexture(MapTextureGenerator.TextureFromColorMap(new_colors, x_size, y_size));
        }
        
    }

    //very handy for changing/setting inspector values
    private void OnValidate()
    {
        if(x_size < 1)
        {
            x_size = 1;
        }

        if(y_size < 1)
        {
            y_size = 1;
        }

        if(scale < 1)
        {
            scale = 1;
        }

        if(octaves < 0)
        {
            octaves = 0;
        }

        if (persistance < 0)
        {
            persistance = 1;
        }

        if (lacunarity < 0)
        {
            lacunarity = 1;
        }
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string terrain_name;
        public float height;
        public Color color;
    }
}

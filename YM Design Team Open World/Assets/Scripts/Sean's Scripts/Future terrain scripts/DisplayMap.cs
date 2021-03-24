using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour
{
    public Renderer display;

    public void DrawMap(float[,] perlinNoise)
    {
        int width = perlinNoise.GetLength(0);
        int height = perlinNoise.GetLength(1);

        Texture2D texture = new Texture2D(width, height);
        Color[] pixel_colors = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pixel_colors[y * width + x] = Color.Lerp(Color.black, Color.white, perlinNoise[x, y]);
            }
        }

        texture.SetPixels(pixel_colors);
        texture.Apply();

        display.sharedMaterial.mainTexture = texture;
        display.gameObject.transform.localScale = new Vector3(width, 1, height);

    }
}

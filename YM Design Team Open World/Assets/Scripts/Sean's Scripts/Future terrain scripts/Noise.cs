using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
//static classes can be referenced from anywhere
    public static float[,] GenerateNoiseMap(int x_size, int y_size, float scale)
    {
        //create a perlinNoise grid
        //the grid must be made from 2 int sizes
        float[,] noise_map = new float[x_size, y_size];
        if(scale <= 0)
        {
            scale = 0.001f;
        }

        for (int x = 0; x < x_size; x++)
        {
            for (int y = 0; y < y_size; y++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinNoise = Mathf.PerlinNoise(sampleX, sampleY);
                //generate and then assign the perlin Noise value(its a float)
                noise_map[x, y] = perlinNoise;
            }
        }

        return noise_map;
    }
}

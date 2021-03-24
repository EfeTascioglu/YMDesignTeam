using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    //static classes can be referenced from anywhere
    //octaves are the number of layers, persistance is, lacunarity is

    
    public static float[,] GenerateNoiseMap(int x_size, int y_size, int seed, float scale, int octaves, float persistance, float lacunarity)
    {
        //create a perlinNoise grid
        //the grid must be made from 2 int sizes
        float[,] noise_map = new float[x_size, y_size];
        float max_height = float.MinValue;
        float min_height = float.MaxValue;

        //phsydo randomness
        System.Random prng = new System.Random(seed);
        //will allow the noise to be shifted generating different terran dependant on the seed
        Vector2[] octaves_offset = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offset_x = prng.Next(-100000, 100000);
            float offset_y = prng.Next(-100000, 100000);

            octaves_offset[i] = new Vector2(offset_x, offset_y);
        }
        for (int x = 0; x < x_size; x++)
        {
            for (int i = 0; i < y_size; i++)
            {

            }
        }
        if(scale <= 0)
        {
            scale = 0.001f;
        }

        for (int x = 0; x < x_size; x++)
        {
            for (int y = 0; y < y_size; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noise_height = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - x_size/2) / scale * frequency + octaves_offset[i].x;
                    float sampleY = (y - y_size/2) / scale * frequency + octaves_offset[i].y;

                    //makes the perlin Noise value negative as well
                    //usually will return between 0-1
                    float perlinNoise = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    //generate and then assign the perlin Noise value(its a float)
                    noise_map[x, y] = perlinNoise;
                    noise_height += perlinNoise * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;

                    
                }
                if (noise_height > max_height)
                {
                    max_height = noise_height;
                }
                else if ( noise_height < min_height)
                {
                    min_height = noise_height;
                }
                noise_map[x, y] = noise_height;
            }
        }

        for (int x = 0; x < x_size; x++)
        {
            for (int y = 0; y < y_size; y++)
            {
                //normalizes all the noise to be 0-1
                noise_map[x, y] = Mathf.InverseLerp(min_height, max_height, noise_map[x, y]);
            }
        }

        return noise_map;
    }
}

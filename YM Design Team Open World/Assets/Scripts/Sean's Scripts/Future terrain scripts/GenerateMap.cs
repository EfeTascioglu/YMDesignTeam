using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public int x_size;
    public int y_size;
    public float scale;
    // Start is called before the first frame update
    public void GenerateNewMap()
    {
        float[,] new_map = Noise.GenerateNoiseMap(x_size, y_size, scale);
        DisplayMap displayMap = FindObjectOfType<DisplayMap>();
        displayMap.DrawMap(new_map);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour
{

    public Renderer display;
    public void DrawTexture(Texture2D texture)
    {
        //using point to reduce the blurryness between pixels
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        display.sharedMaterial.mainTexture = texture;
        display.transform.localScale = new Vector3(texture.width, 1, texture.height);

    }
}

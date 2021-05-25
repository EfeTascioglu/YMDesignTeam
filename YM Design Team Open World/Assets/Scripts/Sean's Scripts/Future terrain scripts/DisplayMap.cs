using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour
{

    public Renderer display;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    public void DrawTexture(Texture2D texture)
    {
        //using point to reduce the blurryness between pixels
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        display.sharedMaterial.mainTexture = texture;
        display.transform.localScale = new Vector3(texture.width, 1, texture.height);

    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        for (int i = 0; i < 5; i++)
        {
            print(meshData.triangles[i]);
        }

        meshRenderer.sharedMaterial.mainTexture = texture;
        meshCollider.sharedMesh = meshData.CreateMesh();
    }
}

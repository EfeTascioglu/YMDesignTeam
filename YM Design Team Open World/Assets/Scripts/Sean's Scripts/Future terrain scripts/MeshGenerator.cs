using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    //returns MeshData to prevent game from freezing
    public static MeshData GenerateTerrainMesh(float[,] perlinNoise, float height_multiplier, AnimationCurve mesh_curve)
    {
        int width = perlinNoise.GetLength(0);
        int height = perlinNoise.GetLength(1);
        //the top left x is negative while the top left z global position will be positive (why it's -2f then 2f)
        //needs 2f the f will make the result a float
        float top_left_x = (width - 1) / -2f;
        float top_left_z = (height - 1) / 2f;
        MeshData meshData = new MeshData(height, width);
        int vertexIndex = 0;
        Debug.Log("does this even work");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //uses the perlinNoise value as the height of the vertex, x is the x and y is the z axis(height on top view)
                //mesh_curve evaluates the actual value from the curve to apply(used to make specific heights smoother than others)
                meshData.vertices[vertexIndex] = new Vector3(top_left_x + x, mesh_curve.Evaluate(perlinNoise[x,y])*height_multiplier, top_left_z - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                //ignore vertices that are on the bottom or right side edge
                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;
    public MeshData(int MeshHeight, int MeshWidth)
    {
        vertices = new Vector3[MeshHeight * MeshWidth];
        triangles = new int[(MeshHeight - 1) * (MeshWidth - 1) * 6];
        uvs = new Vector2[MeshHeight * MeshWidth];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;

        triangleIndex += 3;
    }

    //get mesh from data
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        //recalculates the lighting
        mesh.RecalculateNormals();
        return mesh;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStructures : MonoBehaviour
{
    public int spawn_size_x;
    public int spawn_size_y;
    public int percentage;
    public int spawn_height;

    public GameObject house;
    public LayerMask terrain;
    private void Start()
    {
        for (int x = -1*spawn_size_x/2; x < spawn_size_x/2; x++)
        {
            for (int y = -1*spawn_size_y/2; y < spawn_size_y/2; y++)
            {
                int c = Random.Range(0, 101*(1000-percentage));
                if(c == 0)
                {
                    int r = Random.Range(0, 360);
                    GameObject structure = Instantiate(house, new Vector3(x, spawn_height, y), Quaternion.Euler(0, r, 0) );
                    Transform[] corners = new Transform[4];
                    corners[0] = structure.transform.Find("Corner");
                    corners[1] = structure.transform.Find("Corner (1)");
                    corners[2] = structure.transform.Find("Corner (2)");
                    corners[3] = structure.transform.Find("Corner (3)");
                    print(corners.Length);

                    float shortest_distance = 100000;
                    for (int i = 0; i < corners.Length; i++)
                    {
                        Debug.Log(corners[i].transform.position);
                        RaycastHit hit;
                        Ray down = new Ray(corners[i].position, Vector3.down);

                        if (Physics.Raycast(down, out hit, spawn_height, terrain, QueryTriggerInteraction.Collide))
                        {
                            if (hit.distance < shortest_distance)
                            {
                                shortest_distance = hit.distance;
                            }
                        }
                    }

                    structure.transform.position -= new Vector3(0, shortest_distance, 0);
                    RaycastHit hit2;
                    Ray down2 = new Ray(structure.transform.position, Vector3.down);
                    if (Physics.Raycast(down2, out hit2, spawn_height, terrain, QueryTriggerInteraction.Collide))
                    {
                        structure.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit2.normal);
                    }
                        x += 20;
                    y += 20;
                }
            }
        }
    }

}

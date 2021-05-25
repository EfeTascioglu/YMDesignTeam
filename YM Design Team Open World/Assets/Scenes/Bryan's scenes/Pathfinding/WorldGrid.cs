using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour {
    public LayerMask unwalkableMask;
    public Vector2 gridSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeD; //diameter of a node
    int gNumX, gNumY; //number of nodes that can fit in the grid in the x direction and y direction (different from entries of gridSize)

    void Start() {
        nodeD = nodeRadius * 2; 
        gNumX = Mathf.RoundToInt(gridSize.x / nodeD);
        gNumY = Mathf.RoundToInt(gridSize.y / nodeD);
        CreateGrid();
    }

    void CreateGrid() { //populates grid with all the nodes
        grid = new Node[gNumX, gNumY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

        for (int x = 0; x < gNumX; ++x) {
            for (int y = 0; y < gNumY; ++y) {
                Vector3 worldPoint = bottomLeft + Vector3.right * (nodeD * x + nodeRadius) + Vector3.forward * (nodeD * y + nodeRadius); //position of Node
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node n) { //returns all nodes that n can get to in 1 step
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; ++x ) {
            for (int y = -1; y <= 1; ++y) {
                if (x == 0 && y == 0) continue;

                int cx = n.gX + x, cy = n.gY + y;
                if ((0 <= cx && cx < gNumX) && (0 <= cy && cy < gNumY)) neighbours.Add(grid[cx,cy]); //<gNumX and Y since grid is 0-indexed
            }
        }

        return neighbours;
    }

    public Node NodeFromPos(Vector3 pos) { //returns a Node corresponding to a position
        float percentX = (pos.x + gridSize.x / 2) / gridSize.x;
        float percentY = (pos.z + gridSize.y / 2) / gridSize.y; //we use pos.z bc the z is the y when we look down on the plane
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gNumY - 1) * percentX);
        int y = Mathf.RoundToInt((gNumY - 1) * percentY);

        return grid[x,y];
    }

    public List<Node> path;
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y)); //bc the x axis in unity is our y axis

        if (grid != null) {
            foreach (Node n in grid) {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (path != null && path.Contains(n)) Gizmos.color = Color.black; 
                Gizmos.DrawCube(n.wPos, Vector3.one * (nodeD - 0.1f));
            }
        }
    }
}

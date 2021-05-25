using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
    WorldGrid grid;

    public Transform seeker, target;

    void Awake() {
        grid = GetComponent<WorldGrid>();
    }

    void Update() {
        findPath(seeker.position, target.position);
    }

    void findPath(Vector3 startPos, Vector3 endPos) {
        Node sNode = grid.NodeFromPos(startPos);
        Node eNode = grid.NodeFromPos(endPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(sNode);

        while(openSet.Count > 0) {

            //find the open node with the lowest f cost (or among ones with the same f costs, the lowest h cost) with a linear search
            Node curr = openSet[0];
            for (int i = 1; i < openSet.Count; ++i)
                if (openSet[i].f < curr.f || (openSet[i].f == curr.f && openSet[i].h < curr.h)) curr = openSet[i];

            openSet.Remove(curr); closedSet.Add(curr);
            if (curr == eNode) { RetracePath(sNode, eNode); return; }
               
            foreach (Node nei in grid.GetNeighbours(curr)) {
                if (!nei.walkable || closedSet.Contains(nei)) continue;

                int newG = curr.g + CalcD(curr, nei);
                if (newG < nei.g || !openSet.Contains(nei)) {
                    nei.g = newG;
                    nei.h = CalcD(nei, eNode);
                    nei.parent = curr;
                    if (!openSet.Contains(nei)) openSet.Add(nei);
                }
            }    
        }
    }

    void RetracePath(Node s, Node e) {
        List<Node> path = new List<Node>();
        Node curr = e;

        while (curr != s) {
            path.Add(curr); curr = curr.parent;
        }

        path.Reverse();
        grid.path = path;
    }

    //heuristic function for estimating the distance between node a and node b; but also acts as a function giving actual distance if a and b r neighbours
    int CalcD(Node a, Node b) {
        int dstX = Mathf.Abs(a.gX - b.gX), dstY = Mathf.Abs(a.gY - b.gY);

        if (dstX > dstY) return 14 * dstY + 10 * (dstX-dstY);
        else return 14 * dstX + 10 * (dstY-dstX);
    }
}

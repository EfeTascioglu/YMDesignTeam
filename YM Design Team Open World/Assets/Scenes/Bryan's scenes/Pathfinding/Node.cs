using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public bool walkable;
    public Vector3 wPos;
    public int gX, gY;

    public Node parent;
    public int g;
    public int h;
    public int f{
        get { return g + h; }
    }

    public Node(bool _walkable, Vector3 _wPos, int _gX, int _gY) {
        walkable = _walkable;
        wPos = _wPos;
        gX = _gX;
        gY = _gY;
    }


}
